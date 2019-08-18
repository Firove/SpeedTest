using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Data;
using System.IO;

public class OscillatorTestCtrl : MonoBehaviour
{
    public RandOscillatorValues _randOscVals;
    public Slider _RatingSlider;

    public UnityEvent _Submit;
    
    [System.Serializable]
    public class EventWithFloat : UnityEvent<float> { };
    public EventWithFloat _Difference;

    [System.Serializable]
    public class EventWithString : UnityEvent<string> { };
    public EventWithString _SaveFilePath;

    [System.Serializable]
    public class EventWithInt : UnityEvent<int> { };
    public EventWithInt _TestCount;

    public EventWithFloat _BestResultEvt;
    public EventWithFloat _AverageResultEvt;

    public string _SavePrefix = "Rolling";

    public DataTable _DT = new DataTable();


    public float _BestResult = 100.0f;
    public List<float> _Results = new List<float>();
    public float _AverageResult = 0.0f;

    public void Start()
    {
        CreateDirFilename();
        InitDataTable();
    }

    public void SubmitRatingValue()
    {
        float sliderMin = _RatingSlider.minValue;
        float sliderMax = _RatingSlider.maxValue;

        float val = _RatingSlider.value;
        float propVal = (val - sliderMin) / (sliderMax - sliderMin);


        float freqA = 
            _randOscVals._GrpA.GetFreq();
        float freqB =
            _randOscVals._GrpB.GetFreq();

        float propReal = freqA / freqB;
        if(propReal>1.0f)
        {
            propReal = 1.0f / propReal;
        }

        float freqUser = propVal;
        if(freqA>freqB)
        {
            freqUser = propVal * freqA;
        }
        else
        {
            freqUser = propVal * freqB;
        }

        DataRow dr = _DT.NewRow();
        dr["Time"] = Time.realtimeSinceStartup;
        dr["FrequencyA"] = freqA;
        dr["FrequencyB"] = freqB;
        dr["Rating_Prop"] = propVal;
        dr["Rating_Real"] = freqUser;

        _DT.Rows.Add(dr);
        //DataTableUtils.PrintDataRow(dr);
        DataTableUtils.PrintDataTable(_DT);

        float dif = Mathf.Abs(propVal - propReal) * 100.0f;
        _Difference.Invoke(dif);
        if(dif<_BestResult)
        {
            _BestResult = dif;
        }
        _Results.Add(dif);
        ComputeAverageResult();
        print("Best:" + _BestResult + " Average:" + _AverageResult);

        _BestResultEvt.Invoke(_BestResult);
        _AverageResultEvt.Invoke(_AverageResult);
        _TestCount.Invoke(_DT.Rows.Count);

        _Submit.Invoke();
    }

    public void ComputeAverageResult()
    {
        float sum = 0.0f;
        for(int i=0;i<_Results.Count;i++)
        {
            sum += _Results[i];
        }
        _AverageResult = sum / _Results.Count;
    }

    public void InitDataTable()
    {
        _DT = new DataTable();
        _DT.Columns.Add("Time", typeof(float));
        _DT.Columns.Add("FrequencyA", typeof(float));
        _DT.Columns.Add("FrequencyB", typeof(float));
        _DT.Columns.Add("Rating_Prop", typeof(float));
        _DT.Columns.Add("Rating_Real", typeof(float));
    }

    private string FileName = "";
    [ContextMenu("SaveData")]
    public void SaveData()
    { 
        bool bSaved = DataTableUtils.SaveDataTable(_DT, FileName);
        print("Save as:" + FileName);
        _SaveFilePath.Invoke(FileName);
        _TestCount.Invoke(_DT.Rows.Count);
    }

    private void CreateDirFilename()
    {
        string DirName = "TestResults";
        bool bDirExist = Directory.Exists(DirName);
        if (!bDirExist)
        {
            Directory.CreateDirectory(DirName);
        }
        FileName = DirName + "/" + _SavePrefix +
            System.DateTime.UtcNow.ToFileTimeUtc().ToString() + ".csv";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private float[] speedSelect = { 0, 0, 0, 0, 0, 0 };
    private float[] speedFact = { 0, 0, 0, 0, 0, 0 };
    private float[] speedSelectOri = { 0, 0, 0, 0, 0, 0 };

    public Slider[] sliders;
    public Text[] numNowText;
    public OscillatorGroup[] groups;
    public GameObject panel;
    public GameObject end;
    public Button nextGroup;
    public Text wuchaTxt;
    public Text wuchabiTxt;
    public float wucha;
    public float wuchabi;

    private float maxSpeedIndex;
    private float maxSpeed;
    public float minLine = 10.0f;
    public float limitLine = 80.0f;
    public float maxLine = 100.0f;
    public int maxCat;
    public int testNum = 10;
    public int nowTestNum = 0;

    void Start()
    {
       
        maxCat = UnityEngine.Random.Range(0, 5);

        maxSpeedIndex = UnityEngine.Random.Range(0, 1.2f);

        groups[maxCat]._FreqExp = maxSpeedIndex;

        maxSpeed = groups[maxCat].GetFreq();

        for (int k = 0; k < speedFact.Length; k++)
        {
            if (k == maxCat)
            {
                speedFact[k] = maxSpeed;
            }
            else
            {
                speedFact[k] = maxSpeed * UnityEngine.Random.Range(0.01f, 1.0f);

            }
        }

        for(int n = 0;n< speedFact.Length; n++)
        {
            groups[n].SetFreq(speedFact[n]);
        }



        //float max = speedFact.Max();

    }

    // Update is called once per frame
    void Update()
    {
        sliders[maxCat].value = 1;
        for (int i = 0;i< sliders.Length; i++)
        {
            numNowText[i].text = sliders[i].value * 100 + "";
        }


    }
    //记录速度存入文件
    public void Click0()
    {
        for(int i = 0; i < speedSelect.Length; i++)
        {
            speedSelect[i] = sliders[i].value * 100;
            //speedFact[i] = groups[i].GetFreq();
            speedSelectOri[i] = speedSelect[i] / 100 * maxSpeed;

        }
        float sum = 0;
        float wuchaFact = 0;

        for (int m = 0;m< speedSelect.Length; m++)
        {
            wuchaFact += Math.Abs(speedSelectOri[m] - speedFact[m]);
            sum += speedFact[m];

        }
        wucha = wuchaFact * 100 / speedFact.Max();
        wuchabi = wuchaFact / (sum - speedFact.Max()) * 100;

        string txtselect = numListToString(speedSelect);
        string txtSpeedFact = numListToString(speedFact);
        string txtSpeedSelectOri = numListToString(speedSelectOri);


        string pathselect = Application.dataPath + "/excel/selectSpeedTest.txt";//选择速度百分值
        string pathSpeedFact = Application.dataPath + "/excel/speedFactTest.txt";//实际速度原值
        string pathSpeedSelectOri = Application.dataPath + "/excel/speedSelectOri.txt";//选择速度原值


        FileInfo fileSelect = new FileInfo(pathselect);
        FileInfo fileSpeedFact = new FileInfo(pathSpeedFact);
        FileInfo fileSpeedSelectOri = new FileInfo(pathSpeedSelectOri);


        StreamWriter sw;
        StreamWriter sw1;
        StreamWriter sw2;


        if (!File.Exists(pathselect))
        {
            sw = fileSelect.CreateText();
            sw1 = fileSpeedFact.CreateText();
            sw2 = fileSpeedSelectOri.CreateText();


        }
        else
        {
            sw = fileSelect.AppendText();   //在原文件后面追加内容    
            sw1 = fileSpeedFact.AppendText();   //在原文件后面追加内容   
            sw2 = fileSpeedSelectOri.AppendText();   //在原文件后面追加内容      


        }
        sw.WriteLine(txtselect);
        sw.Close();
        sw.Dispose();
        sw1.WriteLine(txtSpeedFact);
        sw1.Close();
        sw1.Dispose();
        sw2.WriteLine(txtSpeedSelectOri);
        sw2.Close();
        sw2.Dispose();

        wuchaTxt.text = wucha + "";
        wuchabiTxt.text = wuchabi + "%";

        panel.SetActive(true);

    }
    //下一组按钮
    public void Click1()
    {
        for (int i = 0; i < speedSelect.Length; i++)
        {
            sliders[i].value = 0;
        }

        nowTestNum++;
        if (nowTestNum >= testNum - 1)
        {
            end.SetActive(true);
            nextGroup.enabled = false;
        }
        if (nowTestNum >= testNum)
        {
            end.SetActive(true);
            nextGroup.enabled = false;
            return;
        }
        if (nowTestNum < testNum - 3)
        {
            maxSpeedIndex = UnityEngine.Random.Range(0f, 1.2f);

        }
        else
        {
            maxSpeedIndex = UnityEngine.Random.Range(1.2f, 1.5f);

        }


        maxCat = UnityEngine.Random.Range(0, 5);

        //maxSpeedIndex = UnityEngine.Random.Range(0, 1.5f);

        groups[maxCat]._FreqExp = maxSpeedIndex;

        maxSpeed = groups[maxCat].GetFreq();

        for (int k = 0; k < speedFact.Length; k++)
        {
            if (k == maxCat)
            {
                speedFact[k] = maxSpeed;
            }
            else
            {
                speedFact[k] = maxSpeed * UnityEngine.Random.Range(0.01f, 1.0f);

            }
        }

        for (int n = 0; n < speedFact.Length; n++)
        {
            groups[n].SetFreq(speedFact[n]);
        }



        panel.SetActive(false);




    }

    string numListToString(float[] list)
    {
        string str = "";
        foreach (float n in list)
            str += n + "\t";

        return str;
    }
}

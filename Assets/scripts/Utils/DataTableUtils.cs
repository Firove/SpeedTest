using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;

public class DataTableUtils : MonoBehaviour
{

    public static bool SaveDataTable(DataTable DT, string path)
    {
        string csvString = DataTable2CSV(DT);
       // OOFormArray form = DataTable2OOFormArray(DT);
        //form.SaveFormFile(path);

        // WriteAllText creates a file, writes the specified string to the file,
        // and then closes the file.    You do NOT need to call Flush() or Close().
        File.WriteAllText(path, csvString);

        bool bFileCreated = File.Exists(path);
        if(bFileCreated)
        {
            print("Saved at:" + path);
        }

        return bFileCreated;
       // FileStream FS = new FileStream(path, FileMode.OpenOrCreate);
        
    }

    public static string DataTable2CSV(DataTable dt)
    {
        OOFormArray form = DataTable2OOFormArray(dt);

        string csv = form.ToCSVString();
        print(csv);

        return csv;
    }

    public static bool LoadFromCSVFile(string path, ref DataTable DT)
    {
        int DTColNum = DT.Columns.Count;

        OOFormArray form = new OOFormArray();
        form = OOFormArray.ReadFormCSVFile(path);
        int rowCnt = form.mRowCount;
        int colCnt = form.mColumnCount;
        //print("DTColNum" + DTColNum + " colCnt:" + colCnt + " rowCnt:" + rowCnt);

        if(DTColNum!=colCnt)
        {
            print("Not Match!");
        }

        List<string> Keys = new List<string>();
        bool bSameKeys = true;
        for(int i=0;i<colCnt;i++)
        {
            string key = form.GetString(i,0);

            if (DT.Columns[i].ColumnName != key)
            {
                bSameKeys = false;
                break;
            }
            //print("ColumnName:" + DT.Columns[i].ColumnName + " Key:" + key);
            /* if(DT.Columns[i].ColumnName == key)
            {
                print("Good Key:" + key);
            }*/
           
            Keys.Add(key);
            //print("Key:" + key);
        }
        if(!bSameKeys)
        {
            return false;
        }


        // read data
        for(int r=1;r<rowCnt;r++)
        {
            DataRow dr = DT.NewRow();
            for (int c=0;c<Keys.Count;c++)
            {
                System.Type tp = DT.Columns[c].DataType;
                if (tp == typeof(int))
                {
                    int val = form.GetInt( c,r);
                    dr[c] = val;
                    DebugPrint("Int:" + val);
                    // print("Set Int:" + val.ToString() + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(Vector2))
                {
                    Vector2 val = form.GetVector2(c,r);
                    dr[c] = val;
                    DebugPrint("Vector2:" + val.ToString());
                }
                else if (tp == typeof(Vector3))
                {
                    Vector3 val = form.GetVector3(c, r);
                    dr[c] = val;
                    DebugPrint("Vector3:" + val.ToString());
                }
                else if (tp == typeof(Vector4))
                {
                    Vector4 val = form.GetVector4(c, r);
                    dr[c] = val;
                    DebugPrint("Vector4:" + val.ToString());
                }
                else if (tp == typeof(string))
                {
                    string val = form.GetString(c, r);
                    dr[c] = val;
                    DebugPrint("string:" + val);
                }
                else if (tp == typeof(float))
                {
                    float val = form.GetFloat(c, r);
                    dr[c] = val;
                    DebugPrint("float:" + val.ToString());
                }
                else if (tp == typeof(Rect))
                {
                    Rect val = form.GetRect(c, r);
                    dr[c] = val;
                    DebugPrint("Rect:" + val.ToString());
                }
                else if (tp == typeof(bool))
                {
                    bool val = form.GetBool(c, r);
                    dr[c] = val;
                    DebugPrint("Bool:" + val.ToString());
                }
                
                else
                {
                    DebugPrint("Invalid Datatype");
                }

            }
            DT.Rows.Add(dr);
            //PrintDataTable(DT);

        }
        int DTRowCnt =
                 DT.Rows.Count;
        print("Load DataTable Rows:" + DTRowCnt.ToString());

        return true;
    }

    public static void DebugPrint(string txt, bool bDisp = false)
    {
        if(bDisp)
        {
            print(txt);
        }
    }

    private static OOFormArray DataTable2OOFormArray(DataTable dt)
    {
        OOFormArray form = new OOFormArray();
       // csv = "";
        form = InitForm(dt);
        int colNum = dt.Columns.Count;
        int rowNum = dt.Rows.Count;

        for (int r = 0; r < rowNum; r++)
        {
            var dr = dt.Rows[r];
            for (int c = 0; c < colNum; c++)
            {
                string cName = dt.Columns[c].ColumnName;
                int row = r + 1;
                int col = c;
                System.Type tp = dt.Columns[c].DataType;
                //print(tp);
                if (tp == typeof(int))
                {
                    int val = (int)dr[c];
                    bool bSet = form.SetInt(val, col, row);
                    // print("Set Int:" + val.ToString() + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(Vector2))
                {
                    Vector2 val = (Vector2)dr[c];
                    bool bSet = form.SetVector2(val, col, row);
                    //print("Set Vec2:" + val.ToString() + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(Vector3))
                {
                    Vector3 val = (Vector3)dr[c];
                    bool bSet = form.SetVector3(val, col, row);
                    //print("Set Vec3:" + val.ToString() + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(Vector4))
                {
                    Vector4 val = (Vector4)dr[c];
                    bool bSet = form.SetVector4(val, col, row);
                    //print("Set Vec4:" + val.ToString() + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(string))
                {
                    string val = (string)dr[c];
                    bool bSet = form.SetString(val, col, row);
                    //print("Set String:" + val + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(float))
                {
                    float val = (float)dr[c];
                    bool bSet = form.SetFloat(val, col, row);
                    //   print("Set Float:" + val + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(Rect))
                {
                    Rect val = (Rect)dr[c];
                    bool bSet = form.SetRect(val, col, row);
                    // print("Set Rect:" + val + " OK?" + bSet.ToString());
                }
                else if (tp == typeof(bool))
                {
                    bool val = (bool)dr[c];
                    bool bSet = form.SetBool(val, col, row);
                    //print("Set Bool:" + val + " OK?" + bSet.ToString());
                }
                
                else
                {
                    int val = (int)dr[c];
                    bool bSet = form.SetInt(val, col, row);
                }

            }
        }
        return form;
    }

    private static OOFormArray InitForm(DataTable dt)
    {
        OOFormArray form;
        int colNum = dt.Columns.Count;
        int rowNum = dt.Rows.Count;
        form = new OOFormArray();
        colNum = dt.Columns.Count;
        rowNum = dt.Rows.Count;

        // init form
        for (int i = 0; i < colNum; i++)
        {
            form.InsertColumn(i);
        }
        for (int i = 0; i < rowNum + 1; i++)
        {
            form.InsertRow(i);
        }

        // set column key
        for (int c = 0; c < colNum; c++)
        {
            string cName = dt.Columns[c].ColumnName;
            form.SetString(cName, c, 0);
        }

        return form;
    }


    static public bool GetVec3At(
        DataTable DT,
        string name,
        float idf,
        ref Vector3 V)
    {
        int rowCnt = DT.Rows.Count;
        if(idf>=rowCnt||idf<0.0f)
        {
            return false;
        }

        Vector3 v0 = Vector3.negativeInfinity;
        Vector3 v1 = Vector3.negativeInfinity;
        float id0 = Mathf.Floor(idf);
        GetAt(DT, typeof(Vector3), name, (int)id0, ref v0);
        GetAt(DT, typeof(Vector3), name, Mathf.CeilToInt(idf), ref v1);
        float t = idf - id0;
        V = Vector3.Lerp(v0, v1, t);

        return true;

        /*
        int traceCnt = DT.Rows.Count;
        V = Vector3.negativeInfinity;
        if (idf <= 0.0f || idf >= traceCnt)
        {
            return false;
        }

        bool bVec3 =
                (DT.Columns[name].DataType == typeof(Vector3));
        if (!bVec3)
        {
            return false;
        }

        float id0 = Mathf.Floor(idf);
        Vector3 val = Vector3.negativeInfinity;
        if (Mathf.Approximately(id0, idf))
        {
            val = (Vector3)
                DT.Rows[(int)id0][name];
        }
        else
        {
            int id1 = Mathf.CeilToInt(idf);
            Vector3 val0 = (Vector3)
                DT.Rows[(int)id0][name];
            Vector3 val1 = (Vector3)
                DT.Rows[id1][name];
            float t = idf - id0;
            val = Vector3.Lerp(val0, val1, t);
        }

        V = val;
        */


    }

    static public bool GetFloatAt(
        DataTable DT,
        string name,
        float idf,
        ref float V)
    {
        int rowCnt = DT.Rows.Count;
        if (idf >= rowCnt || idf < 0.0f)
        {
            return false;
        }

        float v0 = float.NegativeInfinity;
        float v1 = float.NegativeInfinity;
        float id0 = Mathf.Floor(idf);
        GetAt(DT, typeof(float), name, (int)id0, ref v0);
        GetAt(DT, typeof(float), name, Mathf.CeilToInt(idf), ref v1);
        float t = idf - id0;
        V = Mathf.Lerp(v0, v1, t);

        return true;

        /*
         * int traceCnt = DT.Rows.Count;
        V = float.NegativeInfinity;
        if (idf <= 0.0f || idf >= traceCnt)
        {
            return false;
        }

        bool bFloat =
                (DT.Columns[name].DataType == typeof(float));
        if (!bFloat)
        {
            return false;
        }

        float id0 = Mathf.Floor(idf);
        float val = float.NegativeInfinity;
        if (Mathf.Approximately(id0, idf))
        {
            val = (float)
                DT.Rows[(int)id0][name];
        }
        else
        {
            int id1 = Mathf.CeilToInt(idf);
            float val0 = (float)
                DT.Rows[(int)id0][name];
            float val1 = (float)
                DT.Rows[id1][name];
            float t = idf - id0;
            val = Mathf.Lerp(val0, val1, t);
        }
        V = val;
        return true;
        */
    }

    static public bool GetAt<T>(
        DataTable DT,
        System.Type dataType,
        string name,
        int id,
        ref T V)
    {
        int rowCnt = DT.Rows.Count;
        bool bOK = IsValidIdType(DT, id, dataType, name);
        if (!bOK)
        {
            return false;
        }

        V = (T)DT.Rows[id][name];
        return true;
    }

    static public bool IsValidIdType(
        DataTable DT,
        int id,
        System.Type dataType,
        string colName)
    {
        int rowCnt = DT.Rows.Count;
        bool bIDOK = (id >= 0 && id < rowCnt);
        bool bSameType =
                (DT.Columns[colName].DataType == dataType);

        bool bOK = bIDOK && bSameType;
        return bOK;
    }

    static public int GetLastRowId(DataTable DT)
    {
        if(DT.Rows.Count==0)
        {
            return -1;
        }
        else
        {
            return DT.Rows.Count - 1;
        }
    }

    static public int GetFirstRowId(DataTable DT)
    {
        if (DT.Rows.Count == 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    static public void PrintDataRow(DataRow dr)
    {
        string Text = "";
        foreach (var obj in dr.ItemArray)
        {
            //Text += dr.
            Text +=  obj.ToString()+ " ";
        }
        print(Text);
    }

    static public void PrintDataTable(DataTable DT)
    {
        print("TableName:" + DT.TableName);
        foreach(DataRow dr in DT.Rows)
        {
            PrintDataRow(dr);
        }
    }


}

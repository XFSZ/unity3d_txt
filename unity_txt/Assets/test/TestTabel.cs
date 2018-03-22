using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.Odbc;  
using System.Data;  
public class TestTable : MonoBehaviour  
{  
    private readonly string FILENAME = " TestTable.xls";  
  
    [System.Serializable]  
    public class TestInfo  
    {  
        public int id;  
        public string content;  
    }  
    public TestInfo[] testInfoList;  
  
  
    private bool tableReload = false;  
    public bool TableReload  
    {  
        set { tableReload = value; }  
        get { return tableReload; }  
    }  
  
    public void Load()  
    {  
        if (Application.platform == RuntimePlatform.WindowsEditor)  
        {  
            ReadInfo();  
            tableReload = true;  
        }  
    }  
  
    public void ReadInfo()  
    {  
        string fileName = PathForDocumentsFile("/Resources/Data/" + FILENAME);  
  
        string con = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790; Dbq=" + fileName + ";";    // 必须保存后缀名是.xls的文件  
        string query = "SELECT * FROM [Sheet1$]";  
        OdbcConnection oCon = new OdbcConnection(con);  
        OdbcCommand oCmd = new OdbcCommand(query, oCon);  
        DataTable dtData = new DataTable("MyData");  
        oCon.Open();  
        OdbcDataReader rData = oCmd.ExecuteReader();  
        dtData.Load(rData);  
        rData.Close();  
        oCon.Close();  
  
        int Count = 0;  
        for (int i = 0; i < dtData.Rows.Count; i++)  
        {  
            int Num = 0;  
            if (int.TryParse(dtData.Rows[i][0].ToString(), out Num))  
            {  
                Count++;  
            }  
        }  
  
        testInfoList = new TestInfo[Count];  
  
        for (int i = 0; i < dtData.Rows.Count; i++)  
        {  
            int Num = 0;  
            if (int.TryParse(dtData.Rows[i][0].ToString(), out Num))  
            {  
                TestInfo Info = new TestInfo();  
                Info.id = int.Parse(dtData.Rows[i][0].ToString());  
                Info.content = dtData.Rows[i][1].ToString();  
                testInfoList[i] = Info;  
            }  
        }  
    }  
  
    public int GetTotalCount()  
    {  
        return testInfoList.Length;  
    }  
  
    public TestInfo GetTestInfo(int id)  
    {  
        foreach (TestInfo Info in testInfoList)  
        {  
            if (Info.id == id) return Info;  
        }  
        return null;  
    }  
  
    public string PathForDocumentsFile(string fileName)  
    {  
        return Application.dataPath + fileName;  
    }  
}  


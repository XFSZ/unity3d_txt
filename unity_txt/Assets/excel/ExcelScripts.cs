using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Text;
using Excel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using SimpleJSON;
using LitJson;


public class ExcelScripts : MonoBehaviour
{
    private string _filepath;
    private string _filename;
    GameObject canve;
    JsonData jsondataname = null;
    GameObject[] buttons;
    DataSet mResultSets;
    private string userName = "";
    string httplist = "";
    string httpimg = "";
    string httpaddress = "";
    bool houzhuiming = true;
    Checkcount checkcount = new Checkcount();
    int Z = -2;
    int X = -5;
    int DX = -5;
    int UX = -5;
    int Y = 10;
    int DY = 10;
    int UY = 10;
    float adpi;
    float w = 0;
    float h = 0;
    float dp;
    float cpz;
void Awake(){

   //  PlayerPrefs.SetString("yes","notyes");
}
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<xuanzhuan>().isexcel = false;
        this.name = "Image";
        canve = GameObject.Find("Canvas");
        adpi = Screen.dpi;
        w = Screen.width;
        h = Screen.height;
        dp = adpi / 160;
        Debug.Log(adpi);
        cpz = canve.transform.position.z;
        canve.GetComponent<CanvasScaler>().referencePixelsPerUnit = adpi;
       // Debug.Log(PlayerPrefs.GetInt("1"));
        PC_objectinit();
        string yesui = PlayerPrefs.GetString("yes");
        // if(PlayerPrefs.GetInt("1")!=0){
        // jsondataname = PlayerPrefs.GetString("jsondata");
        // Android_openread();
        // deletebutton();
        // }  
      //  Debug.Log("this is1"+PlayerPrefs.GetString("yes"));
        if(yesui=="yes"){
   //     Debug.Log("this is2"+PlayerPrefs.GetString("yes"));
        string  jsondatan = PlayerPrefs.GetString("jsondata");
        jsondataname = JsonMapper.ToObject(jsondatan);
   //     Debug.Log("this is json "+jsondataname.Count);
        Android_openread();
  //      Debug.Log("this is6"+ PlayerPrefs.GetString("wordhttp"));
        deletebutton();
        PlayerPrefs.SetString("yes","notyes");
        }
    }
    void Update(){
        


    }

    void OnGUI()
    {
        //  string s = string.Format("In = {0}\n name={1}", _filepath, _filename);
        //   GUI.TextArea(new Rect(0, 0, Screen.width * 2 / 10, Screen.height * 2 / 10), s);
        userName = GUI.TextField(new Rect(Screen.width - 200, 60, 200, 20), userName);//15为最大字符串长度
       
        if (GUI.Button(new Rect(Screen.width - 100, 90, 80, 20), "重置"))
        {
            
            deletebutton();
            PC_objectinit();
            httpimg = "http://" + userName + ":8888";
          //  GameObject.Find("Main Camera").GetComponent<xuanzhuan>().isexcel = false;
        }
       // if (GUI.Button(new Rect(Screen.width - 280, 80, 50, 20), "文本"))
     //   {
     //       httpimg = "http://" + userName + ":8888";
       //     GameObject.Find("Main Camera").GetComponent<xuanzhuan>().isexcel = false;
     //       deletebutton();
    //        PC_objectinit();
    //    }

    }

    // 各种button的初始化
    //初始按钮 打开文件用的 并阅读sheet
    void objectinit()
    {

        GameObject obj = (GameObject)Instantiate(Resources.Load("Button"));   //实例化按钮
        obj.transform.SetParent(canve.transform, false);
        obj.transform.position = new Vector3(adpi / (Screen.width / 5), adpi / (Screen.height / 2), cpz);
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("android");
            obj.GetComponent<Button>().onClick.AddListener(Android_openfile);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            obj.GetComponent<Button>().onClick.AddListener(PC_openfile);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            obj.GetComponent<Button>().onClick.AddListener(PC_openfile);  //点击按钮 打开文件
        }
    }

    void PC_objectinit()
    {

        Debug.Log("jieba");
        GameObject obj = (GameObject)Instantiate(Resources.Load("Button"));   //实例化按钮
        obj.transform.SetParent(canve.transform, false);
        canve.GetComponent<CanvasScaler>().referencePixelsPerUnit = adpi;
        obj.transform.position = new Vector3((adpi / Screen.width) / 5, adpi / (Screen.height / 2), cpz);
        if (Application.platform == RuntimePlatform.Android)
        {
            
            obj.GetComponent<Button>().onClick.AddListener(Android_openfile);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            
            obj.GetComponent<Button>().onClick.AddListener(PC_openfile2);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            
            obj.GetComponent<Button>().onClick.AddListener(PC_openfile2);  //点击按钮 打开文件
        }
    }
    // 打开sheet 用的  并阅读 表头
    void sheetbuttoninit(int sheetCount, string sheetname, float x, float y)
    {

        //Debug.Log(sheetCount); Debug.Log(sheetname); Debug.Log(x); Debug.Log(y);
        GameObject obj = (GameObject)Instantiate(Resources.Load("Button"));   //实例化按钮
        obj.transform.SetParent(canve.transform, false);
        obj.transform.position = new Vector3((adpi / w) * 160 * x, (adpi / h) * y, cpz);
        obj.GetComponentInChildren<Text>().text = sheetname;
        obj.gameObject.name = sheetCount.ToString();
        obj.AddComponent<getname>().k = sheetCount;

    }
    //打开 某一列 用的
    public void rowbuttoninit(int sheetCount, int rowCount, string rowname, float x, float y)
    {

        GameObject obj = (GameObject)Instantiate(Resources.Load("Button"));   //实例化按钮
        obj.transform.SetParent(canve.transform, false);

        obj.transform.position = new Vector3((adpi / w) * 160 * x, (adpi / h) * y, cpz);
        obj.GetComponentInChildren<Text>().text = rowname;
        obj.gameObject.name = rowCount.ToString();
        obj.AddComponent<getcolumn>().sheetcount = sheetCount;
        obj.GetComponent<getcolumn>().columncount = rowCount;
    }


    void FileRead(OpenFileName ofn)      //打开PC端文件
    {

        FileStream m_Stream = File.Open(ofn.file, FileMode.Open, FileAccess.Read);
        readexcelfile(m_Stream);
        //readsheet();//读取行数

    }

    IEnumerator Android_file_Update(string excelfilepath)
    {
         
        // 使用WWWForm，往数据库添加数据
        WWW www = new WWW("file://" + excelfilepath);
        yield return www;
        WWWForm form = new WWWForm();

        string wjm = excelfilepath.Substring(excelfilepath.LastIndexOf("/") + 1);  //从路径中获取名字
        string HZM = excelfilepath.Substring(excelfilepath.LastIndexOf(".") + 1);
        if (HZM == "txt")
        {
            houzhuiming = true;
        }
        else
        {
            houzhuiming = false;
        }
        _filename = wjm;
        form.AddBinaryData("excelfile", www.bytes, wjm, "multipart/form-data");

        // 使用WWW上传form的内容
        WWW w = new WWW("http://" + userName + ":8888" + "/file_upload", form);
        httpimg = "http://" + userName + ":8888";
        yield return w;

        if (w.error != null)
            _filepath = w.error;
        else
        {
            _filepath = "Finished Uploading Screenshot";
            StartCoroutine("Android_file_down");
        }
    }
    IEnumerator Android_file_down()     // 下载json
    {
  
        string name;
        if (houzhuiming == true)
        {
            name = "/txtlist";

        }
        else
        {
            name = "/list";
        }
        yield return new WaitForSeconds(8);
        WWW www = new WWW(httpimg + name);           
        yield return www;  
      //  Debug.Log(www.text);      
        if (www.text == null){
          www = new WWW(httpimg + name);
     //     Debug.Log(www.text);
        }
        
        yield return www;

        jsondataname = JsonMapper.ToObject(www.text);
        PlayerPrefs.SetString("jsondata",www.text);
        //读完之后执行下一步
        if (houzhuiming == true)
        {
            Android_openread();
            deletebutton();
          
        }
        else
        {
            Android_openreadsheet();
        }

    }

    public void Android_openread()    // android端读取json
    {

        string sheetname;
        string sheetplace;
        float maxcount=1f;
        for (int sheetCount = 0; sheetCount < jsondataname.Count; sheetCount++)
        {
 
           if(sheetCount>=3){
            sheetname = jsondataname[sheetCount]["name"].ToString();
            sheetplace = jsondataname[sheetCount]["count"].ToString();
            if(sheetCount==3){
              maxcount = int.Parse( sheetplace);
            }
            if(int.Parse( sheetplace)>192){
            LoadImagen(int.Parse( sheetplace)/maxcount*90, sheetname, sheetplace, 2);
            }
             if(int.Parse( sheetplace)<151  ){
            LoadImagen(int.Parse( sheetplace)/maxcount*140, sheetname, sheetplace, 2);
            }
           }
        }
    }
    public void LoadImagen(float sheetCount, string sheetname, string x, float y)
    {
        if(sheetCount<28)
        {
            sheetCount =28;
        }
        X++;
        GameObject obj = (GameObject)Instantiate(Resources.Load("3dtext"));   //实例化按钮
         // obj.transform.SetParent(canve.transform, false);
        obj.transform.position = new Vector3(adpi / (Screen.width) * X * 100, adpi / (Screen.height) * Y * 10, Z);
        obj.GetComponent<TextMesh>().text = sheetname + " : " + x;
     
        obj.GetComponent<TextMesh>().fontSize = (int)sheetCount;
        obj.name = sheetname;
        obj.AddComponent<BoxCollider>();
      
        if (X >= -DX)
        {
            X = UX;
            Y--;        
        }
        if(Y==-UY){
           Z+=10;
           DY +=4;
           UY-=4;
           Y = DY;
           UX -=1;
           X = UX;
           DX +=1;
        }
        

    }


    void readexcelfile(FileStream m_Stream)       //读取excel文件并转成dataset
    {
        //使用OpenXml读取Excel文件
        IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(m_Stream);
        //  IExcelDataReader mExcelReader1 = ;
        //将Excel数据转化为DataSet
        mResultSets = mExcelReader.AsDataSet();
        //读完之后执行下一步
        openreadsheet();
    }
    void deletebutton()           //删除button
    {

        buttons = GameObject.FindGameObjectsWithTag("button");
        foreach (GameObject btn in buttons)
        {
            Destroy(btn);
        }
    }
        void deleteText3d()           //删除Text3d
    {

        buttons = GameObject.FindGameObjectsWithTag("img");
        foreach (GameObject btn in buttons)
        {
            Destroy(btn);
        }
    }
    public void openreadsheet()    // PC端读取表
    {
        deletebutton();
        string sheetname;
        float sheetplace = 1;
        int a = 0;
        int b = Convert.ToInt32(adpi);
        Debug.Log(b);
        for (int sheetCount = 0; sheetCount < mResultSets.Tables.Count; sheetCount++)
        {
            a++;
            if (a == 8)
            {
                b -= 40;
                a = 0;
                sheetplace = 1;
            }
            sheetname = mResultSets.Tables[sheetCount].TableName;
            sheetbuttoninit(sheetCount, sheetname, sheetplace - 4, b);
            sheetplace++;
            //        Debug.Log(sheetplace);
        }
    }
    public void Android_openreadsheet()    // android端读取json
    {
        deletebutton();
        string sheetname;
        float sheetplace = 1;
        int a = 0, b = Convert.ToInt32(adpi);
        for (int sheetCount = 0; sheetCount < jsondataname.Count; sheetCount++)
        {
            a++;
            if (a == 8)
            {
                b -= 40;
                a = 0;
                sheetplace = 1;
            }
            sheetname = jsondataname[sheetCount]["name"].ToString();
            sheetbuttoninit(sheetCount, sheetname, sheetplace - 4, b);
            sheetplace++;

        }
    }
    public void openfirstRow(int sheetCount)   //读取表头数据
    {
        deletebutton();
        string firstRowname;
        float firstRowplace = 1;
        int a = 0, b = Convert.ToInt32(adpi);
        int ColumnCount = mResultSets.Tables[sheetCount].Columns.Count;
        for (int j = 0; j < ColumnCount; j++)
        {
            a++;
            if (a == 8)
            {
                b -= 40;
                a = 0;
                firstRowplace = 1;
            }
            firstRowname = mResultSets.Tables[sheetCount].Rows[0][j].ToString();
            rowbuttoninit(sheetCount, j, firstRowname, firstRowplace - 4, b);
            firstRowplace++;
            //     Debug.Log("sheetCount:"+sheetCount);  //调试

        }
    }

    public void Android_openfirstRow(int sheetCount)   //读取表头数据
    {
        deletebutton();
        string firstRowname;
        float firstRowplace = 1;
        int a = 0, b = Convert.ToInt32(adpi);
        int ColumnCount = jsondataname[sheetCount]["data"][0].Count;
        _filename = jsondataname[sheetCount]["data"][0].Count.ToString();
        for (int j = 0; j < ColumnCount; j++)
        {
            a++;
            if (a == 8)
            {
                b -= 40;
                a = 0;
                firstRowplace = 1;
            }
            firstRowname = jsondataname[sheetCount]["data"][0][j].ToString();
            _filename = firstRowname;
            rowbuttoninit(sheetCount, j, firstRowname, firstRowplace - 4, b);
            firstRowplace++;
        }
    }
    public void openXcloumn(int sheetCount, int ColumnsCount)           //读取某一列值
    {
        List<double> cloumuarr = new List<double>();
        deletebutton();
        int rowCount = mResultSets.Tables[sheetCount].Rows.Count;
        Debug.Log(rowCount);
        for (int i = 1; i < rowCount; i++)
        {
            if (mResultSets.Tables[sheetCount].Rows[i][ColumnsCount].GetType() == Type.GetType("System.Double"))
            {
                cloumuarr.Add((double)mResultSets.Tables[sheetCount].Rows[i][ColumnsCount]);
            }
        }
        calculatevalue(cloumuarr);
    }
    public void Android_openXcloumn(int sheetCount, int ColumnsCount)           //读取某一列值
    {
        List<double> cloumuarr = new List<double>();
        deletebutton();
        int rowCount = jsondataname[sheetCount]["data"].Count;
        _filename = rowCount.ToString();

        Debug.Log(rowCount);
        for (int i = 1; i < rowCount; i++)
        {

            _filename = jsondataname[sheetCount]["data"][i][ColumnsCount].ToString();
            // if(IsNumber(vlaue) == true){
            cloumuarr.Add(Double.Parse(jsondataname[sheetCount]["data"][i][ColumnsCount].ToString()));

            //   } 
        }
        calculatevalue(cloumuarr);
    }
    public static bool IsNumber(String str)          //判断是否为数字
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!Char.IsNumber(str, i))
                return false;
        }
        return true;
    }
    void calculatevalue(List<double> arru)   //  计算占比
    {
        //  _filename = "jisuanzhi";

        double[] arr;
        arr = arru.ToArray();
        _filepath = arr.Length.ToString();
        if (arr != null)
        {
            checkcount.Main(arr);
            Debug.Log("firstcount: " + arr.Length);
        }
        // 分组中第一个组就是重复最多的
    }

    public void Android_openfile()           //调用android  
    {
             PlayerPrefs.SetString("wordhttp",userName);
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenFile");

    }
    public void GetFilePath(string str)         //安卓上传文档
    {
        StartCoroutine(Android_file_Update(str));
    }
    void PC_openfile()  //打开文件
    {
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "Excel files(*.xlsx)|*.xlsx";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
        ofn.title = "打开文件";
        ofn.defExt = "xlsx";//显示文件的类型
                            //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
        if (WindowDll.GetOpenFileName(ofn))    //windows平台下的文件调出窗口
        {
            FileRead(ofn);
            Debug.Log("Selected file with full path: {0}" + ofn.file);
        }
    }
    void PC_openfile2()  //打开文件
    {
         PlayerPrefs.SetString("wordhttp",userName);
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        //   ofn.filter = "Excel files(*.xlsx)|*.xlsx";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
        ofn.title = "打开文件";
        //   ofn.defExt = "xlsx";//显示文件的类型
        //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
        if (WindowDll.GetOpenFileName(ofn))    //windows平台下的文件调出窗口
        {
            StartCoroutine(Android_file_Update(ofn.file));
            //   FileRead(ofn);
            Debug.Log("Selected file with full path: {0}" + ofn.file);
        }
    }

}


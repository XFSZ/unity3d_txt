using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.IO;
using System.Text;
using SimpleJSON;
using System;
using LitJson;

public class tupian : MonoBehaviour
{
    string n = "";
    string fie = "";
    string _filepath = "";
    GameObject canve;
     private string userName = "";
    string httplist = "";
    string httpimg = "";
    string httpaddress = "";
    bool houzhuiming = true;
    
    private string _filename;
    int I = -12;
    int ce = 0;
    int X = -5;
    int Y = -2;
  
 JsonData jsondataname = null;
    // Use this for initialization
    public void Start()
    {
        this.name = "Image";
        canve = GameObject.Find("Canvas");
        objectinit();

    }
 void OnGUI()
    {
        string s = string.Format("In = {0}\n name={1}", _filepath, _filename);
        GUI.TextArea(new Rect(0, 0, Screen.width * 2 / 10, Screen.height * 2 / 10), s);
        userName = GUI.TextField(new Rect(Screen.width - 200, 20, 200, 20), userName);//15为最大字符串长度
        if (GUI.Button(new Rect(Screen.width - 80, 80, 50, 20), "重开"))
        {
            
              objectinit();
             httpimg = "http://" + userName + ":8888";

        }
         if (GUI.Button(new Rect(Screen.width - 280, 80, 50, 20), "文本"))
        {
            
             objectinit();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }

  public void Android_openfile()           //调用android  
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenFile");

    }
        void objectinit()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load("Button"));   //实例化按钮
        obj.transform.SetParent(canve.transform, false);
        obj.transform.position = new Vector3(Screen.width / 5, Screen.height / 2, 0);
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("android");
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
    void PC_openfile2()  //打开文件
    {
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
    public void AndroidSaveHeadpath(string str)
    {
        _filepath = str;
        StartCoroutine("LoadImagejs", str);

    }
    IEnumerator Android_file_Update(string excelfilepath)
    {
        // 使用WWWForm，往数据库添加数据
        WWW www = new WWW("file://" + excelfilepath);
        yield return www;
        WWWForm form = new WWWForm();

        string wjm = excelfilepath.Substring(excelfilepath.LastIndexOf("/") + 1);  //从路径中获取名字
        string HZM =  excelfilepath.Substring(excelfilepath.LastIndexOf(".") + 1);
        if(HZM =="txt"){
           houzhuiming =  true;
        }else{
            houzhuiming =  false;
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
        if(houzhuiming ==true ){
            name = "/txtlist";
           
        }else{
            name = "/list";
        }  
        WWW www = new WWW(httpimg + name);
        yield return www;
        _filepath = www.text;
        jsondataname = JsonMapper.ToObject(www.text);
        //读完之后执行下一步
        Android_openreadsheet();
    }
       public void Android_openreadsheet()    // android端读取json
    {
      
        string sheetname;
        float sheetplace = 1;
        for (int sheetCount = 0; sheetCount < jsondataname.Count; sheetCount++)
        {  
            sheetname = jsondataname[sheetCount]["name"].ToString();
            sheetplace++;
        }
    }
    public IEnumerator LoadImagen(string imagePath)
    {
        WWW www = new WWW("file://" + imagePath);
        yield return www;

        if (www.error == null)
        {
            n = imagePath;
            Texture2D tup = www.texture;
            tup.Compress(false);
            Sprite sprite = Sprite.Create(tup, new Rect(0, 0, tup.width, tup.height), new Vector2(0.5f, 0.5f));          
          //  tup = null;
            GameObject jiushitu = (GameObject)Resources.Load("timg");
            //    DestroyImmediate(jiushitu.GetComponent<Image>().sprite.texture,false);   
            jiushitu.GetComponent<SpriteRenderer>().sprite = sprite;
            float x =   jiushitu.GetComponent<BoxCollider>().bounds.size.x;
            float y =   jiushitu.GetComponent<BoxCollider>().bounds.size.y;
            jiushitu.gameObject.GetComponent<BoxCollider>().size =  (new Vector3(  6, 12,0.2f));
         //   Bounds bounds1 = new Bounds() 
             tup = null;
            GameObject op = Instantiate(jiushitu, new Vector3(X * 15f, Y * 15f, 0f), Quaternion.identity);
            op.gameObject.name = op.gameObject.name + ce;
            ce++;
            if (X >= 6)
            {
                X = -6;
                Y++;
            }
            X++;
            I++;
        }
        else { n = "wanle"; Debug.LogError("LoadImage>>>www.error:" + www.error); }
    }



}

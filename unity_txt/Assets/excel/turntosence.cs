using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using SimpleJSON;
using LitJson;
public class turntosence : MonoBehaviour {
     public InputField inputText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 public   void turnsecen(){
         PlayerPrefs.SetString("wordname",this.transform.parent.name);
         SceneManager.LoadScene(2);
    }
		 public   void mainturnscene(){
			 PlayerPrefs.SetString("wordhttp",inputText.GetComponent<InputField>().text);
			 Debug.Log(inputText.GetComponent<InputField>().text);
         //PlayerPrefs.SetString("wordname",this.transform.parent.name);
       //  SceneManager.LoadScene(1);
		 searchandselectfile();
		
    }
	public void searchandselectfile()
	{
  if (Application.platform == RuntimePlatform.Android)
        {
            
            Android_openfile();  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            
            PC_openfile2();  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            
           PC_openfile2();  //点击按钮 打开文件
        }
	}
	    public void Android_openfile()           //调用android  
    {
             PlayerPrefs.SetString("wordhttp",inputText.GetComponent<InputField>().text);
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenFile");

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
          //  houzhuiming = true;
        }
        else
        {
          //  houzhuiming = false;
        }
       // _filename = wjm;
        form.AddBinaryData("excelfile", www.bytes, wjm, "multipart/form-data");

        // 使用WWW上传form的内容
        WWW w = new WWW("http://" + inputText.GetComponent<InputField>().text + ":8888" + "/file_upload", form);
      string  httpimg = "http://" + inputText.GetComponent<InputField>().text + ":8888";
        yield return w;

        if (w.error == null)
        {  //  _filepath = w.error;
      //  else
     //   {
           // _filepath = "Finished Uploading Screenshot";
           // StartCoroutine("Android_file_down");
			SceneManager.LoadScene(1);
        }
    }
    IEnumerator Android_file_down()     // 下载json
    {
  string  httpimg = "http://" + inputText.GetComponent<InputField>().text + ":8888";
        string name;
      //  if (houzhuiming == true)
      //  {
            name = "/txtlist";

      //  }
     //   else
      //  {
     //       name = "/list";
     //   }
        yield return new WaitForSeconds(8);
        WWW www = new WWW(httpimg + name);           
        yield return www;  
      //  Debug.Log(www.text);      
        if (www.text == null){
          www = new WWW(httpimg + name);
     //     Debug.Log(www.text);
        }
        
        yield return www;
        JsonData jsondataname = null;
        jsondataname = JsonMapper.ToObject(www.text);
        PlayerPrefs.SetString("jsondata",www.text);
		SceneManager.LoadScene(1);
        //读完之后执行下一步
      //  if (houzhuiming == true)
     //   {
         //   Android_openread();
        //    deletebutton();
          
      //  }
      //  else
     //   {
       //     Android_openreadsheet();
      //  }
	}
	    public void GetFilePath(string str)         //安卓上传文档
    {
        StartCoroutine(Android_file_Update(str));
    }
	 
    void PC_openfile2()  //打开文件
    {
         PlayerPrefs.SetString("wordhttp",inputText.GetComponent<InputField>().text);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getcolumn : MonoBehaviour {

	   public	int sheetcount ;
	   public int columncount; 

	public void OnClickButton()
    {
       // k = this.gameObject.GetComponentInChildren<Text>().text;
       // Debug.Log("已经成功监听按钮的点击事件");
  //      Debug.Log(sheetcount);
//		   Debug.Log(columncount);
 if (Application.platform == RuntimePlatform.Android)
        {
            
			   GameObject.Find("Image").GetComponent<ExcelScripts>().Android_openXcloumn(sheetcount,columncount);
        //    GameObject.Find("Image").GetComponent<ExcelScripts>().Android_openfirstRow(k);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
			   GameObject.Find("Image").GetComponent<ExcelScripts>().openXcloumn(sheetcount,columncount);
       //    GameObject.Find("Image").GetComponent<ExcelScripts>().openfirstRow(k);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
			   GameObject.Find("Image").GetComponent<ExcelScripts>().openXcloumn(sheetcount,columncount);
         //  GameObject.Find("Image").GetComponent<ExcelScripts>().openfirstRow(k);  //点击按钮 打开文件
        }
	 //  GameObject.Find("Image").GetComponent<ExcelScripts>().openXcloumn(sheetcount,columncount);
    }
	public void addlistener(){
		this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickButton);
	}
	// Use this for initialization
	void Start () {
		addlistener();
	}
	
}

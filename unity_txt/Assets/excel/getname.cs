using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getname : MonoBehaviour {
      public	int k ;

	public void OnClickButton()
    {
       // k = this.gameObject.GetComponentInChildren<Text>().text;
       // Debug.Log("已经成功监听按钮的点击事件");
        Debug.Log(k);
		 if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("android");
            GameObject.Find("Image").GetComponent<ExcelScripts>().Android_openfirstRow(k);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
           GameObject.Find("Image").GetComponent<ExcelScripts>().openfirstRow(k);  //点击按钮 打开文件
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
           GameObject.Find("Image").GetComponent<ExcelScripts>().openfirstRow(k);  //点击按钮 打开文件
        }
	 //  GameObject.Find("Image").GetComponent<ExcelScripts>().openfirstRow(k);
    }
	public void addlistener(){
		this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickButton);
	}
	// Use this for initialization
	void Start () {
		addlistener();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class androidsearchfile : MonoBehaviour {
    private string _filepath;

    // Use this for initialization
    void Start () {
		
	}
	    public void OnFile()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenFile");

    }
	// Update is called once per frame
	void Update () {
		
	}  
	    public void GetFilePath(string str)
    {
       _filepath = str;
       // StartCoroutine("LoadImagejs", str);

    }
	void OnGUI()
    {
        string s = string.Format("In = {0}", _filepath);
        GUI.TextArea(new Rect(0, 0, Screen.width *2/ 10, Screen.height* 2/ 10), s);
       

       
    }


}

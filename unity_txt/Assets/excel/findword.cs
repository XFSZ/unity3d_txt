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


public class findword : MonoBehaviour {
        public InputField inputtext;
		 JsonData jsondataname = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void findwordwithtext(){
	string httpsearch = 	"http://" + "127.0.0.1" + ":8888" + "/get/"+ inputtext.text;
   
      StartCoroutine(httpsearchwithtext(httpsearch));
	}
	IEnumerator httpsearchwithtext(string s){
		 WWW www = new WWW(s );
                  
        yield return www; 
		 yield return new WaitForSeconds(3);
		  string  httpimg = "http://" + "127.0.0.1" + ":8888" + "/wordlist";
		 StartCoroutine(httpgainwithdata(httpimg)); 
	}
	IEnumerator httpgainwithdata(string s){
		 WWW www = new WWW(s ); 
           yield return www;
		// string jsonData = System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
		 //JsonData _json = JsonMapper.ToObject(versionMessage)["Diet"];  
		//   jsondataname = JsonMapper.ToObject(jsonData);
		jsondataname = JsonMapper.ToObject(www.text);
		   Debug.Log(www.text);
		    Debug.Log(jsondataname[0]["name"].ToString());
	}
	 void deleteText3d()           //删除Text3d
    {

      GameObject []  buttons = GameObject.FindGameObjectsWithTag("img");
        foreach (GameObject btn in buttons)
        {
            Destroy(btn);
        }
    }
}

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
using UnityEngine.SceneManagement;

public class findword : MonoBehaviour
{
    // public  string inputtext = null;
    JsonData jsondataname = null;
	float t1;  
    float t2; 
    public GameObject objfather;
    // Use this for initialization
    void Start()
    {
        //	inputtext = this.gameObject.name;
        findwordwithtext();
    }

    // Update is called once per frame
    void Update()
    {
		 Debug.Log("this is"+PlayerPrefs.GetString("yes"));
        if(Input.GetMouseButtonDown(0)){    
            t2 = Time.realtimeSinceStartup;    
            if(t2 - t1 < 0.2){  
				  
                 SceneManager.LoadScene(0);
            }    
            t1 = t2;    
        }    
    }
    public void findwordwithtext()
    {
		PlayerPrefs.SetString("yes","yes");
        //	string wordname = "社会";
        string wordname = PlayerPrefs.GetString("wordname");
        //	string wordhttp  = "127.0.0.1";
        string wordhttp = PlayerPrefs.GetString("wordhttp");
        Debug.Log(wordhttp);
        Debug.Log(wordname);
        string httpsearch = "http://" + wordhttp + ":8888" + "/get/" + wordname;
        StartCoroutine(httpsearchwithtext(httpsearch));
    }
    IEnumerator httpsearchwithtext(string s)
    {
        WWW www = new WWW(s);
        string wordhttp = PlayerPrefs.GetString("wordhttp");
        //	string wordhttp  = "127.0.0.1";
        yield return www;
        yield return new WaitForSeconds(6);
        string httpimg = "http://" + wordhttp + ":8888" + "/wordlist";
        StartCoroutine(httpgainwithdata(httpimg));
    }
    IEnumerator httpgainwithdata(string s)
    {
        WWW www = new WWW(s);
        yield return www;
        jsondataname = JsonMapper.ToObject(www.text);
		GameObject objname = (GameObject)Instantiate(Resources.Load("WordText"));
		objname.GetComponent<Text>().text = PlayerPrefs.GetString("wordname");
		objname.GetComponent<Text>().fontSize = 24;
		objname.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        objname.transform.SetParent(objfather.transform);
        for (int sheetCount = 0; sheetCount < jsondataname.Count; sheetCount++)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("WordText"));
            string sheetname = jsondataname[sheetCount]["name"].ToString();
			string sheetcount = jsondataname[sheetCount]["count"].ToString();
            //  sheetplace = jsondataname[sheetCount]["count"].ToString();
            obj.GetComponent<Text>().text = sheetcount+" 、"+ sheetname;
            obj.transform.SetParent(objfather.transform);
        }
        // Debug.Log(jsondataname[0]["name"].ToString());
    }
    void deleteText3d()           //删除Text3d
    {

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("img");
        foreach (GameObject btn in buttons)
        {
            Destroy(btn);
        }
    }
}

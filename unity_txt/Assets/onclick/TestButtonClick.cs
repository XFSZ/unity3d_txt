using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonClick : MonoBehaviour {
   string k ;
  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnClickButton()
    {
        k = this.gameObject.GetComponentInChildren<Text>().text;
        Debug.Log("已经成功监听按钮的点击事件");
        Debug.Log(k);
    }
}

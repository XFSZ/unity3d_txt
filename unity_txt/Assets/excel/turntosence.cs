using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class turntosence : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 public   void turnsecen(){
         PlayerPrefs.SetString("wordname",this.transform.parent.name);
         SceneManager.LoadScene(1);
    }
}

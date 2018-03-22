using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour {

	// Use this for initialization
	float time  = 0;
	int  times  = 0;
	bool quit = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
           quit = !quit;
		   times += 1; 	 
		   if(time<1&&times == 2){
            Application.Quit();
		   }			
		}
		  if(time>1){
           time  = 0;
		   quit = true;
           times = 0;
		   }
		if(quit ==false){
			time += Time.deltaTime;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class countvalue : MonoBehaviour {
      GameObject cube;
	  GameObject canve;
	  float cpz;

//	  GameObject test;
//	float heigh = Screen.height/10;
	GameObject copycube;
 //int beishu = 30;
	// Use this for initialization
	void Awake(){
   //  canve = GameObject.Find("Canvas");
	}
	void Start () {
		 canve = GameObject.Find("Canvas");
		 cpz = canve.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

public 	void copyinit(float scaley ,float positionx,string gameObjectname){
	//	copycube =Instantiate(cube);
	cube =(GameObject)(Resources.Load("Cube"));
	//test =(GameObject)(Resources.Load("Text"));
	Debug.Log((float)scaley);
	copycube = Instantiate(cube);
	copycube.AddComponent<donghua>().zhi = scaley;
	copycube.GetComponent<donghua>().xposition = positionx;
	//copycube.transform.SetParent(GameObject.Find("Canvas").transform, false);
	copycube.GetComponentInChildren<TextMesh>().text = gameObjectname;
    copycube.transform.position = new Vector3(positionx,0 ,cpz);
	copycube.gameObject.name = gameObjectname;
//	 copycube.transform.localScale += (new Vector3(1,(float)scaley,1) -copycube.transform.localScale)*Time.deltaTime ;
  //  StartCoroutine(caton(copycube,scaley));
     

	}
	IEnumerator caton(GameObject copycube,float scaley){
    copycube.transform.localScale += (new Vector3(1,(float)scaley,1) -copycube.transform.localScale)*Time.deltaTime ;
		yield return null;

	}

}

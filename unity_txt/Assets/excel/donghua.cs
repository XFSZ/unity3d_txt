using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donghua : MonoBehaviour {
 public float zhi = -1;
 public float xposition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(zhi != -1){
			donghuachuanzhi();
		}
	}
public	void donghuachuanzhi(){
     StartCoroutine(caton(zhi));
	}
		IEnumerator caton(float scaley){
			
    transform.localScale += (new Vector3(1,(float)scaley,1) -transform.localScale)*Time.deltaTime*0.5f ;
//	transform.position += new Vector3(0,(scaley -transform.localScale.y)/2-8 ,0)*Time.deltaTime*0.5f;
//	if((transform.localScale.y- scaley<0.1f)){
//		zhi = -1;
//	}
		yield return null;

	}

}

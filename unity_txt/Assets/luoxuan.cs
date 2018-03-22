using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luoxuan : MonoBehaviour {

 public GameObject circleModel;
      //旋转改变的角度
      public int changeAngle = 0;
     //旋转一周需要的预制物体个数
     private int count;

     private float angle = 0;
     public float r = 5;
 
     // Use this for initialization
     void Start()
     {
         count = (int)360 / changeAngle;
         for (int i = 0; i < count; i++)
         {
             Vector3 center = circleModel.transform.position;
             GameObject cube = (GameObject)Instantiate(circleModel);
             float hudu = (angle / 180) * Mathf.PI;
             float xx = center.x + r * Mathf.Cos(hudu);
             float yy = center.y + r * Mathf.Sin(hudu) ;
			 float zz = (3*i-1)/10;          //(1/(i+1)+i);
             cube.transform.position = new Vector3(xx, yy, zz);
             cube.transform.LookAt(center);
             angle += changeAngle;
        }
     }
}

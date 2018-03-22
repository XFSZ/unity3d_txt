using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class tuxing : MonoBehaviour
{
    int i = 0, j = 0, k = 0;
    GameObject[] box;
    bool sj;
    bool biaozhun;
    bool circl;
    float size = 45f;
    Vector3[] positi;
    
    Transform[] kids;
    bool isfirst = true;
    public Texture biao;
    public Texture random;
    public Texture cirlc;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteAll();
        isfirst = true;
        biaozhun = false;
        circl = false;
        sj = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void sijiweizhi()
    {
        if(isfirst == true ){
        positi = new Vector3[(int)box.Length];
        for (int i = 0; i < box.Length; i++)
        { positi[i] = new Vector3(Random.Range(-60, 60), Random.Range(-30, 30), Random.Range(0, 90)); }
        for (int i = 0; i < box.Length; i++)
        {
            PlayerPrefs.SetFloat(box[i].transform.name + "1", box[i].transform.position.x);
            PlayerPrefs.SetFloat(box[i].transform.name + "2", box[i].transform.position.y);
            PlayerPrefs.SetFloat(box[i].transform.name + "3", box[i].transform.position.z);
        }
        }
    }
    void LateUpdate()
    {

        //	cu.transform.position += (hu.transform.position - cu.transform.position)*Time.deltaTime;

        if (sj == true) { suiji(); }
        if (circl == true) { CalualteSphere(); }
        if (biaozhun == true) { putong(); }

    }
    void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        GUI.DrawTexture(new Rect(Screen.width * 1 / 10-60, Screen.height * 1 / 10-55, 200, 160), biao);
        GUI.DrawTexture(new Rect(Screen.width * 2 / 10 - 60, Screen.height * 1 / 10 - 55, 200, 160), random);
        GUI.DrawTexture(new Rect(Screen.width * 3 / 10 - 60, Screen.height * 1 / 10 - 55, 200, 160), cirlc);
        if (GUI.Button(new Rect(Screen.width*1/10,Screen.height*1/10,80,80),""))
        {box = GameObject.FindGameObjectsWithTag("img");sijiweizhi();biaozhun = true; circl = false;sj = false; k = 0; isfirst = false;}
        if (GUI.Button(new Rect(Screen.width * 2 / 10, Screen.height * 1 / 10, 80, 80), "")) 
        { box = GameObject.FindGameObjectsWithTag("img"); sijiweizhi(); sj = true; biaozhun = false; circl = false; j = 0; isfirst = false;}
        if (GUI.Button(new Rect(Screen.width * 3 / 10, Screen.height * 1 / 10, 80, 80), "")) 
        { box = GameObject.FindGameObjectsWithTag("img");sijiweizhi(); circl = true; biaozhun = false; sj = false; i = 0;isfirst = false; }

    }
    // void suiji()
    // {
    //     bool c = false;
    //     for (j = 0; j < box.Length; j++)
    //     {
    //         box[j].transform.position += (positi[j] - box[j].transform.position) * Time.deltaTime * 1.5f;
    //         box[j].transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, box[j].transform.position.z) - Vector3.back);
            
    //         if (Vector3.Distance(box[j].transform.position, positi[j]) < 1) { c = true; }

    //     }
    //     if (c) { sj = false; }

    // }
        void suiji()
    {
        bool c = false;
        for (j = 0; j < box.Length; j++)
        {
            box[j].transform.position += new Vector3(0,-10,0) * Time.deltaTime * 1.5f;
            box[j].transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, box[j].transform.position.z) - Vector3.back);
            if(box[j].transform.position.y<-10){
                box[j].transform.position = new Vector3(Random.Range(-60, 60),  30, Random.Range(0, 90));
            }
          //  if (Vector3.Distance(box[j].transform.position, positi[j]) < 1) { c = true; }

        }
        if (c) { sj = false; }

    }
    void CalualteSphere()
    {
        bool c = false;
        float inc = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f));
        float off = 2.0f / box.Length;//注意保持数值精度  
        kids = new Transform[box.Length];   
        for (int iu = 0; iu < box.Length; iu++)
        {
  
            float y = (float)iu * off - 1.0f + (off / 2.0f);
            float r = Mathf.Sqrt(1.0f - y * y);
            float phi = iu * inc;
            Vector3 pos = new Vector3(Mathf.Cos(phi) * r * size, Mathf.Sin(phi) * r * size, y * size);
            GameObject tempGo = box[iu].gameObject;  
            tempGo.transform.localPosition += (pos - tempGo.transform.localPosition) * Time.deltaTime * 2;
            tempGo.transform.rotation = Quaternion.LookRotation(Vector3.zero - tempGo.transform.position);
            tempGo.SetActive(true);
            kids[iu] = tempGo.transform;
            if (Vector3.Distance(tempGo.transform.localPosition, pos) < 0.01f) { c = true; }
        }

        if (c) { circl = false; }
    }
    void putong()
    {
        float xp ,yp,zp;
        Vector3 positio;
        bool c = false;
        for (k = 0; k < box.Length; k++)
        {
            
            xp = PlayerPrefs.GetFloat(box[k].transform.name + "1", box[k].transform.position.x);
            yp =  PlayerPrefs.GetFloat(box[k].transform.name + "2", box[k].transform.position.y);
            zp = PlayerPrefs.GetFloat(box[k].transform.name + "3", box[k].transform.position.z);
            positio = new Vector3(xp,yp, zp);
            box[k].transform.position += (positio - box[k].transform.position) * Time.deltaTime * 2f;
            box[k].transform.rotation = Quaternion.LookRotation( new Vector3(0, 0,1));
            if (Vector3.Distance(box[k].transform.position, positio) < 0.01f) { c = true; }
        }
        if (c) { biaozhun = false; }
    }
}

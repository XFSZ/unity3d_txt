using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

//public class ClickView : View {
public class xuanzhuan : MonoBehaviour
{
 
    private float lastDis = 0;
    private float cameraDis = -200f;
    public float ScaleDump = 0.1f;
    // public float speed = 0.1F;
    public GameObject[] box;
    Vector3 StartPosition;
    Vector3 cameraM;
    Vector3 positiontemp;
    Vector3 previousPosition;
    Vector3 offset;
    Vector3 finalOffset;
    public float time = 0;
    bool isSlide;
    public bool touchhit = false;
    GameObject touchh;
    float angle;
    public bool two = false;
    bool tempposi = false;
  public  bool isexcel= true;
    // public Vector3 center =  Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            
            AndroidUpdate();
        }
        else
        {
            cameraM = new Vector3(0, 0, -185);
            PCUpdate();
        }

    }
    void AndroidUpdate()
    {
        if (Input.touchCount > 0)
        {
              cameraM = new Vector3(0, 0, -185);
            // dispatcher.Dispatch(TouchMainEvent.TOUCH_CLICK);
            box = GameObject.FindGameObjectsWithTag("img");
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    StartPosition = Input.mousePosition;
                    previousPosition = Input.mousePosition;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    offset = Input.mousePosition - previousPosition;
                    previousPosition = Input.mousePosition;
                    Vector3 Xoffset = new Vector3(offset.x, 0, 0);
                    Vector3 Yoffset = new Vector3(0, offset.y, 0);
                    for (int i = 0; i < box.Length; i++)
                    {
                        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
                            box[i].transform.RotateAround(Vector3.up, Vector3.Cross(Xoffset, Vector3.forward).normalized, offset.magnitude / 10);
                        else
                            box[i].transform.RotateAround(Vector3.left, Vector3.Cross(Yoffset, Vector3.forward).normalized, offset.magnitude / 20);

                    }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "img" && touchhit == false)
                        {
                            touchhit = true;
                            tempposi = !tempposi;
                          
                        }
                        touchh = GameObject.Find(hit.transform.name);
                        if (touchh.gameObject.transform.position.z >= -0.2f)
                        {
                            positiontemp = touchh.gameObject.transform.position;
                        }
                    }

                }
                finalOffset = Input.mousePosition - StartPosition;
                isSlide = true;
                angle = finalOffset.magnitude;
                if (two == true)
                {
                    isSlide = false;
                    // angle = 0;
                    // finalOffset = Vector3.zero;
                    two = false;
                }
            }
            else if (Input.touchCount > 1)
            {
                two = true;
                Touch t1 = Input.GetTouch(0);
                Touch t2 = Input.GetTouch(1);
                if (t2.phase == TouchPhase.Began)
                {
                    lastDis = Vector2.Distance(t1.position, t2.position);
                }
                if (t1.phase == TouchPhase.Moved && t2.phase == TouchPhase.Moved)
                {
                    float dis = Vector2.Distance(t1.position, t2.position);
                    if (Mathf.Abs(dis - lastDis) > 1)
                    {
                        cameraDis += (dis - lastDis) * ScaleDump;
                        cameraDis = Mathf.Clamp(cameraDis, -200f, -10f);
                        lastDis = dis;
                    }
                }

            }
        }


        if (isSlide)

        {
            Vector3 XfinalOffset = new Vector3(finalOffset.x, 0, 0);
            Vector3 YfinalOffset = new Vector3(0, finalOffset.y, 0);
            for (int i = 0; i < box.Length; i++)
            {
                if (Mathf.Abs(finalOffset.y) > Mathf.Abs(finalOffset.x))
                    box[i].transform.RotateAround(Vector3.right, Vector3.Cross(YfinalOffset, Vector3.forward).normalized, angle / 8 * Time.deltaTime);
                else
                    box[i].transform.RotateAround(Vector3.up, Vector3.Cross(XfinalOffset, Vector3.forward).normalized, angle / 8 * Time.deltaTime);
            }

            if (angle > 20)
            {
                angle -= angle / 20;
            }
            else
            {
                angle = 0;
                isSlide = false;
            }
        }
        if (touchhit == true)
        {
             StartCoroutine(move());
            time += Time.deltaTime;
            if (time <= 0.5f)
            {
                // hu.Update();
                touchh.transform.Rotate(-1 * Vector3.up, 360 / 0.5f * Time.deltaTime);
            }
            else { touchhit = false; time = 0; }

        }
    }
    void PCUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // box = GameObject.FindGameObjectsWithTag("img");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "img" && touchhit == false)
                {
                    touchhit = true;
                    tempposi = !tempposi;
                }
                touchh = GameObject.Find(hit.transform.name);
                if (touchh.gameObject.transform.position.z >= -0.2f)
                {
                    positiontemp = touchh.gameObject.transform.position;
                }

            }
        }
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(Vector3.zero, Vector3.Cross(new Vector3(-1, 0, 0), Vector3.forward).normalized, Input.GetAxis("Mouse X") * 5);
            //transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * 5);
            transform.RotateAround(Vector3.zero, new Vector3(-1, 0, 0), Input.GetAxis("Mouse Y") * 5);

            // this.transform.LookAt(center.transform.position);
            this.transform.LookAt(Vector3.zero);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0&& isexcel ==false )
        {
            //   cameraDis += 2 ;
            //   cameraDis = Mathf.Clamp(cameraDis, -200f, -10f);
                        
            if(Camera.main.fieldOfView>160){
               Camera.main.fieldOfView = 160;
            }else{
               Camera.main.fieldOfView += 2;
            }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0&& isexcel ==false )
        {
             if(Camera.main.fieldOfView<6){
               Camera.main.fieldOfView = 4;
            }else{
               Camera.main.fieldOfView -= 2;
            }
         
        }
        if (touchhit == true)
        {
            time += Time.deltaTime;
            StartCoroutine(move());
            if (time <= 0.5f)
            {
                // hu.Update();
                touchh.transform.Rotate(-1 * Vector3.up, 360 / 0.5f * Time.deltaTime);
                //   touchh.transform.position += (cameraM - touchh.transform.position)*Time.deltaTime;


            }

            else { touchhit = false; time = 0; }

        }


    }
    void LateUpdate()
    {
        if (time >= 0.5f)
        {
            touchhit = false; time = 0;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            this.transform.position = new Vector3(0, 0, cameraDis);
        }

    }
    IEnumerator move()
    {
        if (tempposi == true)
        {
            touchh.transform.position += (cameraM - touchh.transform.position) * Time.deltaTime * 6f;
        }
        else
        {
            touchh.transform.position += (positiontemp - touchh.transform.position) * Time.deltaTime * 6f;
        }

        yield return null;
    }
}

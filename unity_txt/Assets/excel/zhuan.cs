using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zhuan : MonoBehaviour
{
    private float lastDis = 0;
    private float cameraDis = -200f;
    public float ScaleDump = 0.1f;
    // public float speed = 0.1F;
    private GameObject[] box;
    Vector3 StartPosition;
    Vector3 previousPosition;
    Vector3 offset;
    Vector3 finalOffset;
    float time;
    bool isSlide;
    bool touchhit;
    GameObject touchh;
    float angle;
    bool two;

    // Use this for initialization
    void Start()
    {
        time = 0;
        touchhit = false;
        two = false;

    }

    // Update is called once per frame
    public void Update()
    {

        if (Input.touchCount > 0)
        {
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
                        // if (box[i].transform.eulerAngles.y<=90) {

                        //   box[i].transform.RotateAround(Vector3.up, Vector3.Cross(Xoffset, Vector3.forward).normalized, offset.magnitude / 10);

                        //   }                   
                        //  box[i].transform.RotateAround(Vector3.left, Vector3.Cross(Yoffset, Vector3.forward).normalized, offset.magnitude / 20);
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
                        }
                        touchh = GameObject.Find(hit.transform.name);
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
        if (touchhit)
        {
            time += Time.deltaTime;
            if (time <= 0.5f)
            {
                // hu.Update();
                touchh.transform.Rotate(-1 * Vector3.up, 360 / 0.5f * Time.deltaTime);
            }
            else { touchhit = false; time = 0; }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(1);
        //  Application.Quit();
    }
    void LateUpdate()
    {
        this.transform.position = new Vector3(0, 0, cameraDis);
    }
}

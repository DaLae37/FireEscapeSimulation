using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour
{
    public static Walk instance;

    public GameObject fire1, fire2, smoke;
    public Light light;
    FileStream fs;
    NavMeshAgent nav;
    Vector3 forward;
    Transform child;
    bool isMove;
    bool startSecondFire;

    string m_strPath = "Assets/";

    public void WriteData(string strData)
    {
        FileStream f = new FileStream(m_strPath + "output.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(m_strPath + "output.txt"))
        {
            FileInfo fileDel = new FileInfo(m_strPath + "output.txt");
            fileDel.Delete();
        }
        instance = this;
        nav = GetComponent<NavMeshAgent>();
        child = transform.GetChild(0);
        isMove = false;
        startSecondFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!SubWayManager.instance.gameOver)
        {
            if (SubWayManager.instance.isFire)
            {
                WriteData(transform.position.x.ToString() + ", " + transform.position.z.ToString());
            }
            if (Input.GetKeyDown("joystick button 15"))
            {
                forward = child.transform.forward;
                isMove = true;
            }
            else if (Input.GetKeyUp("joystick button 15"))
            {
                isMove = false;
            }
            if (isMove)
                nav.SetDestination(transform.position + forward);
            if (!startSecondFire)
            {
                if (SubWayManager.instance.secondFire)
                {
                    startSecondFire = true;
                    fire2.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StairSignal")
        {
            if (SubWayManager.instance.isFire)
            {
                nav.speed = 0.25f;
            }
            else
            {
                nav.speed = 0.375f;
            }
        }
        if (other.gameObject.tag == "Exit" && SubWayManager.instance.isFire)
        {
            SubWayManager.instance.canExit = true;
        }
        if (other.gameObject.tag == "FireStart")
        {
            SubWayManager.instance.isFire = true;
            SubWayManager.instance.sw.Start();
            SoundManager.instance.BuzzSiren();
            fire1.SetActive(true);
            fire2.SetActive(true);
            smoke.SetActive(true);
            isMove = false;
            nav.speed = 1.5f;
            StartCoroutine("StartingFire");
        }

    }

    IEnumerator StartingFire()
    {
        for(int i=1; i<=123; i++)
        {
            light.color = new Color(light.color.r - 1, light.color.g - 1, light.color.b - 1);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StairSignal")
        {
            if (SubWayManager.instance.isFire)
            {
                nav.speed = 1;
            }
            else
            {
                nav.speed = 1.5f;
            }
        }
    }
}
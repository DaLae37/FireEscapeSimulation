using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour
{
    public static Walk instance;
    FileStream fs;
    NavMeshAgent nav;
    Vector3 forward;
    Transform child;
    bool isMove, upStair;

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
        isMove = upStair = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!SubWayManager.instance.gameOver)
        {
            WriteData(transform.position.x.ToString() + ", " + transform.position.z.ToString());
            if (Input.GetKeyDown("joystick button 14"))
            {
                if (isMove)
                {
                    isMove = false;
                }
                else
                {
                    forward = child.transform.forward;
                    isMove = true;
                }
            }
            if (isMove)
                nav.SetDestination(transform.position + forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "StairSignal")
        {
            if (!upStair)
            {
                nav.speed = 0.25f;
                upStair = true;
            }
            else
            {
                nav.speed = 1;
                upStair = false;
            }
        }
        if(other.gameObject.tag == "Exit")
        {
            SubWayManager.instance.canExit = true;
        }
    }
}

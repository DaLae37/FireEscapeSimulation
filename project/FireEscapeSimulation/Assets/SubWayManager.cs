using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SubWayManager : MonoBehaviour
{
    public static SubWayManager instance;
    Stopwatch sw = new Stopwatch();
    public bool gameOver, canExit;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameOver = canExit = false;
        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && sw.ElapsedMilliseconds >= 1000 * 300)
        {
            gameOver = true;
        }
        if (canExit)
        {
            SceneManager.LoadScene("mainScene");
        }
    }
}

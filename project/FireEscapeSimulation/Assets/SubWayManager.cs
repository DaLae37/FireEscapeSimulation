using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SubWayManager : MonoBehaviour
{
    public static SubWayManager instance;
    public Stopwatch sw = new Stopwatch();
    public bool gameOver, canExit, isFire, secondFire;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameOver = canExit = false;
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            if (!gameOver)
            {
                if(sw.ElapsedMilliseconds >= 1000 * 300)
                {
                    gameOver = true;
                }
                else if(sw.ElapsedMilliseconds >= 1000 * 120)
                {
                    secondFire = true;
                }
            }
            if (canExit)
            {
                SceneManager.LoadScene("mainScene");
            }
        }
    }
}

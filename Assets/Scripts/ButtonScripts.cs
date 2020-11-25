using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScripts : MonoBehaviour
{
    public bool isPlay, isExit;
    private Rigidbody myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (isPlay)
        {
            myBody.AddForce(Vector3.up * 200f);
            Invoke("LoadGame", 1.5f);
        }

        if (isExit)
        {
            myBody.AddForce(Vector3.up * 200f);
            Invoke("ExitGame", 1.5f);
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}

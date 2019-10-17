using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public static bool active = false;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Update()
    {
        //if(active)
        //    gameObject.SetActive(true);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Menu");
    }
}

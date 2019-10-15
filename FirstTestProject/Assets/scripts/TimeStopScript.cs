using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTime()
    {
        ScrollingScript.isTimeStopped = true;
        EnemyScript.isTimeStopped = true;
        MoveScript.isTimeStopped = true;
        //var variableForPrefab = Resources.Load("prefabs/Enemy", GameObject) as GameObject;
    }
}

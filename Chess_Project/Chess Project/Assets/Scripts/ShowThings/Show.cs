using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour {

    public bool showing;
    Vector3 positionGo = new Vector3(115f,145f,0f);
    Vector3 positionBack = new Vector3(115f, -150f, 0f);
    // Update is called once per frame
    void Update () {
        if(showing)
            transform.position = Vector3.MoveTowards(transform.position, positionGo, 500f * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, positionBack, 500f * Time.deltaTime);
    }
}

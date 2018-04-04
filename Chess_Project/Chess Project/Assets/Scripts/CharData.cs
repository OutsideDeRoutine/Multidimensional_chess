using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharData : MonoBehaviour {

    public Vector3 pos;
    public float height=0.8f;


    public void colocar(Vector3 pos)
    {
        this.transform.position = pos + new Vector3(0f, height, 0f);
        this.pos = pos;
    }

    internal void setTeam(string tag)
    {
        this.tag = tag;
        if (tag.Equals("Blancas"))
        {
            GetComponent<Renderer>().material.color = Color.white;
        } 
        else if (tag.Equals("Negras"))
        {
            GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}

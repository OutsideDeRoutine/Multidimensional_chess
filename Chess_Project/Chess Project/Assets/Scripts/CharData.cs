using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharData : MonoBehaviour {

    public Vector3 pos;
    public GameObject tile;

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

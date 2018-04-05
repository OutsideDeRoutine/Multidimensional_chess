using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharData : MonoBehaviour {

    public GameObject tile;
    public UnityEvent canMoveThere;

    //Called once when creating table
    internal void setTeam(string tag)
    {
        Renderer render = GetComponent<Renderer>();
        this.tag = tag;
        if (tag.Equals("Blancas"))
        {
            render.material.color = Color.white;
        } 
        else if (tag.Equals("Negras"))
        {
            render.material.color = Color.grey;
        }
    }

    public Vector3 getTilePosition()
    {
       return this.tile.GetComponent<TileData>().position;
    }
}

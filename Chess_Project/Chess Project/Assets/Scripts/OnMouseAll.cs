﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseAll : MonoBehaviour {

    private Renderer rend;
    public Color color=Color.gray;
    private Color fcolor;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseEnter()
    {
        fcolor = rend.material.color;
        rend.material.color += color;
    }

    void OnMouseExit()
    {
        rend.material.color -= color;
    }

    void OnMouseDown()
    {
        if(this.tag=="Blancas" || this.tag == "Negras")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AssingChar(this.gameObject);
        }
        else if(this.tag == "Tile")
        {

        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseAll : MonoBehaviour {

    private Renderer rend;
    public Color color=Color.gray;
    private PlayerController player;

    void Start()
    {
        rend = GetComponent<Renderer>();
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnMouseEnter()
    {
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
            player.AssingChar(this.gameObject);
        }
        else if(this.tag == "Tile")
        {
            if (this.GetComponent<TileData>().state == 2 && player.selectedChar!=null)
            {
                //Mover  , resetear tablero , cambiar de jugador
                ArrayList all = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));
                foreach (GameObject tile in all)
                {
                    if (tile.GetComponent<TileData>().state == 2)
                    {
                        tile.GetComponent<TileData>().unSetMovableTo();
                    }
                }
                GetComponent<TileData>().setCharacter(player.selectedChar);
                player.ChangePlayer();
            }
        }
    }
}

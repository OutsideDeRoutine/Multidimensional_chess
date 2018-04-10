using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharData : MonoBehaviour {

    public GameObject tile;
    public UnityEvent canMoveThere;
    public UnityEvent canAttackThere;
    public UnityEvent canHitThere;
    public UnityEvent canHitTheKing;

    //datos para el juego

    public int vida;
    public int damage;

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
            this.transform.Rotate(0f, 180f, 0f);
            render.material.color = Color.grey;
        }
    }

    public Vector3 getTilePosition()
    {
       return tile.GetComponent<TileData>().position;
    }

    public Boolean receibeDamage(int damageRecibed)
    {
        
        vida -= damageRecibed;
        if (vida <= 0)
        {
            return true;
        }       
        return false;
    }

    public Boolean isDead()
    {
        return (vida <= 0);
    }

    internal string getData()
    {
        return damage+ "T " + vida + "♥" ;
    }
}

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
            render.material.color = Color.grey;
        }
    }

    public Vector3 getTilePosition()
    {
       return this.tile.GetComponent<TileData>().position;
    }

    public Boolean receibeDamage(int damageRecibed)
    {
        this.vida -= damageRecibed;
        Debug.Log("A la pieza " + this.name + " le queda " + this.vida + "\n");
        if (this.vida <= 0)
        {
            Debug.Log("La pieza "+ this.name + " ha muerto \n");
            return true;
        }       
        return false;
    }

    public Boolean isDead()
    {
        return (vida <= 0) ? true : false;
    }
}

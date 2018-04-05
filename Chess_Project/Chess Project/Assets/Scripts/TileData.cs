using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    public Vector3 position;
    public GameObject character;
    public int state = 0; //0 - empty   1 - full    2- actually movible to


    public void setPosition(int x, int y, int z)
    {
        position = new Vector3(x,y,z);
    }

    internal void setCharacter(GameObject v)
    {
        if (v != null)
        {
            character = v;
            GameObject lasttile = character.GetComponent<CharData>().tile;
            if (lasttile != null)
            {
                lasttile.GetComponent<TileData>().removeChar();
            }
            character.GetComponent<CharData>().tile = this.gameObject;
            state = 1;
            character.transform.position = this.transform.position+new Vector3(0f,this.GetComponent<Collider>().bounds.size.y/2 + character.GetComponent<Collider>().bounds.size.y / 2, 0f);
        }
    }

    private void removeChar()
    {
        character.GetComponent<CharData>().tile=null;
        character = null;
        state = 0;
    }

    internal void setMovableTo()
    {
        state = 2;
        GetComponent<Renderer>().material.color += Color.green;
    }

    internal void unSetMovableTo()
    {
        state = 0;
        GetComponent<Renderer>().material.color -= Color.green;
    }
}

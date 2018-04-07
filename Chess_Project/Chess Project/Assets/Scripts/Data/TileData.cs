using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    public Vector3 position;
    public GameObject character;
    public int state = 0; //0 - empty   1 - full    2- actually movible to      3- actually attack posible  4- actually hit posible


    public void setPosition(int x, int y, int z)
    {
        position = new Vector3(x,y,z);
        if(x%2==0 && z % 2 == 0)
        {
            GetComponent<Renderer>().material.color = Color.grey;
        }else if(z % 2 != 0 && x % 2 != 0)
        {
            GetComponent<Renderer>().material.color = Color.grey;
        }
    }

    internal void setCharacter(GameObject v,bool instant=false)
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
            Vector3 position = this.transform.position+new Vector3(0f,this.GetComponent<Collider>().bounds.size.y/2 + character.GetComponent<Collider>().bounds.size.y / 2, 0f);
            if(instant) v.transform.position = position;
            else StartCoroutine(moveChar(v, position));

        }
    }


    private IEnumerator moveChar(GameObject v, Vector3 position)
    {
        while (Vector3.Distance(v.transform.position, position)!=0)
        {
            if (v.transform.position.y== position.y)
            {
                v.transform.position = Vector3.MoveTowards(v.transform.position, position, 10f * Time.deltaTime);
            }
            else
            {
                v.transform.position += new Vector3(0f, Mathf.Clamp(-v.transform.position.y + position.y , -10f * Time.deltaTime, 10f * Time.deltaTime), 0f);
            }
               
            yield return new WaitForFixedUpdate();
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


    internal void unSetAttackTo()
    {
        state = 1;
        GetComponent<Renderer>().material.color -= Color.red;
    }


    internal void setAttackTo()
    {
        state = 3;
        GetComponent<Renderer>().material.color += Color.red;
    }

    internal void setHitTo()
    {
        state = 4;
        GetComponent<Renderer>().material.color += Color.blue;
    }

    internal void unSetHitTo()
    {
        state = 1; //solo puede estar vacio cuando se realice el movimiento
        GetComponent<Renderer>().material.color -= Color.blue;
    }


    internal void destroyChar()
    {
        GameObject.Destroy(character);
        character = null;
        state = 0;

    }
}

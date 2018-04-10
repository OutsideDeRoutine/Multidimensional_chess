using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    public enum State {Empty=0, Taken=1, Move=2, Attack=3, Hit=4}

    public Vector3 position;
    public GameObject character;
    public State state = State.Empty;

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
            state = State.Taken;
            Vector3 position = GetPos(character);
            if (instant) v.transform.position = position;
            else StartCoroutine(moveChar(v, position));

        }
    }

    internal Vector3 GetPos(GameObject chara)
    {
        return this.transform.position + new Vector3(0f, this.GetComponent<Collider>().bounds.size.y / 2 + chara.GetComponent<Collider>().bounds.size.y / 2, 0f);
    }


    internal IEnumerator moveChar(GameObject v, Vector3 position, params Action[] end)
    {
        while (v!=null && Vector3.Distance(v.transform.position, position)!=0)
        {
            if (v.transform.position.y== position.y)
            {
                v.transform.position = Vector3.MoveTowards(v.transform.position, position, 10f * Time.deltaTime);
            }
            else
            {
                v.transform.position += new Vector3(0f, Mathf.Clamp(-v.transform.position.y + position.y , -10f * Time.deltaTime, 10f * Time.deltaTime), 0f);
            }
            if ( Vector3.Distance(v.transform.position, position) == 0 && end.Length > 0)
            {
                foreach(Action e in end)
                {
                    e();
                }
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

    private void removeChar()
    {
        character.GetComponent<CharData>().tile=null;
        character = null;
        state = State.Empty;
    }

    internal void setMovableTo()
    {
        state = State.Move;
        GetComponent<Renderer>().material.color += Color.green;
    }

    internal void unSetMovableTo()
    {
        state = State.Empty;
        GetComponent<Renderer>().material.color -= Color.green;
    }


    internal void unSetAttackTo()
    {
        state = State.Taken;
        GetComponent<Renderer>().material.color -= Color.red;
    }


    internal void setAttackTo()
    {
        state = State.Attack;
        GetComponent<Renderer>().material.color += Color.red;
    }

    internal void setHitTo()
    {
        state = State.Hit;
        GetComponent<Renderer>().material.color += Color.blue;
    }

    internal void unSetHitTo()
    {
        state = State.Taken; //solo puede estar vacio cuando se realice el movimiento
        GetComponent<Renderer>().material.color -= Color.blue;
    }


    internal void destroyChar()
    {
        GameObject.Destroy(character);
        character = null;
        state = State.Empty;

    }
}

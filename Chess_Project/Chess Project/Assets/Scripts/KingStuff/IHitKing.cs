using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IHitKing {

    bool canWhite(Vector3 from);
    bool canBlack(Vector3 from);
    void hitKing();
    void manageAtack(bool atacked,string tag);
}

public abstract class IHitContiguo: MonoBehaviour, IHitKing
{
    //si la fichas es blanca
    //miramos si puede atacar al rey negro
    public bool canWhite(Vector3 from)
    {
        if (from.x == 10 && from.y == 2)
        {
            return true;
        }
        return false;
    }


    //si la fichas es blanca
    //miramos si puede atacar al rey blanco
    public bool canBlack(Vector3 from)
    {
        if (from.x == 0 && from.y == 0)
        {
            return true;
        }
        return false;
    }

    public void hitKing()
    {
        Vector3 from = this.GetComponent<CharData>().getTilePosition();
        if (this.tag.Equals("Blancas"))
        {
            manageAtack(canWhite(from), this.tag) ;          
        }
        else if (this.tag.Equals("Negras"))
        {
            manageAtack(canBlack(from), this.tag);
        }

    }

    public void manageAtack(bool atacked,string tagAux)
    {
        if (atacked)
        {
            ArrayList tilesKing = new ArrayList(GameObject.FindGameObjectsWithTag("TileKing"));

            var query = from GameObject tile in tilesKing where (!tile.GetComponent<TileData>().character.tag.Equals(tagAux)) select tile;

            foreach (GameObject tile in query)
            {
                tile.GetComponent<TileData>().setAttackTo();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//GENERALIZAR PARA EL RESTO DE FICHAS
public class DGuyMove : MonoBehaviour {


	public void PossibleMovTiles()
    {
        ArrayList all = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));
        foreach (GameObject tile in all)
        {
            if (tile.GetComponent<TileData>().state == TileData.State.Move)
            {
                tile.GetComponent<TileData>().unSetMovableTo();
            }
        }

        Vector3 fromo = this.GetComponent<CharData>().getTilePosition();

        var query = from GameObject tile in all
                    where can(fromo ,tile.GetComponent<TileData>().position) &&
                          tile.GetComponent<TileData>().state == 0
                    select tile;

        foreach (GameObject tile in query)
        {
            tile.GetComponent<TileData>().setMovableTo();
        }

    }

    public virtual  bool can(Vector3 from , Vector3 where)
    {
        return (from.y == where.y && (from.x == where.x || from.z == where.z)); 
        //|| (Mathf.Abs(from.y - where.y) == 1 && from.x == where.x && from.z == where.z); PARA CAMBIAR DE DIMENSION
    }



    public bool controlIsla(Vector3 fromo, Vector3 where, ArrayList all)
    {
        for (float i = fromo.x; i < where.x; i++)
        {
            var query = from GameObject tile in all
                        where tile.GetComponent<TileData>().position.x == i &&
                                tile.GetComponent<TileData>().position.y == fromo.y
                        select tile;
            if (query.Count() == 0)
            {
                return false;
            }
        }
        return true;
    }
}

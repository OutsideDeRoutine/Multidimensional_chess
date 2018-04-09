using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IHit
{

    bool can(Vector3 from, Vector3 where);
    bool controlIsla(Vector3 fromo, Vector3 where, ArrayList all);
    void PossibleHitTiles();
}

public abstract class AbstractHit: MonoBehaviour,IHit
{
    public abstract bool can(Vector3 from, Vector3 where);

    public void PossibleHitTiles()
    {
        ArrayList all = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));

        foreach (GameObject tile in all)
        {
            if (tile.GetComponent<TileData>().state == 4)
            {
                tile.GetComponent<TileData>().unSetHitTo();
            }
        }

        Vector3 fromo = this.GetComponent<CharData>().getTilePosition();

        var query = from GameObject tile in all
                    where can(fromo, tile.GetComponent<TileData>().position) &&
                          tile.GetComponent<TileData>().state == 1 &&
                          tile.GetComponent<TileData>().character.tag != this.tag
                    select tile;

        foreach (GameObject tile in query)
        {
            tile.GetComponent<TileData>().setHitTo();
        }
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

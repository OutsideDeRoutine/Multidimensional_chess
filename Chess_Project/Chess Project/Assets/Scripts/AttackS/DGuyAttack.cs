using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DGuyAttack : MonoBehaviour {

    public void PossibleAttackTiles()
    {
        ArrayList all = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));
        foreach (GameObject tile in all)
        {
            if (tile.GetComponent<TileData>().state == TileData.State.Attack)
            {
                tile.GetComponent<TileData>().unSetAttackTo();
            }
        }

        Vector3 fromo = this.GetComponent<CharData>().getTilePosition();

        var query = from GameObject tile in all
                    where can(fromo, tile.GetComponent<TileData>().position) &&
                          tile.GetComponent<TileData>().state == TileData.State.Taken &&
                          tile.GetComponent<TileData>().character.tag != this.tag
                    select tile;

        foreach (GameObject tile in query)
        {
            tile.GetComponent<TileData>().setAttackTo();
        }

    }

    public virtual bool can(Vector3 from, Vector3 where)
    {
        return from.y == where.y && ((Mathf.Abs(from.x - where.x) == Mathf.Abs(from.z - where.z)) && Mathf.Abs(from.x - where.x) == 1);
    }
}

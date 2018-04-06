using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SniperMove : DGuyMove{


    public override bool can(Vector3 from, Vector3 where)
    {
        bool cancan= from.y == where.y && (Mathf.Abs(from.x - where.x) == Mathf.Abs(from.z - where.z)) || (Mathf.Abs(from.y - where.y) == 1 && from.x == where.x && from.z == where.z);
        //Manera chapucera de mirar si estas intentando cruzar a una de las islas
        if (cancan)
        {
            ArrayList all = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));
            if (from.x < where.x)
            {
                if(!controlIsla(from, where, all)) return false;
            }
            else if (from.x > where.x)
            {
                if (!controlIsla(where, from, all)) return false;
            }
        }
        return cancan;
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHitSniper :  IHitContiguo
{
    public override bool canWhite(Vector3 from)
    {
        if (from.y==2)
        {
            return true;
        }
        return false;
    }

    public override bool canBlack(Vector3 from)
    {
        if ( from.y == 0)
        {
            return true;
        }
        return false;
    }


}

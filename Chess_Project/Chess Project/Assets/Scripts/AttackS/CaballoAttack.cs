using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballoAttack : DGuyAttack
{

    public override bool can(Vector3 from, Vector3 where)
    {
        return (from.y == where.y && Mathf.Abs( from.x - where.x)==1 && Mathf.Abs(from.z - where.z) == 1);
    }
}

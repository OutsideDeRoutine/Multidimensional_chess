using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : DGuyAttack
{

    public override bool can(Vector3 from, Vector3 where)
    {
        return (from.y == where.y && (from.x == where.x || from.z == where.z));
    }
}

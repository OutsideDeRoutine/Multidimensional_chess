﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseHit : AbstractHit
{
    public override bool can(Vector3 from, Vector3 where)
    {
        return Mathf.Abs(from.y - where.y) == 1 && ((from.z == where.z && Mathf.Abs(from.x - where.x) == 2) || (from.x == where.x && Mathf.Abs(from.z - where.z) == 2));

    }
}

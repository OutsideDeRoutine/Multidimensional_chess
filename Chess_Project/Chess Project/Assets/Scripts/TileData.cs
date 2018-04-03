using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    public Vector3 position;
    public int state = 0;


    public void setPosition(int x, int y, int z)
    {
        position = new Vector3(x,y,z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {



    private string sideform = "-----  --|" +
                              "  -----  |" +
                              "--  -----";

    private int zTiles=5;
    private int yDif = 5;


    // Use this for initialization
    void Start () {
        GenerateMap(sideform.Split('|'));
    }

    void GenerateMap (string[] platforms)
    {
        int y = 0;
        int x = 0;
        foreach (string platform in platforms)
        {
            foreach (char plat in platform)
            {
                if(plat!=' ')
                {
                    for(int z=0; z< zTiles; z++)
                    {
                        GameObject tile = Instantiate(Resources.Load("Cube", typeof(GameObject))) as GameObject;
                        tile.transform.position = new Vector3(x, y * -yDif, z);
                        tile.GetComponent<TileData>().setPosition(x, y, z);
                    }
                }
                x++;
            }
            y ++;
            x = 0;
        }
    }
	
}

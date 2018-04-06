using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [System.Serializable]
    public struct ficha
    {
        public string name;
        public string resource;
        public Vector3 pos;
        public string tag;
    };

    public List<ficha> fichas;

    //patrón para la creación del tablero
    private string sideform = "----- --   |" +
                              "   -----   |" +
                              "   -- -----";

    //tamaño del tile
    private int zTiles = 5;
    //diferencias de alturas entre tiles
    private int yDif = 5;
    //private IDictionary<String, GameObject> tiles;

    // Use this for initialization
    void Start()
    {
        //tiles = new Dictionary<String, GameObject>();
        GenerateMap(sideform.Split('|'));
    }
    //revisar
    //metodo para generar los tiles del tablero
    void GenerateMap(string[] platforms)
    {
        int y = 0;
        int x = 0;
        foreach (string platform in platforms)
        {
            foreach (char plat in platform)
            {
                if (plat != ' ')
                {
                    for (int z = 0; z < zTiles; z++)
                    {
                        //En Cube se guardan los TileData
                        GameObject tile = Instantiate(Resources.Load("Cube", typeof(GameObject))) as GameObject;
                        //definimos posicion fisica de los tiles
                        tile.transform.position = new Vector3(x, y * -yDif, z);
                        //definimos posicion lógica de los tiles (se guarda dentro de TileData)
                        tile.GetComponent<TileData>().setPosition(x , y, z);
                        tile.name = "Tile_" + x + "-" + z + "_nivel:" + y;
                            tile.GetComponent<TileData>().setCharacter(generateCharAtPos(new Vector3(x, y, z)));
                    }
                }
                x++;
            }
            y++;
            x = 0;
        }
    }

    //Método para generar a los personajes jugables sobre los tiles del tablero
    private GameObject generateCharAtPos(Vector3 pos)
    {
        ficha colocar=fichas.Find(x=>x.pos.Equals(pos));
        if(colocar.Equals( default(ficha))) return null;

        GameObject fc = Instantiate(Resources.Load(colocar.resource, typeof(GameObject))) as GameObject;
        fc.name = colocar.name;
        fc.GetComponent<CharData>().setTeam(colocar.tag);
        return fc;


        /*
         Cosas a quizá tener en cuenta:
            usar un prefab para las piezas (de manera general) y luego especificarlas en codigo
            definir un mouseClick o algo parrecido luego para las fichas

            habrá que ver como mover luego las piezas entre las islas
         
            suicidio :)
         */
    }

}

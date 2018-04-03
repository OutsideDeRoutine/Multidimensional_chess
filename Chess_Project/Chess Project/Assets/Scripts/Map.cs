using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{


    //patrón para la creación del tablero
    private string sideform = "-----  --|" +
                              "  -----  |" +
                              "--  -----";

    //tamaño del tile
    private int zTiles = 5;
    //diferencias de alturas entre tiles
    private int yDif = 5;
    private IDictionary<String,GameObject> tiles;

    // Use this for initialization
    void Start()
    {
        tiles = new Dictionary<String,GameObject>();
        GenerateMap(sideform.Split('|'));       
    }

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
                        tile.GetComponent<TileData>().setPosition(x, y, z);
                        tile.name = "Tile_" + tile.GetComponent<TileData>().position.x +"-"+ tile.GetComponent<TileData>().position.z +"_nivel:"+ tile.GetComponent<TileData>().position.y;
                        //añadimos el tile a la 
                        String key = "tile" + tile.GetComponent<TileData>().position.x + "-" + tile.GetComponent<TileData>().position.z + "-" + tile.GetComponent<TileData>().position.y;
                        tiles.Add(key,tile);
                    }
                }
                x++;
            }
            y++;
            x = 0;
        }
        generateCharacters();
    }

    //Método para generar a los personajes jugables sobre los tiles del tablero
    private void generateCharacters()
    {
        /*
         Cosas a quizá tener en cuenta:
            usar un prefab para las piezas (de manera general) y luego especificarlas en codigo
            definir un mouseClick o algo parrecido luego para las fichas
         
            suicidio :)
         */


        //crear personajes circulares (de mientras) 
        GameObject charPorDefecto = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        charPorDefecto.transform.localScale = new Vector3(0.7F , 0.7F,0.7F);
        //probando generacion simple en una celda determinada
        //primero sacamos el tile, tendremos que sacarlos a pelo para la generación de las piezas
        //no tiene que ser chapucero por que se colocan de esa manera y no permitimos diferentes tableros
        GameObject tileAux = tiles["tile1-2-0"];

        charPorDefecto.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);



    }

}

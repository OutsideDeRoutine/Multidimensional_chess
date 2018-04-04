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
    private IDictionary<String, GameObject> tiles;

    // Use this for initialization
    void Start()
    {
        tiles = new Dictionary<String, GameObject>();
        GenerateMap(sideform.Split('|'));
    }
    //revisar
    //metodo para generar los tiles del tablero
    void GenerateMap(string[] platforms)
    {
        int y = 0;
        int x = 0;
        int white = 0;
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
                        tile.transform.position = new Vector3(x + white, y * -yDif, z);
                        //definimos posicion lógica de los tiles (se guarda dentro de TileData)
                        tile.GetComponent<TileData>().setPosition(x , y, z);
                        tile.name = "Tile_" + tile.GetComponent<TileData>().position.x + "-" + tile.GetComponent<TileData>().position.z + "_nivel:" + tile.GetComponent<TileData>().position.y;
                        //añadimos el tile a la 
                        String key = "tile" + tile.GetComponent<TileData>().position.x + "-" + tile.GetComponent<TileData>().position.z + "-" + tile.GetComponent<TileData>().position.y;
                        tiles.Add(key, tile);
                    }
                    x++;
                }
                else
                {
                    white++;
                }

            }
            y++;
            white = 0;
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

            habrá que ver como mover luego las piezas entre las islas
         
            suicidio :)
         */

        //creacion de las piezas quizá se cambie en funcion de como se hagan los creadores

        //Señores que se mueven en 2d
        GameObject caminante2dblanco = Instantiate(Resources.Load("2DGuy", typeof(GameObject))) as GameObject;
        caminante2dblanco.tag = "Blancas";
        caminante2dblanco.name = "Caminante2dBlanco";
        GameObject tileAux = tiles["tile0-0-0"];
        caminante2dblanco.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);

        GameObject caminante2dNegro = Instantiate(Resources.Load("2DGuy", typeof(GameObject))) as GameObject;
        caminante2dNegro.tag = "Negras";
        caminante2dNegro.name = "Caminante2dNegro";
        tileAux = tiles["tile6-4-2"];
        caminante2dNegro.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);

        //Francotirador
        GameObject sniperBlanco = Instantiate(Resources.Load("Sniper", typeof(GameObject))) as GameObject;
        sniperBlanco.tag = "Blancas";
        sniperBlanco.name = "SniperBlanco";
        tileAux = tiles["tile0-4-0"];
        sniperBlanco.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 1.25F, tileAux.transform.position.z);

        GameObject sniperNegro = Instantiate(Resources.Load("Sniper", typeof(GameObject))) as GameObject;
        sniperNegro.tag = "Negras";
        sniperNegro.name = "SniperNegro";
        tileAux = tiles["tile6-0-2"];
        sniperNegro.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 1.25F, tileAux.transform.position.z);

        //Caballo interdimensional
        GameObject caballoBlanco = Instantiate(Resources.Load("Caballo", typeof(GameObject))) as GameObject;
        caballoBlanco.tag = "Blancas";
        caballoBlanco.name = "CaballoBlanco";
        tileAux = tiles["tile3-0-0"];
        caballoBlanco.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 1.25F, tileAux.transform.position.z);

        GameObject caballoNegro = Instantiate(Resources.Load("Caballo", typeof(GameObject))) as GameObject;
        caballoNegro.tag = "Negras";
        caballoNegro.name = "CaballoNegro";
        tileAux = tiles["tile3-4-2"];
        caballoNegro.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 1.25F, tileAux.transform.position.z);


        //Gemelos chungers
        GameObject gemeloBlanco1 = Instantiate(Resources.Load("Gemelo", typeof(GameObject))) as GameObject;
        gemeloBlanco1.tag = "Blancas";
        gemeloBlanco1.name = "GemeloBlanco1";
        tileAux = tiles["tile3-4-0"];
        gemeloBlanco1.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);

        GameObject gemeloBlanco2 = Instantiate(Resources.Load("Gemelo", typeof(GameObject))) as GameObject;
        gemeloBlanco2.tag = "Blancas";
        gemeloBlanco2.name = "GemeloBlanco2";
        tileAux = tiles["tile0-4-1"];
        gemeloBlanco2.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);


        GameObject gemeloNegro1 = Instantiate(Resources.Load("Gemelo", typeof(GameObject))) as GameObject;
        gemeloNegro1.tag = "Negras";
        gemeloNegro1.name = "GemeloNegro1";
        tileAux = tiles["tile3-0-2"];
        gemeloNegro1.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);

        GameObject gemeloNegro2 = Instantiate(Resources.Load("Gemelo", typeof(GameObject))) as GameObject;
        gemeloNegro2.tag = "Negras";
        gemeloNegro2.name = "GemeloNegro2";
        tileAux = tiles["tile4-0-1"];
        gemeloNegro2.transform.position = new Vector3(tileAux.transform.position.x, tileAux.transform.position.y + 0.8F, tileAux.transform.position.z);

    }

}

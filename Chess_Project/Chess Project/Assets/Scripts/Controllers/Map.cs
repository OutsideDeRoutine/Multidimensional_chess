using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject fadeImg;

    public Texture2D cursor;

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

        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        //tiles = new Dictionary<String, GameObject>();
        GenerateMap(sideform.Split('|'));
        StartCoroutine(FadeIn(1.5f));
    }

    IEnumerator FadeIn(float time)
    {
        float currentTime = Time.time;
        fadeImg.GetComponent<CanvasGroup>().alpha = 0;
        while (Time.time - currentTime < time)
        {
            fadeImg.GetComponent<CanvasGroup>().alpha = Mathf.Abs(((Time.time - currentTime) / time)-1);
            yield return new WaitForEndOfFrame();
        }
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
                         tile.GetComponent<TileData>().setCharacter(generateCharAtPos(new Vector3(x, y, z)),true);
                    }
                }
                x++;
            }
            y++;
            x = 0;
        }
        //generamos a los reyes
        //generamos su tile
        //generamos el rey blanco
        GameObject tileRey = Instantiate(Resources.Load("TileKing",typeof(GameObject))) as GameObject;
        tileRey.name = "Tile del rey blanco";
        tileRey.transform.position = new Vector3(-2, 0,2);
        //no nos hace falta la posicion logica
        tileRey.GetComponent<TileData>().setCharacter(generateCharAtPos(new Vector3(-1,0,0)),true);


        //generamos el rey negro
        tileRey = Instantiate(Resources.Load("TileKing", typeof(GameObject))) as GameObject;
        tileRey.name = "Tile del rey negro";
        tileRey.transform.position = new Vector3(12, -10, 2);
        //no nos hace falta la posicion logica
        tileRey.GetComponent<TileData>().setCharacter(generateCharAtPos(new Vector3(11, 2, 0)), true);
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
    }

}

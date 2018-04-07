using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseAll : MonoBehaviour {

    private Renderer rend;
    public Color color=Color.gray;
    private PlayerController player;

    void Start()
    {
        rend = GetComponent<Renderer>();
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnMouseEnter()
    {
        rend.material.color += color;
    }

    void OnMouseExit()
    {
        rend.material.color -= color;
    }

    void OnMouseDown()
    {
        if(this.tag=="Blancas" || this.tag == "Negras")
        {
            player.AssingChar(this.gameObject);
        }
        else if(this.tag == "Tile")
        {
            if (this.GetComponent<TileData>().state == 2 && player.selectedChar!=null)
            {
                //Mover  , resetear tablero , cambiar de jugador
                resetAllTiles();
                GetComponent<TileData>().setCharacter(player.selectedChar);
                player.ChangePlayer();
            }
            else if (this.GetComponent<TileData>().state == 3 && player.selectedChar != null)
            {
                resetAllTiles();
                bool muerto =this.GetComponent<TileData>().character.GetComponent<CharData>().receibeDamage(player.selectedChar.GetComponent<CharData>().damage);
                if (muerto == true)
                {
                    this.GetComponent<TileData>().destroyChar();
                }
                player.ChangePlayer();
            }
            else if (this.GetComponent<TileData>().state == 4 && player.selectedChar != null)
            {
                resetAllTiles();
                /*vamos a comprobar las tres opciones
                 * la primera es si la pieza de la casilla tiene más salud que la que se ha movido
                 * la segunda es si la pieza de la casilla tiene menos salud que la que se ha movido
                 * la tercera es si las dos acaban con la salud a 0
                */
                int vida1 = this.GetComponent<TileData>().character.GetComponent<CharData>().vida;
                int vida2 = player.selectedChar.GetComponent<CharData>().vida;
                bool muerto1 = player.selectedChar.GetComponent<CharData>().receibeDamage(vida1);
                bool muerto2 = this.GetComponent<TileData>().character.GetComponent<CharData>().receibeDamage(vida2);
                if (!muerto2 && muerto1)
                {
                    player.selectedChar.GetComponent<CharData>().tile.GetComponent<TileData>().destroyChar();
                }
                else if (muerto2 && !muerto1)
                {
                    GetComponent<TileData>().destroyChar();
                    GetComponent<TileData>().setCharacter(player.selectedChar);
                }
                else
                {
                    //los dos han muerto
                    GetComponent<TileData>().destroyChar();
                    GetComponent<TileData>().setCharacter(player.selectedChar);
                    GetComponent<TileData>().destroyChar();
                }
                player.ChangePlayer();
            }
        }
    }

    void resetAllTiles()
    {
        ArrayList all = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));
        foreach (GameObject tile in all)
        {
            if (tile.GetComponent<TileData>().state == 2)
            {
                tile.GetComponent<TileData>().unSetMovableTo();
            }
            if (tile.GetComponent<TileData>().state == 3)
            {
                tile.GetComponent<TileData>().unSetAttackTo();
            }
            if(tile.GetComponent<TileData>().state == 4)
            {
                tile.GetComponent<TileData>().unSetHitTo();
            }
        }
    }
}

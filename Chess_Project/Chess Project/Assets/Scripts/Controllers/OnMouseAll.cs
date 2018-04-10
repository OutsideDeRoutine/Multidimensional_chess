using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseAll : MonoBehaviour {

    private Renderer rend;
    public Color color = new Color();
    private PlayerController player;
    public string card;
    public GameObject cardo;
    void Start()
    {
        ColorUtility.TryParseHtmlString("#4D4D4DFF", out color);
        rend = GetComponent<Renderer>();
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (card != "")
        {
            cardo = GameObject.Find(card);
        }
    }

    Coroutine stopMe;

    void OnMouseEnter()
    {
        rend.material.color += color;
        if (cardo != null)
        {
            cardo.GetComponentInChildren<UnityEngine.UI.Text>().text = GetComponent<CharData>().getData();
            if(stopMe!=null) StopCoroutine(stopMe);
            stopMe = StartCoroutine(moveCard());
        }

        //HAY QUE QUITAR ESTO DE AQUI xd
        if (this.tag == "Tile" && this.GetComponent<TileData>().state == 4 && player.selectedChar != null)
        {
            int vida1 = this.GetComponent<TileData>().character.GetComponent<CharData>().vida;
            int vida2 = player.selectedChar.GetComponent<CharData>().vida;

            if (vida1 > vida2)
            {
                player.selectedChar.GetComponent<Renderer>().material.color +=Color.red;
            }
            else if(vida2 > vida1)
            {
                this.GetComponent<TileData>().character.GetComponent<Renderer>().material.color += Color.red;
            }
            else if(vida2 == vida1)
            {
                player.selectedChar.GetComponent<Renderer>().material.color += Color.red;
                this.GetComponent<TileData>().character.GetComponent<Renderer>().material.color += Color.red;
            }
        }
        else if (this.tag == "Tile" && this.GetComponent<TileData>().state == 3 && player.selectedChar != null)
        {
            int vida1 = this.GetComponent<TileData>().character.GetComponent<CharData>().vida;
            int damage = player.selectedChar.GetComponent<CharData>().damage;

            if (vida1 - damage<=0)
            {
                this.GetComponent<TileData>().character.GetComponent<Renderer>().material.color += Color.red;
            }
        }
    }

    void OnMouseExit()
    {
        rend.material.color -= color;
        if (cardo != null)
        {
            if (stopMe != null) StopCoroutine(stopMe);
            stopMe = StartCoroutine(moveCardBack());
        }

        //HAY QUE QUITAR ESTO DE AQUI xd
        if (this.tag == "Tile" && this.GetComponent<TileData>().state == 4 && player.selectedChar != null)
        {
            int vida1 = this.GetComponent<TileData>().character.GetComponent<CharData>().vida;
            int vida2 = player.selectedChar.GetComponent<CharData>().vida;

            if (vida1 > vida2)
            {
                player.selectedChar.GetComponent<Renderer>().material.color -= Color.red;
            }
            else if (vida2 > vida1)
            {
                this.GetComponent<TileData>().character.GetComponent<Renderer>().material.color -= Color.red;
            }
            else if (vida2 == vida1)
            {
                player.selectedChar.GetComponent<Renderer>().material.color -= Color.red;
                this.GetComponent<TileData>().character.GetComponent<Renderer>().material.color -= Color.red;
            }
        }
        else if (this.tag == "Tile" && this.GetComponent<TileData>().state == 3 && player.selectedChar != null)
        {
            int vida1 = this.GetComponent<TileData>().character.GetComponent<CharData>().vida;
            int damage = player.selectedChar.GetComponent<CharData>().damage;

            if (vida1 - damage<=0)
            {
                this.GetComponent<TileData>().character.GetComponent<Renderer>().material.color -= Color.red;
            }
        }
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
                    GameObject select=player.selectedChar;
                    StartCoroutine(GetComponent<TileData>().moveChar(select, GetComponent<TileData>().GetPos(select), select.GetComponent<CharData>().tile.GetComponent<TileData>().destroyChar));
                }
                else if (muerto2 && !muerto1)
                {
                    GetComponent<TileData>().destroyChar();
                    GetComponent<TileData>().setCharacter(player.selectedChar);
                }
                else
                {
                    GameObject select = player.selectedChar;
                    StartCoroutine(GetComponent<TileData>().moveChar(select, GetComponent<TileData>().GetPos(select), select.GetComponent<CharData>().tile.GetComponent<TileData>().destroyChar, GetComponent<TileData>().destroyChar));
                }
                player.ChangePlayer();
            }
        }
        else if (this.tag == "TileKing")
        {
            if (this.GetComponent<TileData>().state == 3 && player.selectedChar != null)
            {
                resetAllTiles();
                bool muerto = this.GetComponent<TileData>().character.GetComponent<CharData>().receibeDamage(player.selectedChar.GetComponent<CharData>().damage);
                if (muerto == true)
                {
                    this.GetComponent<TileData>().destroyChar();
                    End();
                }
                player.ChangePlayer();
            }
        }
    }

    void End()
    {
        Application.LoadLevel(Application.loadedLevel);
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

        all = new ArrayList(GameObject.FindGameObjectsWithTag("TileKing"));
        foreach (GameObject tile in all)
        {          
            if (tile.GetComponent<TileData>().state == 3)
            {
                tile.GetComponent<TileData>().unSetAttackTo();
            }         
        }
    }

    IEnumerator moveCard()
    {
        yield return new WaitForSeconds(0.8f);
        cardo.GetComponent<Show>().showing = true;
    }


    IEnumerator moveCardBack()
    {
        yield return new WaitForSeconds(0.8f);
        cardo.GetComponent<Show>().showing = false;
    }

}

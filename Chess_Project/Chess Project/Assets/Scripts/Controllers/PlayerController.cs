﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject image;
    public Sprite whiteImg;
    public Sprite blackImg;

    public bool player; //White false Black true;
    public GameObject selectedChar;

    public Color color= Color.blue;

    public void AssingChar(GameObject character)
    {
        if((character.tag== "Blancas" && !player) || (character.tag == "Negras" && player))
        {
            if (selectedChar != null)  selectedChar.GetComponent<Renderer>().material.color -= color;
            selectedChar = character;
            selectedChar.GetComponent<Renderer>().material.color += color;
            selectedChar.GetComponent<CharData>().canMoveThere.Invoke();
            selectedChar.GetComponent<CharData>().canAttackThere.Invoke();
            selectedChar.GetComponent<CharData>().canHitThere.Invoke();
            selectedChar.GetComponent<CharData>().canHitTheKing.Invoke();
        }
    }

    public void ChangePlayer()
    {
        if (selectedChar != null)
        {
            selectedChar.GetComponent<Renderer>().material.color -= color;
            selectedChar = null;
        }
        player = player? false : true;
        if (player)
        {
            image.GetComponent<Image>().sprite = blackImg;
        }
        else
        {
            image.GetComponent<Image>().sprite = whiteImg;
        }
    }
}

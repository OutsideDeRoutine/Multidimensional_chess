using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public GameObject canvas;


    void Start()
    {
        StartCoroutine(FadeIn(1.5f));
    }
    IEnumerator FadeIn(float time)
    {
        float currentTime = Time.time;
        canvas.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = 0;
        while (Time.time - currentTime < time)
        {
            canvas.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = Mathf.Abs(((Time.time - currentTime) / time));
            yield return new WaitForEndOfFrame();
        }
    }

    public void empezarJuego()
    {
        StartCoroutine(FadeOut("test",1,Color.black));
    }

    IEnumerator FadeOut(string level, float time, Color color)
    {
        float currentTime = Time.time;
        Color bcolor = Camera.main.backgroundColor;
        while (Time.time- currentTime < time)
        {
            canvas.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha =  Mathf.Abs(((Time.time - currentTime) / time)-1);
            Camera.main.backgroundColor= Color.Lerp(bcolor, color, (Time.time - currentTime)/time);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(level);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void changeAbout()
    {
        SceneManager.LoadScene("AboutScene");
    }

    public void changeMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
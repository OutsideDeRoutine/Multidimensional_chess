using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject canvas;

	public void empezarJuego()
    {
        StartCoroutine(Fade("test",1,Color.black));
    }

    IEnumerator Fade(string level, float time, Color color)
    {
        float currentTime = Time.time;
        canvas.GetComponent<Canvas>().enabled=false;
        Color bcolor = Camera.main.backgroundColor;
        while (Time.time- currentTime < time)
        {
            Camera.main.backgroundColor= Color.Lerp(bcolor, color, (Time.time - currentTime)/time);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(level);
    }
}
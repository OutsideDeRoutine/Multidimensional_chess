using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndShow : MonoBehaviour {

    public GameObject WhiteShow;
    public GameObject BlackShow;
    public GameObject TieShow;

    public enum Conditions { White=0, Black=1, Tie=2}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MenuScene");
    }


    public void ShowEnd(Conditions cond)
    {
        switch (cond)
        {
            case Conditions.White:
                ShowMe(WhiteShow);
                break;
            case Conditions.Black:
                ShowMe(BlackShow);
                break;
            case Conditions.Tie:
                ShowMe(TieShow);
                break;
        }
    }

    private void ShowMe(GameObject show)
    {
        StartCoroutine(FadeIn(1.5f,show));
    }

    IEnumerator FadeIn(float time, GameObject canvas)
    {
        float currentTime = Time.time;
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        while (Time.time - currentTime < time)
        {
            canvas.GetComponent<CanvasGroup>().alpha = Mathf.Abs(((Time.time - currentTime) / time));
            yield return new WaitForEndOfFrame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum GameState { PreGame, Playing, Dead, Win };
    public static GameState gameState;

    public GameObject gameStartText, deadText, winText, controlText;
    Image controlsImage;
    bool isFadingControlsImage;

    public bool canRestart;

	// Use this for initialization
	void Start ()
    {
        isFadingControlsImage = false;
        controlsImage = controlText.GetComponent<Image>();
        gameState = GameState.PreGame;
        canRestart = false;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (isFadingControlsImage)
        {
            if (controlsImage.color.a > 0)
            {
                Color c = controlsImage.color;
                c.a -= (2.5f * Time.deltaTime);
                controlsImage.color = c;
            }
        }
	}

    public void ChangeUIFromPreToPlaying()
    {
        gameStartText.SetActive(false);
        deadText.SetActive(false);
        winText.SetActive(false);
        controlText.SetActive(true);
        StartCoroutine(FadeControlsImage());
    }

    public void ChangeUIFromPlayingToWin()
    {
        gameStartText.SetActive(false);
        deadText.SetActive(false);
        winText.SetActive(true);
        controlText.SetActive(false);
    }

    public void ChangeUIFromPlayingToDead()
    {
        gameStartText.SetActive(false);
        deadText.SetActive(true);
        winText.SetActive(false);
        controlText.SetActive(false);
    }

    IEnumerator respawnTimer()
    {
        yield return new WaitForSeconds(1.5f);
        deadText.SetActive(true);
        canRestart = true;
    }

    IEnumerator FadeControlsImage()
    {
        yield return new WaitForSeconds(6);
        isFadingControlsImage = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum GameState { PreGame, Playing, Dead, Win };
    public static GameState gameState;

    public GameObject gameStartText, deadText, winText, controlText;
    Image controlsImage;
    bool isFadingControlsImage;
    float quitTimer;
    float loadlevel1Timer;
    float loadlevel2Timer;
    float loadlevel3Timer;

    public bool canRestart;

	// Use this for initialization
	void Start ()
    {
        isFadingControlsImage = false;
        controlsImage = controlText.GetComponent<Image>();
        gameState = GameState.PreGame;
        canRestart = false;
        quitTimer = 0;
        loadlevel1Timer = loadlevel2Timer = loadlevel3Timer = 0;

        Cursor.visible = false;
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

        // quitting the game
        if (Input.GetKey(KeyCode.Alpha8) && Input.GetKey(KeyCode.Escape))
        {
            quitTimer += Time.deltaTime;
        }
        else
        {
            quitTimer = 0;
        }

        if (quitTimer > 3)
        {
            Application.Quit();
        }

        // loading easiest difficulty
        // LEVELS are loaded in reverse order, so scene 2 = diff 1, scene 0 = lvl 3
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Alpha1))
        {
            loadlevel1Timer += Time.deltaTime;
        }
        else
        {
             loadlevel1Timer = 0;
        }

        if (loadlevel1Timer > 2)
        {
            SceneManager.LoadScene(2);
        }

        // loading medium difficulty
        // LEVELS are loaded in reverse order, so scene 2 = diff 1, scene 0 = lvl 3
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Alpha2))
        {
            loadlevel2Timer += Time.deltaTime;
        }
        else
        {
            loadlevel2Timer = 0;
        }

        if (loadlevel2Timer > 2)
        {
            SceneManager.LoadScene(1);
        }

        // loading hardest difficulty
        // LEVELS are loaded in reverse order, so scene 2 = diff 1, scene 0 = lvl 3
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Alpha3))
        {
            loadlevel3Timer += Time.deltaTime;
        }
        else
        {
            loadlevel3Timer = 0;
        }

        if (loadlevel3Timer > 2)
        {
            SceneManager.LoadScene(0);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState { PreGame, Playing, Dead, Win };
    public static GameState gameState;

    public GameObject gameStartText, deadText, winText;

    public bool canRestart;

	// Use this for initialization
	void Start ()
    {
        gameState = GameState.PreGame;
        canRestart = false;
	}
	
	// Update is called once per frame
	private void Update ()
    {

	}

    public void ChangeUIFromPreToPlaying()
    {
        gameStartText.SetActive(false);
        deadText.SetActive(false);
        winText.SetActive(false);
    }

    public void ChangeUIFromPlayingToWin()
    {
        gameStartText.SetActive(false);
        deadText.SetActive(false);
        winText.SetActive(true);
    }

    public void ChangeUIFromPlayingToDead()
    {
        gameStartText.SetActive(false);
        deadText.SetActive(true);
        winText.SetActive(false);
    }

    IEnumerator respawnTimer()
    {
        yield return new WaitForSeconds(1.5f);
        deadText.SetActive(true);
        canRestart = true;
    }
}

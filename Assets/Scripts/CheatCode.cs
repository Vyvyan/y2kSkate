using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour {

    public string cheatcode;
    public string enteredcode;
    public int index;

    GameManager gameManager;
    public GameObject player, cheatSpawn;

	// Use this for initialization
	void Start ()
    {
        index = 0;
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.gameState == GameManager.GameState.PreGame)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    enteredcode += 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    enteredcode += 2;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    enteredcode += 3;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    enteredcode += 4;
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    enteredcode += 5;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    enteredcode += 6;
                }
                else
                {
                    enteredcode += 0;
                    Debug.Log("Incorrect");
                }


                if (enteredcode[index] == cheatcode[index])
                {
                    Debug.Log("correct!");
                    index++;
                }
                else
                {
                    enteredcode = null;
                    index = 0;
                }

                if (index == cheatcode.Length)
                {
                    Debug.Log("WE'RE CHEATERS!");
                    player.transform.position = cheatSpawn.transform.position;
                    index = 0;
                    enteredcode = null;
                }
            }
        }
    }
}

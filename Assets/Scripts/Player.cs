using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float playerSpeed;
    public bool isGrounded;
    public float jumpPower;
    Rigidbody rb;
    public bool isAlive;
    GameManager gameManager;

    public GameObject sprite_Riding, sprite_Ollie, sprite_Air, Sprite_Dead;
    enum SpriteState { riding, ollie, air, dead};
    SpriteState spriteState;
    public bool isJumping;
    public bool canJump;
    public bool isGrinding;
    public GameObject grindSparks;

    public GameObject deadBoard;

    public int sceneToLoadOnRestart;

	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isAlive = true;
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        spriteState = SpriteState.riding;
        isJumping = false;
        canJump = true;
        isGrinding = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (GameManager.gameState != GameManager.GameState.Win)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(sceneToLoadOnRestart);
            }
        }

        // animation stuff
        if (spriteState == SpriteState.riding)
        {
            sprite_Riding.SetActive(true);
            sprite_Ollie.SetActive(false);
            sprite_Air.SetActive(false);
            Sprite_Dead.SetActive(false);
        }
        else if (spriteState == SpriteState.ollie)
        {
            sprite_Riding.SetActive(false);
            sprite_Ollie.SetActive(true);
            sprite_Air.SetActive(false);
            Sprite_Dead.SetActive(false);
        }
        else if (spriteState == SpriteState.air)
        {
            sprite_Riding.SetActive(false);
            sprite_Ollie.SetActive(false);
            sprite_Air.SetActive(true);
            Sprite_Dead.SetActive(false);
        }
        else if (spriteState == SpriteState.dead)
        {
            sprite_Riding.SetActive(false);
            sprite_Ollie.SetActive(false);
            sprite_Air.SetActive(false);
            Sprite_Dead.SetActive(true);
        }

        // Pre game
        if (GameManager.gameState == GameManager.GameState.PreGame)
        {
            // start game
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gameManager.ChangeUIFromPreToPlaying();
                GameManager.gameState = GameManager.GameState.Playing;
            }
        }

        // we're playing?
        if (GameManager.gameState == GameManager.GameState.Playing)
        {

            // raycast down to see if we're grounded
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            Ray rayToDetectAngleToRotate = new Ray(transform.position, Vector3.down);
            RaycastHit hit2;

            // grounded ray
            if (Physics.Raycast(ray, out hit, 1.6f))
            {
                if (hit.collider.tag == "Ground")
                {
                    isGrounded = true;
                }
            }
            else
            {
                isGrounded = false;

                // changes our sprite
                if (isJumping)
                {
                    spriteState = SpriteState.ollie;
                }
                else
                {
                    spriteState = SpriteState.air;
                }
            }

            // check for rotation, so we can rotate, this ray shoots further than our grounded ray
            if (Physics.Raycast(rayToDetectAngleToRotate, out hit2, 4.6f))
            {
                if (hit2.collider.tag == "Ground")
                {
                    if (hit2.collider)
                    {
                        Debug.Log(hit2.collider.ToString());
                        gameObject.transform.rotation = hit2.collider.gameObject.transform.rotation;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    if (canJump)
                    {
                        rb.velocity = Vector3.zero;
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        StartCoroutine(CanJumper());
                        StartCoroutine(OllieSpriteTimer());
                    }
                }
            }

            if (isGrounded)
            {
                if (!isJumping)
                {
                    spriteState = SpriteState.riding;
                }
            }
        }

        // we're dead
        if (GameManager.gameState == GameManager.GameState.Dead)
        {

            // we dead
            spriteState = SpriteState.dead;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameManager.canRestart)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gameState == GameManager.GameState.Playing)
        {
            if (isAlive)
            {
                rb.MovePosition(transform.position + Vector3.right * (playerSpeed * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isAlive)
            {
                KillUs();
            }
        }

        if (other.gameObject.tag == "WinZone")
        {
            gameManager.ChangeUIFromPlayingToWin();
            rb.useGravity = false;
            rb.isKinematic = true;
            GameManager.gameState = GameManager.GameState.Win;
        }

        if (other.gameObject.name == "RailCollision")
        {
            grindSparks.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "RailCollision")
        {
            grindSparks.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isAlive)
            {
                KillUs();
            }
        }
    }

    void KillUs()
    {
        Debug.Log("we dead");
        isAlive = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * 8, ForceMode.VelocityChange);
        //rb.AddForce(Vector3.forward * 6, ForceMode.VelocityChange);
        rb.AddTorque(Vector3.forward * -100);
        //GetComponent<BoxCollider>().isTrigger = true;
        //rb.MovePosition(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -4));

        // spawn death board
        GameObject db = Instantiate(deadBoard, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
        db.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 100);

        GameManager.gameState = GameManager.GameState.Dead;
        gameManager.ChangeUIFromPlayingToDead();
    }

    IEnumerator OllieSpriteTimer()
    {
        isJumping = true;
        yield return new WaitForSeconds(.25f);
        isJumping = false;
    }

    IEnumerator CanJumper()
    {
        canJump = false;
        yield return new WaitForSeconds(.15f);
        canJump = true;
    }
}

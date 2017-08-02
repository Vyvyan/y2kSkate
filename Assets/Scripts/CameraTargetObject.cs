using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetObject : MonoBehaviour {

    Transform player;
    public float xOffset;
    public float yOffset;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = new Vector3(player.position.x + xOffset, player.position.y + yOffset, 0);
	}
}

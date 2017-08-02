using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualCamFollowScript : MonoBehaviour {

    public float speed;
    public Transform target;
    public float zOffset;
    public float yOffset;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y + yOffset, target.position.z + zOffset), speed * Time.time);
    }
}

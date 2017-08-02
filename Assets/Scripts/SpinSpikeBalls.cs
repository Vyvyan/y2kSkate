using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpikeBalls : MonoBehaviour {

    float randomX, randomY, randomZ;
    Vector3 rotation;

	// Use this for initialization
	void Start ()
    {
        randomX = Random.Range(0, 100);
        randomY = Random.Range(0, 100);
        randomZ = Random.Range(0, 100);

        rotation = new Vector3(randomX, randomY, randomZ);
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Rotate(rotation * Time.deltaTime);
	}
}

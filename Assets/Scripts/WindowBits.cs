using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBits : MonoBehaviour {

    public Rigidbody[] glassRBs;

	// Use this for initialization
	void Start ()
    {
        Vector3 explosionPos = new Vector3(gameObject.transform.position.x - 5, gameObject.transform.position.y, 0);
        foreach (Rigidbody rb in glassRBs)
        {
            rb.gameObject.transform.parent = null;
            if (rb != null)
                rb.AddExplosionForce(800, explosionPos, 200, .5f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    
}

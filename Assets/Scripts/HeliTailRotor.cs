using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliTailRotor : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(transform.up * (speed * Time.deltaTime));
    }
}

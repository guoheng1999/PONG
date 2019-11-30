using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnCollisionEnter2D()
    {
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.Play();
    }
}

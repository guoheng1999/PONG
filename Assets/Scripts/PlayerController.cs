using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public KeyCode upKey;
	public KeyCode downKey;
	public float speed = 14;
	private Rigidbody2D rigidbody2D;
    private AudioSource audio;
    private bool isStop;

    void Start(){
        audio = GetComponent<AudioSource>();
		rigidbody2D = GetComponent<Rigidbody2D>();
        isStop = false;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(upKey)&& !isStop){
			rigidbody2D.velocity = new Vector2(0,speed);
		}
		else if(Input.GetKey(downKey)&& !isStop){
			rigidbody2D.velocity = new Vector2(0,-speed);
		}
		else{
			rigidbody2D.velocity = new Vector2(0,0);
		}
	}
    void OnCollisionEnter2D()
    {
        audio.Play();
    }

    public void PlayerStop()
    {
        isStop = true;
    }

    public void PlayerStart()
    {
        isStop = false;
    }
}

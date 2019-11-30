using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Rigidbody2D rigidbody2D;
    private Vector2 ballVelocityBuff;
 	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
        Goball();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = rigidbody2D.velocity;

		if(velocity.x > -8.2f && velocity.x < 8.2f && velocity.x!=0 ){

			if(velocity.x>0){
				velocity.x = 8.3f;
			}
			else{
				velocity.x = -8.3f;
			}
			rigidbody2D.velocity = velocity;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.tag == "Player"){

			Vector2 velocity = rigidbody2D.velocity;
			velocity.y = (velocity.y + col.rigidbody.velocity.y)/2f ; //取平均速度
			rigidbody2D.velocity = velocity;
		}
        if (col.gameObject.name == "leftWall")
        {
            GameManager.Instance.ChangeScore(col.gameObject.name);
        }
        if (col.gameObject.name == "rightWall")
        {
            GameManager.Instance.ChangeScore(col.gameObject.name);
        }

    }
    public void Reset()
    {
        transform.position = Vector3.zero;
        rigidbody2D.velocity = Vector2.zero;
        Goball();
    }
    void Goball()
    {
        int number = Random.Range(0, 2);
        if (number == 1)
        {
            rigidbody2D.AddForce(new Vector2(100, 0));
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(-100, 0));
        }
    }

    void BallStart()
    {
        rigidbody2D.velocity = ballVelocityBuff;
    }

    void BallStop()
    {
        ballVelocityBuff = rigidbody2D.velocity;
        rigidbody2D.velocity = Vector2.zero;
    }
}

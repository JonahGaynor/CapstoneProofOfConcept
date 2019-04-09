using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionDice : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ScreenBorders" || collision.gameObject.tag == "TransitionDice")
        {
            return;
        }
        else
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), collision.collider);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}

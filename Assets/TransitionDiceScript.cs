using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDiceScript : MonoBehaviour {

    GameObject player;
    Vector3 borders;
    float yDiff;

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(this.gameObject);

    }

    public void CheckBG()
    {
        borders = GameObject.Find("ScreenBorders").transform.position;
        Check();
    }

    public void Check()
    {
        //checks versus the black background's y
        player = GameObject.Find("Player");
        borders = GameObject.Find("ScreenBorders").transform.position;
        transform.position = new Vector3(transform.position.x - borders.x, transform.position.y - borders.y, 0);
        
    }
}

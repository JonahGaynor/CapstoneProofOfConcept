using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDiceScript : MonoBehaviour {

    GameObject player;
    Vector3 borders;
    float yDiff;

    public static bool haveHit = false;
    public static int timesHit = 0;

    public AudioClip mySounds;
    AudioSource myAudio;

    void Start()
    {
        timesHit = 0;
        haveHit = false;
        myAudio = this.GetComponent<AudioSource>();
        //if (Random.value <= 0.08f)
        //{
        //    //int rand = Random.Range(0, mySounds.Length);
        //    myAudio.PlayOneShot(mySounds);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!haveHit && collision.gameObject.tag == "ScreenBorders")
        {
            timesHit++;
            if (timesHit >= 3)
            {
                haveHit = true;
            }
            for (int i = 0; i < 3; i++)
            {
                myAudio.PlayOneShot(mySounds);
            }
        }
        if (Random.value <= 0.01f)
        {
            //int rand = Random.Range(0, mySounds.Length);
            //myAudio.PlayOneShot(mySounds[rand]);
            //myAudio.PlayOneShot(mySounds);
        }
    }

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

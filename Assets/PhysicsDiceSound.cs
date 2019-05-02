using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDiceSound : MonoBehaviour {

    public AudioClip[] mySounds;
    AudioSource myAudio;

	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        int rand = Random.Range(0, mySounds.Length);
        myAudio.PlayOneShot(mySounds[rand]);
    }
}

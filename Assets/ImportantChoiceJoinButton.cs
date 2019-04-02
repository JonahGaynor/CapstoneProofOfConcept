using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantChoiceJoinButton : MonoBehaviour {

    GameObject player;
    public Animator myAnim;

	// Use this for initialization
	void Start () {
        myAnim = GetComponentInParent<Animator>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        myAnim.SetBool("turnOn", false);
        StartCoroutine(PauseToMove());
    }

    IEnumerator PauseToMove()
    {
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume -= 0.01f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume -= 0.01f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume -= 0.01f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume -= 0.01f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume -= 0.01f;
        yield return new WaitForSeconds(1.1f);
        player.GetComponent<ImportantChoicePlayer>().animate = true;

    }
}

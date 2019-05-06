using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantChoiceJoinButton : MonoBehaviour {

    GameObject player;
    public Animator myAnim;

	// Use this for initialization
	void Start () {
        myAnim = GetComponentInParent<Animator>();
        Debug.Log(myAnim);
        player = GameObject.Find("Player");
	}

    public void OnClick()
    {
        myAnim.SetBool("turnOn", false);
        StartCoroutine(PauseToMove());
    }

    IEnumerator PauseToMove()
    {
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        yield return new WaitForSeconds(.1f);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        yield return new WaitForSeconds(1.1f);
        player.GetComponent<ImportantChoicePlayer>().animate = true;

    }
}

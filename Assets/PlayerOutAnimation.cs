using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutAnimation : MonoBehaviour {

    public Sprite[] outSprites;
    public Sprite normalForward;

    GameObject[] tiles;

	// Use this for initialization
	void Start () {
        StartCoroutine(GetIn());
	}
	
    public IEnumerator GetIn()
    {
        this.GetComponentInParent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        this.GetComponentInParent<SpriteRenderer>().enabled = true;
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[4];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[3];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[2];
        yield return new WaitForSeconds(0.1f);
        tiles = GameObject.FindGameObjectsWithTag("BaseTile");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<BaseTileScript>().Check();
        }
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[1];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[0];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = normalForward;

    }

    public IEnumerator GetOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[0];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[1];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[2];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[3];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[4];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().enabled = false;
    }
}

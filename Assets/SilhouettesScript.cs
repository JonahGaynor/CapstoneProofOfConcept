using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouettesScript : MonoBehaviour {

    public Sprite[] mySprites;
    int myNumb = -1;
    float counter;
    public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (counter >= 2f)
        {
            counter = 0;
            StartCoroutine(FadeOut());
        }
	}

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.8f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.6f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.4f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0f);
        yield return new WaitForSeconds(0.4f);
        myNumb++;
        if (myNumb == 3)
        {
            myNumb = 0;
        }
        sr.sprite = mySprites[myNumb];
        sr.color = new Color(255, 255, 255, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.4f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.6f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 0.8f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(255, 255, 255, 1);
    }

}

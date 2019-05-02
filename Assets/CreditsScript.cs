using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {

    public float yVal;

    float scrollSpeed = 0.2f;

    bool zoomOut = false;

	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector2(0, transform.position.y + scrollSpeed);
        if (transform.position.y >= yVal)
        {
            scrollSpeed -= 0.01f;
            if (scrollSpeed < 0)
            {
                scrollSpeed = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StartCoroutine(Quit());
        }
        if (zoomOut)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize * 1.05f;
            if (Camera.main.orthographicSize >= 300)
            {
                Debug.Log("quit");
                Application.Quit();
            }
        }
	}

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.1f);
        zoomOut = true;
    }
}

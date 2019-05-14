using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4SecondPulse : MonoBehaviour {

    bool grow = true;

    float xScaleStart;
    float yScaleStart;

    float targetXScale;
    float targetYScale;

    // Use this for initialization
    void Start () {
        xScaleStart = transform.lossyScale.x;
        yScaleStart = transform.lossyScale.y;
        targetXScale = xScaleStart * 1.25f;
        targetYScale = yScaleStart * 1.25f;
	}
	
	// Update is called once per frame
	void Update () {
        if (grow)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f, 0);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.02f, transform.localScale.y - 0.02f, 0);
        }
        if (transform.localScale.x >= targetXScale || transform.localScale.y >= targetYScale)
        {
            grow = false;
        }
        if (transform.localScale.x <= xScaleStart || transform.localScale.y <= yScaleStart)
        {
            grow = true;
        }
    }

    public void Reset()
    {
        transform.localScale = new Vector3(xScaleStart, yScaleStart, 0);
    }
}

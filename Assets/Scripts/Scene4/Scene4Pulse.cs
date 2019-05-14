using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene4Pulse : MonoBehaviour {

    public static Scene4Pulse instance;

    float xScaleStart;
    float yScaleStart;

    float targetXScale;
    float targetYScale;

    public Transform[] gabesTransforms;

    bool grow = true;

    int stepNumber = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
        //xScaleStart = this.GetComponent<Transform>().
        //yScaleStart = this.GetComponent<Transform>().localScale.y;


        transform.position = gabesTransforms[stepNumber].position;
        transform.localScale = gabesTransforms[stepNumber].lossyScale;
        xScaleStart = gabesTransforms[stepNumber].lossyScale.x;
        yScaleStart = gabesTransforms[stepNumber].lossyScale.y;


        targetXScale = xScaleStart * 1.25f;
        targetYScale = yScaleStart * 1.25f;
    }
	
	// Update is called once per frame
	void Update () {
	    if (grow) {
            transform.localScale = new Vector3(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f, 0);
        } else {
            transform.localScale = new Vector3(transform.localScale.x - 0.02f, transform.localScale.y - 0.02f, 0);
        }
        if (transform.localScale.x >= targetXScale || transform.localScale.y >= targetYScale) {
            grow = false;
        }
        if (transform.localScale.x <= xScaleStart || transform.localScale.y <= yScaleStart) {
            grow = true;
        }

    }

    public void IncreaseStep() {
        stepNumber++;
        transform.position = gabesTransforms[stepNumber].position;
        transform.localScale = gabesTransforms[stepNumber].lossyScale;
        xScaleStart = gabesTransforms[stepNumber].lossyScale.x;
        yScaleStart = gabesTransforms[stepNumber].lossyScale.y;
        targetXScale = xScaleStart * 1.25f;
        targetYScale = yScaleStart * 1.25f;
    }
}

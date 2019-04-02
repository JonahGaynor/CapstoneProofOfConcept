using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour {

    public Texture2D cursorTexture;
    public Texture2D cursorTexturePressed;
    Vector2 currentPos;

	// Use this for initialization
	void Start () {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorTexturePressed, Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnMouseEnter()
    {
        Debug.Log("click");
        Cursor.SetCursor(cursorTexturePressed, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}

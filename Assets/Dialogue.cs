using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {

    //public string name;
    public Sprite speakerSprite;

    [TextArea(3, 10)]
    public string[] sentences;

}

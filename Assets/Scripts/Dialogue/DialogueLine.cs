using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public enum SpeakerSide {Left, Right};
    public Sprite speakerSprite;
    public SpeakerSide side;
    [TextArea] public string dialogueText;
    public bool seen = false;

}

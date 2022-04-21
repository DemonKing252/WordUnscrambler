using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterAnswer : MonoBehaviour
{
    public MeshRenderer mesh;
    [SerializeField]
    private LetterBoxController letterBox = null;

    [SerializeField]
    private TextMesh text;

    public int Index = 0;
    public string Text { set { text.gameObject.SetActive( true ); text.text = value; } }

    [System.NonSerialized]
    public bool Occupied = false;

    public bool Overlapped = false;

    // Collision enter and exit has some issues. So im using a box cast instead.
    // The main issue is that if i collide with 2 objects, and exit one of them, collision exit gets called,
    // which causes a problem where the game thinks im placing a letter in an invalid spot when its
    // actually in a valid spot.
    public void OnHitEnter(LetterBoxController letterBox)
    {
        if (Index == letterBox.Index)
        {
            Overlapped = true;
            letterBox.IsOnLetterAnswer = true;
            letterBox.letterAnswer = this;
        }
    }
    public void OnHitExit(LetterBoxController letterBox)
    {
        Overlapped = false;
        letterBox.IsOnLetterAnswer = false;
        letterBox.letterAnswer = null;
        letterBox = null;
    }
}

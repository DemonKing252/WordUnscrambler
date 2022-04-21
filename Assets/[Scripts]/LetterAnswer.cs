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

    public void _OnTriggerEnter(LetterBoxController letterBox)
    {
        //letterBox = other.GetComponent<LetterBoxController>();
        //if (other.CompareTag("LetterBox"))
        //{
            Debug.Log("Letter enter");
            //letterBox.colliderNames.Add(other.name);
            if (Index == letterBox.Index)
            {
                Overlapped = true;
                letterBox.IsOnLetterAnswer = true;
                letterBox.letterAnswer = this;
            }
        //}
    }
    public void _OnTriggerExit(LetterBoxController letterBox)
    {
        //if (other.CompareTag("LetterBox"))
        //{
            //letterBox.colliderNames.Remove(other.name);
            //if (letterBox.colliderNames.Count == 0)
            //{
                Overlapped = false;
                //Debug.Log("Letter exit");
                letterBox.IsOnLetterAnswer = false;
                letterBox.letterAnswer = null;
                letterBox = null;
           // }
        //}
    }
}

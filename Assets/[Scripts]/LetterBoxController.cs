using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBoxController : MonoBehaviour
{
    private Vector3 offsetFromWall;
    private float zOffset;
    private Vector3 originalPosition;
    public int Index = 0;

    [SerializeField] private string character;
    [SerializeField] private TextMesh text;
    public bool IsOnLetterAnswer = false;

    [System.NonSerialized] public LetterAnswer letterAnswer = null;

    [SerializeField]
    private MeshRenderer mesh = null;

    public string Character { get { return character; } set { character = value; text.text = value; } }

    public List<string> colliderNames = new List<string>();
    void OnMouseDown()
    {
        zOffset = Camera.main.WorldToScreenPoint(
        gameObject.transform.position).z;
        offsetFromWall = gameObject.transform.position - GetMouseAsWorldPoint();
    }
    private void OnMouseUpAsButton()
    {
        if (!IsOnLetterAnswer)
            ResetLetterBox();
        else
        {
            if (!letterAnswer.Occupied && Index == letterAnswer.Index)
            {
                letterAnswer.Occupied = true;
                letterAnswer.mesh.enabled = true;
                letterAnswer.Text = Character;
                Character = string.Empty;
                mesh.enabled = false;
                BoardManager.Instance.mTime = 10f;

                bool shouldWin = true;
                foreach(LetterAnswer boxController in BoardManager.Instance.letterAnswers)
                {
                    if (!boxController.Occupied)
                        shouldWin = false;
                }
                if (shouldWin)
                    MenuController.Instance.OnAction(Action.Victory);
            }
            else
            {
                ResetLetterBox();
            }
        }
    }
    private void Awake()
    {

        originalPosition = transform.position;
    }

    private void Start()
    {
    }

    public void ResetLetterBox()
    {
        transform.position = originalPosition;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + offsetFromWall;
    }

}

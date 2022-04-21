using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class LetterBoxController : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private Vector3 offsetFromWall;
    private float zOffset;
    private Vector3 originalPosition;
    public int Index = 0;
    public bool IsHeldByMouse = false;

    [SerializeField] private string character;
    [SerializeField] private TextMesh text;
    public bool IsOnLetterAnswer = false;

    [System.NonSerialized] public LetterAnswer letterAnswer = null;

    [SerializeField]
    private MeshRenderer mesh = null;
    private bool heldDown = false;
    private BoxCollider collider;

    public string Character { get { return character; } set { character = value; text.text = value; } }

    public List<string> colliderNames = new List<string>();

    // Can't use this with the new Input syste, why!!!
    public void _OnMouseDown()
    {
        
        zOffset = Camera.main.WorldToScreenPoint(
        gameObject.transform.position).z;
        offsetFromWall = gameObject.transform.position - GetMouseAsWorldPoint();
    }
    public void _OnMouseUpAsButton()
    {
        Debug.Log("Button pressed.");

        if (!IsOnLetterAnswer)
            ResetLetterBox();
        else
        {
            if (!letterAnswer.Occupied && Index == letterAnswer.Index && letterAnswer.Overlapped)
            {
                letterAnswer.Occupied = true;
                letterAnswer.mesh.enabled = true;
                letterAnswer.Text = Character;
                Character = string.Empty;
                mesh.enabled = false;
                BoardManager.Instance.mTime = 10f;

                BoardManager.Instance.slotSelect.Play();

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
        collider = GetComponent<BoxCollider>();
        originalPosition = transform.position;
    }

    
    private void Update()
    {
        if (IsHeldByMouse)
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, collider.bounds.size * 0.25f, Vector3.forward, transform.rotation, 10f, mask);

            foreach (LetterAnswer box in BoardManager.Instance.letterAnswers)
            {
                box.OnHitExit(this);
            }
            foreach(RaycastHit hit in hits)
            {
                hit.transform.GetComponent<LetterAnswer>().OnHitEnter(this);
            }
        }
    }
    public void ResetLetterBox()
    {
        transform.position = originalPosition;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Mouse.current.position.ReadValue();
        mousePoint.z = zOffset;
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    public void _OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + offsetFromWall;
    }
    
}

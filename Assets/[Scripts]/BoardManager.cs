using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;

public class BoardManager : MonoBehaviour
{
    public delegate void OnKeyPressed(KeyCode key);
    public event OnKeyPressed onKeyPressed;

    [SerializeField]
    public LetterBoxController[] letters;
    [SerializeField]
    public LetterAnswer[] letterAnswers;

    public AudioSource slotSelect;

    private string[] words = {
        "CROWN",
        "STAGS",
        "SPIKE",
        "CLAWS",
        "TEARS",
    };
    private int[] distinctLetters;

    private static BoardManager instance;
    public static BoardManager Instance => instance;
    private float time = 10f;
    [SerializeField] private TMP_Text timeText;
    public float mTime { get { return time; } set { time = Mathf.Max(0f, value); timeText.text = "0:" + time.ToString("00"); if (mTime <= 0f) MenuController.Instance.OnAction(Action.Defeat); } }

    public InputActionAsset asset;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }
    public void Setup()
    {
        string randomWord = words[Random.Range(0, words.Length)];
        distinctLetters = new int[5]
        { -1, -1, -1, -1, -1 };

        int rand;
        for (int i = 0; i < distinctLetters.Length; i++)
        {
            do
            {
                rand = Random.Range(0, 5);

            } while (distinctLetters.Contains(rand));
            distinctLetters[i] = rand;
        }

        for (int i = 0; i < distinctLetters.Length; i++)
        {
            letters[i].Character = randomWord[distinctLetters[i]].ToString();
            letters[i].Index = distinctLetters[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        mTime -= Time.deltaTime;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {

            
            if (hit.collider.gameObject.GetComponent<LetterBoxController>() != null)
            {
                LetterBoxController letter = hit.transform.GetComponent<LetterBoxController>();

                //Debug.Log("Hit on: " + hit.transform.name);
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    letter.IsHeldByMouse = true;
                    letter._OnMouseDown();
                }
                else if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    //Debug.Log("got here: " + letter.name);

                    foreach (LetterBoxController box in letters)
                    {
                        if (box.IsHeldByMouse)
                        {
                            letter.IsHeldByMouse = false;
                            box._OnMouseUpAsButton();
                        }
                    }
                }
            }
        }

        foreach(LetterBoxController box in letters)
        {
            if (box.IsHeldByMouse)
            {
                box._OnMouseDrag();
            }
        }
    }
}

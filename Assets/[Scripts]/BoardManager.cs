using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public LetterBoxController[] letters;
    [SerializeField]
    public LetterAnswer[] letterAnswers;

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

    void Awake()
    {
        instance = this;
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
    }
}

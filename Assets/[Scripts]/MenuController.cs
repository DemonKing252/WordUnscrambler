using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public enum Action
{
    Start        = 0,
    Quit         = 1,
    PlayAgain    = 2,
    MainMenu     = 3,
    Victory      = 4,
    Defeat       = 5,
    Instructions = 6,
    Pause        = 7,
    Resume       = 8,
    Credit       = 9
}
public class MenuController : MonoBehaviour
{
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas instructionsCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas creditCanvas;

    private bool isPaused = false;
    private static MenuController instance;
    public static MenuController Instance => instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // New input system
    public void OnPauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;

            if (isPaused)
                OnAction((int)Action.Pause);
            else
                OnAction((int)Action.Resume);
        }
    }

    public void OnAction(Action action)
    {
        OnAction((int)action);
    }

    public void OnAction(int action)
    {

        switch ((Action)action)
        {
            case Action.Quit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
                break;
            case Action.MainMenu:
                Cursor.lockState = CursorLockMode.None;
                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(false);
                Time.timeScale = 0f;
                SceneManager.LoadScene("Main");
                break;
            case Action.Pause:

                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(true);
                creditCanvas.gameObject.SetActive(false);
                break;
            case Action.Resume:
            
                Time.timeScale = 1f;
                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(false);
                break;
            case Action.Start:
                gameCanvas.gameObject.SetActive(true);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
                BoardManager.Instance.Setup();

                break;
            case Action.Instructions:

                Cursor.lockState = CursorLockMode.None;
                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(true);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(false);
                break;

            case Action.Victory:
                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(true);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(false);
                Time.timeScale = 0f;
                break;
            case Action.Defeat:
                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(true);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(false);
                Time.timeScale = 0f;
                break;
            case Action.PlayAgain:
                SceneManager.LoadScene("Main");

                break;
            case Action.Credit:

                gameCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(false);
                loseCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                creditCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
    }
}

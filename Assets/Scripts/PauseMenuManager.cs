using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Importing InputSystem

public class PauseMenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private bool pauseButtonAlreadyPressed = false;

    PlayerInput playerInput;
    InputAction pauseAction;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        float pausing = pauseAction.ReadValue<float>();
        if (pausing == 1f && !pauseButtonAlreadyPressed)
        {
            pauseButtonAlreadyPressed = true;

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else if (pausing != 1f)
        {
            pauseButtonAlreadyPressed = false;
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // unfreezes the game
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freezes the game
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}

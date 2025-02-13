using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private UIDocument _document;

    private Button _startButton; // Button to restart the game
    private Button _exitButton;  // Button to load the main menu scene

    [Header("Scene Names")]
    [SerializeField] private string _gameSceneName = "Game"; // Name of the game scene
    [SerializeField] private string _menuSceneName = "Main Menu"; // Name of the main menu scene

    private void Awake()
    {
        // Get the UIDocument component
        _document = GetComponent<UIDocument>();

        if (_document == null)
        {
            Debug.LogError("UIDocument component is missing!", this);
            enabled = false; // Disable the script if UIDocument is missing
            return;
        }

        // Get the buttons from the UI Toolkit
        _startButton = _document.rootVisualElement.Q("STARTGAMEBUTTON") as Button;
        _exitButton = _document.rootVisualElement.Q("EXITGAMEBUTTON") as Button;

        // Check if buttons were found
        if (_startButton == null)
        {
            Debug.LogError("Start button not found in UI Toolkit!", this);
        }
        else
        {
            _startButton.RegisterCallback<ClickEvent>(OnStartGameClick);
        }

        if (_exitButton == null)
        {
            Debug.LogError("Exit button not found in UI Toolkit!", this);
        }
        else
        {
            _exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);
        }
    }

    private void OnDisable()
    {
        // Unregister button callbacks to avoid memory leaks
        if (_startButton != null)
        {
            _startButton.UnregisterCallback<ClickEvent>(OnStartGameClick);
        }

        if (_exitButton != null)
        {
            _exitButton.UnregisterCallback<ClickEvent>(OnExitGameClick);
        }
    }

    private void OnStartGameClick(ClickEvent evt)
    {
        // Load the game scene
        if (string.IsNullOrEmpty(_gameSceneName))
        {
            Debug.LogError("Game scene name is not set!", this);
            return;
        }

        SceneManager.LoadScene(_gameSceneName);
    }

    private void OnExitGameClick(ClickEvent evt)
    {
        // Load the main menu scene
        if (string.IsNullOrEmpty(_menuSceneName))
        {
            Debug.LogError("Menu scene name is not set!", this);
            return;
        }

        SceneManager.LoadScene(_menuSceneName);
    }
}
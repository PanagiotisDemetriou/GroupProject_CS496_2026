using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Documents")]
    [SerializeField] private UIDocument pauseMenuDocument;
    [SerializeField] private UIDocument gameHudDocument;

    private VisualElement root;
    private VisualElement pausePanel;
    private VisualElement pauseControlsPanel;

    private Button resumeButton;
    private Button controlsButton;
    private Button backButton;

    private bool gameStarted = false;
    private bool isPaused = false;

    private void Awake()
    {
        if (pauseMenuDocument == null)
            pauseMenuDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if (pauseMenuDocument == null)
        {
            Debug.LogError("PauseMenuController: pauseMenuDocument is NULL");
            return;
        }

        root = pauseMenuDocument.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("PauseMenuController: rootVisualElement is NULL");
            return;
        }

        pausePanel = root.Q<VisualElement>("PausePanel");
        pauseControlsPanel = root.Q<VisualElement>("PauseControlsPanel");

        resumeButton = root.Q<Button>("ResumeButton");
        controlsButton = root.Q<Button>("ControlsButton");
        backButton = root.Q<Button>("PauseBackButton");

        Debug.Log("PauseMenuController: root found = " + (root != null));
        Debug.Log("PauseMenuController: PausePanel found = " + (pausePanel != null));
        Debug.Log("PauseMenuController: PauseControlsPanel found = " + (pauseControlsPanel != null));
        Debug.Log("PauseMenuController: ResumeButton found = " + (resumeButton != null));
        Debug.Log("PauseMenuController: ControlsButton found = " + (controlsButton != null));
        Debug.Log("PauseMenuController: PauseBackButton found = " + (backButton != null));

        if (resumeButton != null)
            resumeButton.clicked += ResumeGame;

        if (controlsButton != null)
            controlsButton.clicked += ShowControlsPanel;

        if (backButton != null)
            backButton.clicked += ShowMainPausePanel;

        root.style.display = DisplayStyle.None;
        //root.style.display = DisplayStyle.Flex;

        if (pausePanel != null)
            pausePanel.style.display = DisplayStyle.Flex;

        if (pauseControlsPanel != null)
            pauseControlsPanel.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        if (resumeButton != null)
            resumeButton.clicked -= ResumeGame;

        if (controlsButton != null)
            controlsButton.clicked -= ShowControlsPanel;

        if (backButton != null)
            backButton.clicked -= ShowMainPausePanel;
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed. isPaused = " + isPaused);

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void SetGameStarted(bool started)
    {
        gameStarted = started;
        Debug.Log("PauseMenuController: gameStarted = " + gameStarted);
    }

    private void PauseGame()
    {
        Debug.Log("PauseGame() called");

        isPaused = true;
        Time.timeScale = 0f;

        if (root != null)
            root.style.display = DisplayStyle.Flex;

        if (pausePanel != null)
            pausePanel.style.display = DisplayStyle.Flex;

        if (pauseControlsPanel != null)
            pauseControlsPanel.style.display = DisplayStyle.None;

        if (gameHudDocument != null)
            gameHudDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void ResumeGame()
    {
        Debug.Log("ResumeGame() called");

        isPaused = false;
        Time.timeScale = 1f;

        if (root != null)
            root.style.display = DisplayStyle.None;

        if (gameHudDocument != null)
            gameHudDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void ShowControlsPanel()
    {
        Debug.Log("ShowControlsPanel() called");

        if (pausePanel != null)
            pausePanel.style.display = DisplayStyle.None;

        if (pauseControlsPanel != null)
            pauseControlsPanel.style.display = DisplayStyle.Flex;
    }

    private void ShowMainPausePanel()
    {
        Debug.Log("ShowMainPausePanel() called");

        if (pausePanel != null)
            pausePanel.style.display = DisplayStyle.Flex;

        if (pauseControlsPanel != null)
            pauseControlsPanel.style.display = DisplayStyle.None;
    }
}
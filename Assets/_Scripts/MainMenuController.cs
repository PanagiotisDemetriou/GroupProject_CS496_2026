using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public UIDocument menuDocument;
    public UIDocument gameHudDocument;
    public HUDTimer hudTimer;

    private VisualElement mainMenuPanel;
    private VisualElement controlsPanel;

    private Button startButton;
    private Button controlsButton;
    private Button backButton;

    void Awake()
    {
        // Πάγωσε το game αμέσως
        Time.timeScale = 0f;

        // Κρύψε το HUD αμέσως
        if (gameHudDocument != null)
        {
            gameHudDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    void Start()
    {
        var root = menuDocument.rootVisualElement;

        mainMenuPanel = root.Q<VisualElement>("MainMenuPanel");
        controlsPanel = root.Q<VisualElement>("ControlsPanel");

        startButton = root.Q<Button>("StartButton");
        controlsButton = root.Q<Button>("ControlsButton");
        backButton = root.Q<Button>("BackButton");

        if (startButton != null)
            startButton.clicked += OnStartClicked;

        if (controlsButton != null)
            controlsButton.clicked += OnControlsClicked;

        if (backButton != null)
            backButton.clicked += OnBackClicked;

        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.style.display = DisplayStyle.Flex;

        if (controlsPanel != null)
            controlsPanel.style.display = DisplayStyle.None;
    }

    void ShowControls()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.style.display = DisplayStyle.None;

        if (controlsPanel != null)
            controlsPanel.style.display = DisplayStyle.Flex;
    }

    void OnStartClicked()
    {
        menuDocument.rootVisualElement.style.display = DisplayStyle.None;

        if (gameHudDocument != null)
            gameHudDocument.rootVisualElement.style.display = DisplayStyle.Flex;

        if (hudTimer != null)
            hudTimer.StartTimer();

        Time.timeScale = 1f;
    }

    void OnControlsClicked()
    {
        ShowControls();
    }

    void OnBackClicked()
    {
        ShowMainMenu();
    }
}
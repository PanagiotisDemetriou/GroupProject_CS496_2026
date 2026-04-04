using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverController : MonoBehaviour
{
    [Header("UI Documents")]
    [SerializeField] private UIDocument gameOverDocument;
    [SerializeField] private UIDocument gameHudDocument;
    [SerializeField] private UIDocument pauseMenuDocument;
    [SerializeField] private HUDTimer hudTimer;

    private VisualElement root;
    private Button restartButton;

    private bool gameOverShown = false;

    private void Awake()
    {
        if (gameOverDocument == null)
            gameOverDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if (gameOverDocument == null)
            return;

        root = gameOverDocument.rootVisualElement;
        restartButton = root.Q<Button>("RestartButton");

        if (restartButton != null)
            restartButton.clicked += RestartGame;

        if (root != null)
            root.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        if (restartButton != null)
            restartButton.clicked -= RestartGame;
    }

   public void ShowGameOver()
{
    if (gameOverShown || root == null)
        return;

    gameOverShown = true;

    if (hudTimer != null)
    {
        hudTimer.StopTimer();
        hudTimer.SaveBestIfNeeded();
    }

    Time.timeScale = 0f;

    if (gameHudDocument != null)
        gameHudDocument.rootVisualElement.style.display = DisplayStyle.None;

    if (pauseMenuDocument != null)
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;

    root.style.display = DisplayStyle.Flex;
}

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
{
    if (Input.GetKeyDown(KeyCode.G))
    {
        ShowGameOver();
    }
}
}
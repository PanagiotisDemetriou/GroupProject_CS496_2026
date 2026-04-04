using UnityEngine;
using UnityEngine.UIElements;

public class HUDTimer : MonoBehaviour
{
    [SerializeField] private UIDocument gameHudDocument;

    private Label timerLabel;
    private Label bestLabel;

    private float elapsedTime = 0f;
    private float bestTime = 0f;

    private bool timerRunning = false;

    private const string BestTimeKey = "BestTime";

    private void Awake()
    {
        if (gameHudDocument == null)
            gameHudDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if (gameHudDocument == null)
            return;

        VisualElement root = gameHudDocument.rootVisualElement;

        timerLabel = root.Q<Label>("TimerLabel");
        bestLabel = root.Q<Label>("BestLabel");

        bestTime = PlayerPrefs.GetFloat(BestTimeKey, 0f);

        UpdateTimerLabel(0f);
        UpdateBestLabelDisplay();
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        elapsedTime += Time.deltaTime;

        UpdateTimerLabel(elapsedTime);
        UpdateBestLabelDisplay();
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        timerRunning = true;

        bestTime = PlayerPrefs.GetFloat(BestTimeKey, 0f);

        UpdateTimerLabel(0f);
        UpdateBestLabelDisplay();
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void SaveBestIfNeeded()
    {
        if (elapsedTime > bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat(BestTimeKey, bestTime);
            PlayerPrefs.Save();
        }
    }

    private void UpdateTimerLabel(float timeValue)
    {
        if (timerLabel != null)
            timerLabel.text = FormatTime(timeValue);
    }

    private void UpdateBestLabelDisplay()
    {
        if (bestLabel == null)
            return;

        if (elapsedTime >= bestTime)
            bestLabel.text = FormatTime(elapsedTime);
        else
            bestLabel.text = FormatTime(bestTime);
    }

    private string FormatTime(float timeValue)
    {
        int minutes = Mathf.FloorToInt(timeValue / 60f);
        int seconds = Mathf.FloorToInt(timeValue % 60f);

        return $"{minutes:00}:{seconds:00}";
    }
}
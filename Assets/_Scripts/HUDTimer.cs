using UnityEngine;
using UnityEngine.UIElements;

public class HUDTimer : MonoBehaviour
{
    public UIDocument uiDocument;

    private Label timerLabel;
    private Label bestLabel;

    private float currentTime = 0f;
    private float bestTime = 0f;

    private bool isNewRecord = false;
    private bool gameRunning = false;   // <- σημαντικό: ξεκινά κλειστό

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("TimerLabel");
        bestLabel = root.Q<Label>("BestLabel");

        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        UpdateUI();
    }

    void Update()
    {
        if (!gameRunning) return;

        currentTime += Time.deltaTime;

        if (currentTime > bestTime)
        {
            isNewRecord = true;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timerLabel != null)
            timerLabel.text = FormatTime(currentTime);

        if (bestLabel != null)
        {
            if (isNewRecord)
                bestLabel.text = FormatTime(currentTime);
            else
                bestLabel.text = FormatTime(bestTime);
        }
    }

    public void StartTimer()
    {
        gameRunning = true;
    }

    public void StopTimer()
    {
        gameRunning = false;

        if (currentTime > bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
            PlayerPrefs.Save();
        }
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        isNewRecord = false;
        gameRunning = false;
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        UpdateUI();
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
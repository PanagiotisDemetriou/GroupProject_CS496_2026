using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public static AlarmController Instance { get; private set; }

    [Header("Alarm Target")]
    [SerializeField] private Transform alarmPoint;

    [Header("Audio")]
    [SerializeField] private AudioSource sirenAudioSource;

    [Header("Agents")]
    [SerializeField] private string agentTag = "Unit";

    public bool IsAlarmActive { get; private set; }
    public Transform AlarmPoint => alarmPoint;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateAlarm()
    {
        if (alarmPoint == null)
        {
            Debug.LogWarning("No alarm point assigned.");
            return;
        }

        IsAlarmActive = true;

        if (sirenAudioSource != null && !sirenAudioSource.isPlaying)
        {
            sirenAudioSource.Play();
        }

        GameObject[] units = GameObject.FindGameObjectsWithTag(agentTag);

        foreach (GameObject unit in units)
        {
            MoveToBoss move = unit.GetComponent<MoveToBoss>();
            if (move != null)
            {
                move.SetOverrideTarget(alarmPoint);
            }
        }

        Debug.Log("Alarm activated: agents moving to alarm point.");
    }

    public void DeactivateAlarm()
    {
        IsAlarmActive = false;

        if (sirenAudioSource != null && sirenAudioSource.isPlaying)
        {
            sirenAudioSource.Stop();
        }

        GameObject[] units = GameObject.FindGameObjectsWithTag(agentTag);

        foreach (GameObject unit in units)
        {
            MoveToBoss move = unit.GetComponent<MoveToBoss>();
            if (move != null)
            {
                move.ClearOverrideTarget();
            }
        }

        Debug.Log("Alarm deactivated: agents return to normal behavior.");
    }
}
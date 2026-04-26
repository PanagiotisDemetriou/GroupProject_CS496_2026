using System.Collections;
using UnityEngine;

public class AlarmButtonInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AlarmController alarmController;
    [SerializeField] private GameObject interactPrompt;

    [Header("Timings")]
    [SerializeField] private float alarmDuration = 15f;
    [SerializeField] private float cooldownDuration = 60f;

    private bool playerInRange = false;
    private bool isOnCooldown = false;

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldown)
        {
            StartCoroutine(ActivateAlarmRoutine());
        }
    }

    private IEnumerator ActivateAlarmRoutine()
    {
        isOnCooldown = true;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        
        if (alarmController != null)
            alarmController.ActivateAlarm();

        Debug.Log("Alarm ACTIVATED.");

        
        yield return new WaitForSeconds(alarmDuration);

        
        if (alarmController != null)
            alarmController.DeactivateAlarm();

        Debug.Log("Alarm OFF. Cooldown started.");

        // Cooldown
        yield return new WaitForSeconds(cooldownDuration);

        isOnCooldown = false;

        if (playerInRange && interactPrompt != null)
            interactPrompt.SetActive(true);

        Debug.Log("Alarm ready again.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = true;

        if (!isOnCooldown && interactPrompt != null)
            interactPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = false;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }
}
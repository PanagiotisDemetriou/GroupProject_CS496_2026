using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class SmokeButtonInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem toxicSmoke;
    [SerializeField] private GameObject interactPrompt;

    [Header("Timings")]
    [SerializeField] private float smokeOnDuration = 10f;
    [SerializeField] private float cooldownDuration = 60f;
    [SerializeField] private Collider smokeBlocker;
    [SerializeField] private NavMeshObstacle smokeObstacle;
    private bool playerInRange = false;
    private bool isOnCooldown = false;

    private void Start()
    {
        // Ensure smoke starts OFF
        if (toxicSmoke != null)
            toxicSmoke.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        if (interactPrompt != null)
            interactPrompt.SetActive(false);
        if (smokeBlocker != null)
            smokeBlocker.enabled = false;
        if (smokeObstacle != null)
            smokeObstacle.enabled = false;
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldown)
        {
            StartCoroutine(ActivateSmokeRoutine());
        }
    }

    private IEnumerator ActivateSmokeRoutine()
    {
        isOnCooldown = true;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        // Turn smoke ON
        if (toxicSmoke != null)
            toxicSmoke.Play();
        // Enable blocking
        if (smokeBlocker != null)
            smokeBlocker.enabled = true;
        if (smokeObstacle != null)
            smokeObstacle.enabled = true;

        Debug.Log("Smoke ON for 10 seconds.");

        // Stay ON for 10 seconds
        yield return new WaitForSeconds(smokeOnDuration);

        // Turn smoke OFF again
        if (toxicSmoke != null)
            toxicSmoke.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        // Disable blocking
        if (smokeBlocker != null)
            smokeBlocker.enabled = false;
        if (smokeObstacle != null)
            smokeObstacle.enabled = false;
        Debug.Log("Smoke OFF. Cooldown started.");

        // Cooldown
        yield return new WaitForSeconds(cooldownDuration);

        isOnCooldown = false;

        if (playerInRange && interactPrompt != null)
            interactPrompt.SetActive(true);

        Debug.Log("Cooldown finished.");
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
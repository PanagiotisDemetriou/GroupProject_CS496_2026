using UnityEngine;

public class PalletInteractTrigger : MonoBehaviour
{
    public PalletInteract parentInteract;
    [SerializeField] private GameObject interactPrompt;
    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentInteract.SetPlayerInRange(true);
            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentInteract.SetPlayerInRange(false);
            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }
}
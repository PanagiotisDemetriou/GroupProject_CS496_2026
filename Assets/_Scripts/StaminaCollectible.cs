using UnityEngine;

public class StaminaCollectible : MonoBehaviour
{
    public float staminaIncrease = 20f;

    private static CollectibleSpawner spawner;

    void Start()
    {
        if (spawner == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Spawner");

            if (obj != null)
                spawner = obj.GetComponent<CollectibleSpawner>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStamina stamina = other.GetComponent<PlayerStamina>();

        if (stamina != null)
        {
            stamina.IncreaseMaxStamina(staminaIncrease);

            if (spawner != null)
                spawner.PlayPickupSound();

            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina = 100f;

    public float drainRate = 25f;
    public float regenRate = 15f;

    private VisualElement staminaFill;

    void Start()
    {
        currentStamina = maxStamina;

        var root = FindFirstObjectByType<UIDocument>().rootVisualElement;
        staminaFill = root.Q<VisualElement>("StaminaBarFill");

        UpdateUI();
    }

    public bool HasStamina()
    {
        return currentStamina > 0f;
    }

    public void Drain()
    {
        currentStamina -= drainRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateUI();
    }

    public void Regen()
    {
        currentStamina += regenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateUI();
    }

    public void IncreaseMaxStamina(float amount)
    {
        maxStamina += amount;
        currentStamina = maxStamina;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (staminaFill == null) return;

        float percent = currentStamina / maxStamina;

        staminaFill.style.width = Length.Percent(percent * 100f);
    }
}
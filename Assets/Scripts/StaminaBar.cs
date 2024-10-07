using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include for using UI components

public class StaminaBar : MonoBehaviour
{
    public float stamina = 100f;
    public float maxStamina = 100f;
    
    private RectTransform rectTransform; // Reference to the RectTransform
    private float initialWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component
        initialWidth = rectTransform.sizeDelta.x; // Store the initial width of the stamina bar
    }

    void Update()
    {
        // Calculate the new width based on current stamina
        float newWidth = initialWidth * (stamina / maxStamina);
        
        // Update the size of the RectTransform to reflect the new width
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y); // Update width, keep height unchanged

    }

    public void SetStamina(float newStamina)
    {
        stamina = Mathf.Clamp(newStamina, 0f, maxStamina); // Ensure stamina is within limits
    }
}

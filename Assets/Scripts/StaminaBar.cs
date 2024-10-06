using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public float stamina = 100f;
    public float maxStamina = 100f;
    SpriteRenderer spriteRenderer;
    float initialWidth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        initialWidth = spriteRenderer.transform.localScale.x;
    }

    void Update()
    {
        spriteRenderer.transform.localScale = new Vector3(initialWidth * stamina / maxStamina, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
    }
}
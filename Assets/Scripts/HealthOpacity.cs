using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOpacity : MonoBehaviour
{
    public Character healthRef;
    public float maxOpacity = 1f;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float opacity = (1 - ((float)healthRef.health / healthRef.maxHealth)) * maxOpacity;
        opacity = Mathf.Clamp(opacity, 0, maxOpacity);  
        spriteRenderer.color = new Color(1, 1, 1, opacity);
    }
}

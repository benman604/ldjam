using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health = 100;
    public const int maxHealth = 100;
    public int attackDamage = 50;

    public List<Weapon> weapons;

    SpriteRenderer spriteRenderer;

    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        Debug.Log(characterName + " took " + damage + " damage! Remaining health: " + health);
        StartCoroutine(FlashSpriteCoroutine());
    }

    IEnumerator FlashSpriteCoroutine() {
        if (spriteRenderer == null) {
            Debug.LogError("SpriteRenderer is null!" + characterName);
            yield break;
        }
        float duration = 1f;
        float flashInterval = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }
        spriteRenderer.enabled = true;
    }

    public void Die() {
        Debug.Log(characterName + " died!");
        Destroy(gameObject);
    }
}

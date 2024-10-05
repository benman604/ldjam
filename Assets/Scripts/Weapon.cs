using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Collider2D attackCollider;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer readyIndicator;
    public string weaponName;
    public int damage = 10;
    public float cooldown = 1f;
    public float attackDuration = 0.2f;

    float lastAttackTime = -100f;

    public void Attack() {
        Debug.Log(weaponName + " attacked!");
        if (Time.time - lastAttackTime < cooldown) {
            Debug.Log("Attack on cooldown!");
            return;
        }
        List<Collider2D> hitColliders = new List<Collider2D>();
        int hitCount = attackCollider.OverlapCollider(new ContactFilter2D(), hitColliders);
        foreach (Collider2D hitCollider in hitColliders) {
            if (hitCollider.gameObject != gameObject) {
                Character character = hitCollider.GetComponent<Character>();
                if (character != null) {
                    character.TakeDamage(damage);
                }
            }
        }
        lastAttackTime = Time.time;
    }

    // Color attackingColor = new Color(1, 0.3f, 0.3f, 1f);
    // Color cooldownColor = new Color(1, 1, 1, 0.1f);
    // Color readyColor = new Color(1, 1, 1, 0f);

    void Update() {
        if (Time.time - lastAttackTime < attackDuration) {
            // spriteRenderer.color = attackingColor;
            spriteRenderer.enabled = true;
        } else {
            spriteRenderer.enabled = false;
            if (Time.time - lastAttackTime < cooldown) {
                // spriteRenderer.color = cooldownColor;
                readyIndicator.enabled = false;
            } else {
                // spriteRenderer.color = readyColor;
                readyIndicator.enabled = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // public Collider2D attackCollider;
    Collider2D attackCollider;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer readyIndicator;
    public GameObject parent;
    public string weaponName;
    public int damage = 10;
    public float cooldown = 0.0000001f;
    public float attackDuration = 0.02f;

    float lastAttackTime = -100f;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider = GetComponent<Collider2D>();
    }

    public void Attack() {
        // Debug.Log(weaponName + " attacked!");
        if (Time.time - lastAttackTime < cooldown) {
            // Debug.Log("Attack on cooldown!");
            return;
        }
        List<Collider2D> hitColliders = new List<Collider2D>();
        int hitCount = attackCollider.OverlapCollider(new ContactFilter2D(), hitColliders);
        foreach (Collider2D hitCollider in hitColliders) {
            if (hitCollider.gameObject != parent) {
                Character character = hitCollider.GetComponent<Character>();
                if (character != null) {
                    character.TakeDamage(damage);
                }
            }
        }

        lastAttackTime = Time.time;
    }

    void Update() {
        if (Time.time - lastAttackTime < attackDuration) {
            spriteRenderer.enabled = true;
        } else {
            spriteRenderer.enabled = false;
            if (readyIndicator != null) {
                if (Time.time - lastAttackTime < cooldown) {
                    readyIndicator.enabled = false;
                } else {
                    readyIndicator.enabled = true;
                }
            }
        }
    }
}

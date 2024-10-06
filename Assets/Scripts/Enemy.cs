using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{   
    public float attackEvery = 2f;
    public float delayBetweenAttacks = 0.5f;
    public Transform playerTransform; // Reference to the player's position
    public float followSpeed = 0.5f;    // Speed at which the enemy follows the player
    public float attackRange = 1.5f;  // Distance at which the enemy stops following and starts attacking


    
    protected override void Start()
    {
        base.Start();
        if (weapons == null || weapons.Count == 0)
        {
            Debug.LogError("No weapons assigned to the enemy!");
            return; 
        }
        InvokeRepeating("RepeatAttack", 1f, attackEvery);
    }

    void RepeatAttack() {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        foreach (Weapon weapon in weapons) {
            weapon.Attack();
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }
    void Update()
    {
        // Move the enemy towards the player if the player is not in attack range
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > attackRange)
        {
            // Face towards the player (rotate only on the Z axis)
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Rotate on Z-axis

            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
        }
    }
}

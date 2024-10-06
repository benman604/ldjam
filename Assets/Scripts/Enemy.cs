using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{   
    public float attackEvery = 2f;
    public float delayBetweenAttacks = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
                // Initialize or ensure the weapons list is populated
        if (weapons == null || weapons.Count == 0)
        {
            Debug.LogError("No weapons assigned to the enemy!");
            return; // Exit to avoid running the attack loop with no weapons
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
}

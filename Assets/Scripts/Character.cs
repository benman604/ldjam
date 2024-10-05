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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        Debug.Log(characterName + " took " + damage + " damage! Remaining health: " + health);
    }

    public void Die()
    {
        Debug.Log(characterName + " died!");
        Destroy(gameObject);
    }
}

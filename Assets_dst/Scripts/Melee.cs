using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    private Collider2D attackCollider;

    protected override void Start()
    {
        base.Start();
        attackCollider = GetComponent<Collider2D>();
    }

    public override void Attack()
    {
        if (!CanAttack())
        {
            Debug.Log("Attack on cooldown!");
            return;
        }

        Debug.Log($"{weaponName} attacked!");

        List<Collider2D> hitColliders = new List<Collider2D>();
        int hitCount = attackCollider.OverlapCollider(new ContactFilter2D(), hitColliders);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != parent)
            {
                Character character = hitCollider.GetComponent<Character>();
                character?.TakeDamage(damage);
            }
        }

        lastAttackTime = Time.time;
    }
}

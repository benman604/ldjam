using UnityEngine;

public class Spit : Weapon
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float maxDistance = 10f;

    public override void Attack()
    {
        if (!CanAttack())
        {
            Debug.Log("Attack on cooldown!");
            return;
        }

        Debug.Log($"{weaponName} spit!");

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectileScript = projectile.AddComponent<Projectile>();
        projectileScript.Initialize(damage, projectileSpeed, maxDistance, parent);

        lastAttackTime = Time.time;
    }
}
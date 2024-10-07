using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public float attackEvery = 2f;
    public float delayBetweenAttacks = 0.5f;
    public float followSpeed = 0.5f;  // Normal speed
    public float attackRange = 1.5f;  // Distance at which the enemy starts attacking
    public float closeEnough = 0.1f;  // Distance to stop following
    public float lungeSpeed = 16f;     // Speed during lunge
    public float lungeDuration = 0.25f; // How long the lunge lasts
    public float minLungeCooldown = 2f; // Minimum time before next lunge
    public float maxLungeCooldown = 5f; // Maximum time before next lunge
    public SpiderSFX spiderSFX;
    public float timeBetweenSteps = 1.492f;

    private bool isLunging = false;

    [SerializeField] Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        if (weapons == null || weapons.Count == 0)
        {
            Debug.LogError("No weapons assigned to the enemy!");
            return;
        }

        StartCoroutine(PlayAudioIfWalking());
        InvokeRepeating("RepeatAttack", 1f, attackEvery);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = followSpeed;

        StartCoroutine(HandleLunge()); // Start the lunge mechanic
    }

    
    public override void TakeDamage(int damage)
    {
        spiderSFX.PlayDamageSound();

        base.TakeDamage(damage); // Call the base class method
    }

    public override void Die() {
        spiderSFX.PlayDeathSound();
        base.Die();
    }

    void RepeatAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.Attack();
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

    void Update()
    {
        // Move the enemy towards the player if the player is not in attack range
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (!isLunging && distanceToPlayer > closeEnough && distanceToPlayer < attackRange) // Only move if not lunging
        {
            agent.SetDestination(target.position);
        }

        float rotation = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
    }

    IEnumerator PlayAudioIfWalking() {

        spiderSFX.PlayMoveSound();

        yield return new WaitForSeconds(timeBetweenSteps);
        StartCoroutine(PlayAudioIfWalking());
    }

    IEnumerator HandleLunge()
    {
        while (true) // Continuously run this coroutine
        {
            // Wait for a random time before starting the next lunge
            float lungeCooldown = Random.Range(minLungeCooldown, maxLungeCooldown);
            yield return new WaitForSeconds(lungeCooldown);

            // Start lunging
            isLunging = true;
            spiderSFX.PlayDashSound();
            agent.speed = lungeSpeed; // Increase speed during lunge

            // Lunge duration
            yield return new WaitForSeconds(lungeDuration);

            // Reset speed and stop lunging
            agent.speed = followSpeed;
            isLunging = false;

        }
    }
}

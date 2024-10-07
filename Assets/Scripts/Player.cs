using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character
{
    public Rigidbody2D rb;
    public float speed = 1000f;
    Vector2 movement;
    public float rotationDegreesPerSecond = 180f;

    AnimationSwitcher animationSwitcher;

    public float speedSprinting = 9f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public StaminaBar staminaBar;

    public float staminaRegenRate = 0.5f;
    public float staminaSprintingCost = 5f;
    public float notGoingForwardMultiplier = 0.5f;
    public CharacterSFX characterSFX;
    public float timeBetweenSteps = 0.75f;

    float staminaCooldown = 2f;
    bool isSprinting = false;

    // Health Bar UI Reference (now using Slider)
    public Slider healthBar; // Reference to the health bar Slider

    // Flower collection counter
    public int numFlowers = 0; // Counter for the number of flowers

    protected override void Start()
    {
        base.Start();
        // animator = GetComponent<Animator>();
        animationSwitcher = GetComponent<AnimationSwitcher>();
        UpdateHealthBar();
        StartCoroutine(PlayAudioIfWalking());
    }

    IEnumerator PlayAudioIfWalking() {
        Debug.Log(movement);
        if (!(movement.x == 0 && movement.y == 0)) {
            characterSFX.PlayMoveSound();
        }
        yield return new WaitForSeconds(timeBetweenSteps);
        StartCoroutine(PlayAudioIfWalking());
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        Vector2 mouseToChar = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(mouseToChar.y, mouseToChar.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationDegreesPerSecond * Time.deltaTime);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        float movementAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        float angleDiff = Mathf.DeltaAngle(angle, movementAngle);
        bool isMovingForward = Mathf.Abs(angleDiff) >= 90;
        float _speed = speed * (isMovingForward ? notGoingForwardMultiplier : 1f);

        if (movement.magnitude > 0 || transform.rotation != rotation)
        {
            animationSwitcher.SetAnimation("walking");
        }
        else
        {
            animationSwitcher.SetAnimation("idle");
        }

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 targetPosition = transform.position + 0.2f * (mouseWorldPosition - transform.position);
        Camera.main.transform.position = new Vector3(targetPosition.x, targetPosition.y, Camera.main.transform.position.z);

        // Stamina logic
        bool usingStamina = false;
        isSprinting = false;
        if (Input.GetKey(KeyCode.Space) && stamina > 0)
        {
            _speed = speedSprinting * (isMovingForward ? notGoingForwardMultiplier : 1f);
            stamina -= staminaSprintingCost;
            usingStamina = true;
            isSprinting = true;

            // characterSFX.PlayDashSound();
        }

        if (Input.GetKeyDown(KeyCode.Space) && stamina > 0) {
            characterSFX.PlayDashSound();
        }

        rb.velocity = movement * _speed;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (weapons[0].Attack()) {
                characterSFX.PlayBiteSound();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
        {
            if (weapons[1].Attack()) {
                characterSFX.PlayAOESound();
            }
        }

        staminaBar.stamina = stamina;

        if (!usingStamina)
        {
            staminaCooldown -= Time.deltaTime;
            if (staminaCooldown <= 0f && stamina < maxStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
                stamina = Mathf.Min(stamina, maxStamina);
            }
        }
        else
        {
            staminaCooldown = 1f;
        }

        // Update health bar
        UpdateHealthBar();
    }

    // Override TakeDamage to show health updates
    public override void TakeDamage(int damage)
    {
        characterSFX.PlayDamageSound();

        base.TakeDamage(damage); // Call the base class method
        UpdateHealthBar(); // Update the health bar display after taking damage
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)health / maxHealth; // Update health bar Slider based on health
        }
    }

    public override void Die()
    {
        animationSwitcher.SetAnimation("die");
        isDead = true;
        Debug.Log("Game Over!");
        SceneManager.LoadScene("Game Over");
    }

    // Method to increase flower count
    public void IncreaseFlowerCount()
    {
        numFlowers++;
        Debug.Log("Flowers collected: " + numFlowers);

        // Check if 4 flowers are collected and load Victory scene
        if (numFlowers >= 4)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    // Detect collisions with flower sprites
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flower")) // Ensure your flower sprites have this tag
        {
            // Increase the flower count
            IncreaseFlowerCount();
            
            // Destroy the flower GameObject
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI; // Make sure to include this for using UI components

public class Player : Character
{
    public Rigidbody2D rb;
    public float speed = 1000f;
    Vector2 movement;
    public float rotationDegreesPerSecond = 180f;

    // Animator animator;
    AnimationSwitcher animationSwitcher;

    // Stamina Variables
    public float speedSprinting = 9f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public StaminaBar staminaBar;

    public float staminaRegenRate = 0.5f;
    public float staminaSprintingCost = 5f;
    bool isSprinting = false;

    float staminaCooldown = 2f;

    // Health Bar UI Reference (now using Slider)
    public Slider healthBar; // Reference to the health bar Slider

    protected override void Start()
    {
        base.Start();
        // animator = GetComponent<Animator>();
        animationSwitcher = GetComponent<AnimationSwitcher>();
        UpdateHealthBar();
    }

    void Update()
    {
        if (isDead) {
            return;
        }
        Vector2 mouseToChar = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(mouseToChar.y, mouseToChar.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationDegreesPerSecond * Time.deltaTime);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.magnitude > 0 || transform.rotation != rotation) {
            // animator.speed = 1;
            animationSwitcher.SetAnimation("walking");
        } else {
            // animator.speed = 0;
            animationSwitcher.SetAnimation("idle");
        }

        // if (transform.rotation != rotation) {
        //     animator.speed = 2;
        // }

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane; 
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 targetPosition = transform.position + 0.2f * (mouseWorldPosition - transform.position);
        Camera.main.transform.position = new Vector3(targetPosition.x, targetPosition.y, Camera.main.transform.position.z);

        // Stamina logic
        bool usingStamina = false;
        float _speed = speed;
        isSprinting = false;
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            _speed = speedSprinting;
            stamina -= staminaSprintingCost;
            usingStamina = true;
            isSprinting = true;
        }

        rb.velocity = movement * _speed;

        if (Input.GetMouseButtonDown(0)) 
        {
            weapons[0].Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            weapons[1].Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            weapons[2].Attack();
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

    public override void Die() {
        animationSwitcher.SetAnimation("die");
        isDead = true;
        Debug.Log("Game Over!");
        SceneManager.LoadScene("Game Over");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public float speed = 1000f;
    Vector2 movement;
    public float smoothingFactor = 0.1f;

    Animator animator;

    public float speedSprinting = 9f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public StaminaBar staminaBar;

    public float staminaRegenRate = 0.5f;
    public float staminaSprintingCost = 5f;
    public float staminaDodgeCost = 50f;

    float staminaCooldown = 2f;

    public float dodgeSpeed = 20f;
    public float dodgeDuration = 0.5f;

    bool isDodging = false;
    float timeSinceLastDodge = 0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseToChar = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(mouseToChar.y, mouseToChar.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smoothingFactor);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.magnitude > 0 || Quaternion.Angle(rotation, transform.rotation) >= 1) {
            animator.speed = 1;
        } else {
            animator.speed = 0;
        }

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane; 
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 targetPosition = transform.position + 0.2f * (mouseWorldPosition - transform.position);
        Camera.main.transform.position = new Vector3(targetPosition.x, targetPosition.y, Camera.main.transform.position.z);

        bool usingStamina = false;
        float _speed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) {
            _speed = speedSprinting;
            stamina -= staminaSprintingCost;
            usingStamina = true;
        }

        rb.velocity = movement * _speed;

        if (Input.GetMouseButtonDown(0)) {
            weapons[1].Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            weapons[0].Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            weapons[2].Attack();
        }

        staminaBar.stamina = stamina;

        if (!usingStamina)
        {
            staminaCooldown -= Time.deltaTime; // Reduce cooldown time
            if (staminaCooldown <= 0f && stamina < maxStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
                stamina = Mathf.Min(stamina, maxStamina); // Clamp to max stamina
            }
        }
        else
        {
            staminaCooldown = 2f; // Set cooldown duration after sprinting
        }

    }

    void FixedUpdate()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public float speed = 3f;
    public float speedSprinting = 9f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public StaminaBar staminaBar;
    Vector2 movement;
    public float smoothingFactor = 0.1f;

    Animator animator;

    public float staminaRegenRate = 0.03f;
    public float staminaSprintingCost = 0.1f;
    public float staminaDodgeCost = 10f;
    public float minStaminaPercentage = 20;

    public float dodgeSpeed = 10f;
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

        if (!isDodging) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }

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
        if (Input.GetKey(KeyCode.LeftShift)) {
            usingStamina = true;
            if (stamina > minStaminaPercentage / 100 * maxStamina) {
                _speed = speedSprinting;
                if (movement.magnitude > 0) {
                    stamina -= staminaSprintingCost;
                }
            }
        }

        if (isDodging) {
            _speed = dodgeSpeed;
            timeSinceLastDodge += Time.deltaTime;
        }

        if (timeSinceLastDodge >= dodgeDuration) {
            isDodging = false;
            timeSinceLastDodge = 0f;
        }

        rb.velocity = movement * _speed;

        if (Input.GetMouseButtonDown(0)) {
            weapons[1].Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            weapons[0].Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            usingStamina = true;
            if (stamina >= minStaminaPercentage / 100 * maxStamina) {
                // get current rotation forward
                Vector2 dodgeDirection = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
                rb.velocity = dodgeDirection * dodgeSpeed;
                isDodging = true;
                stamina -= staminaDodgeCost;
            }
        }

        staminaBar.stamina = stamina;
        if (stamina < maxStamina && !usingStamina) {
            stamina += staminaRegenRate;
        }
    }

    void FixedUpdate()
    {

    }
}
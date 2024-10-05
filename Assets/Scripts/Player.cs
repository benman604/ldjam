using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public float speed = 5f;
    Vector2 movement;
    public float smoothingFactor = 0.1f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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

        if (movement.magnitude > 0 || (Quaternion.Angle(rotation, transform.rotation) >= -5 && Quaternion.Angle(rotation, transform.rotation) <= 5)) {
            animator.speed = 1;
        } else {
            animator.speed = 0;
        }

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane; 
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 targetPosition = transform.position + 0.2f * (mouseWorldPosition - transform.position);
        Camera.main.transform.position = new Vector3(targetPosition.x, targetPosition.y, Camera.main.transform.position.z);

        rb.velocity = movement * speed;

        if (Input.GetMouseButtonDown(0)) {
            weapons[0].Attack();
        }
    }

    void FixedUpdate()
    {

    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private float speed;
    private float maxDistance;
    private Vector3 initialPosition;
    private GameObject owner;

    public void Initialize(int damage, float speed, float maxDistance, GameObject owner)
    {
        this.damage = damage;
        this.speed = speed;
        this.maxDistance = maxDistance;
        this.owner = owner;
        initialPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != owner)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                Debug.Log("The damage has been dealt by the projectile.");
                character.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
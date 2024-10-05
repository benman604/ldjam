using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage = 10;
    public float cooldown = 0.0000001f;
    public float attackDuration = 0.02f;
    protected float lastAttackTime = -100f;
    
    protected SpriteRenderer spriteRenderer;
    public SpriteRenderer readyIndicator;
    public GameObject parent;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual bool CanAttack()
    {
        return Time.time - lastAttackTime >= cooldown;
    }

    public abstract void Attack();

    protected virtual void Update()
    {
        if (Time.time - lastAttackTime < attackDuration)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            if (readyIndicator != null)
            {
                readyIndicator.enabled = Time.time - lastAttackTime >= cooldown;
            }
        }
    }
}

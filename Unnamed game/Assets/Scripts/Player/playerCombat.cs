using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int damage;
    public LayerMask enemyLayer;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown;
    [SerializeField] private AudioClip swordAttackSound;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    private Health enemyHealth;
    
    void Update()
    {
             

         cooldownTimer += Time.deltaTime;

        
        if (cooldownTimer >= attackCooldown)
        {                  
            
            if(Input.GetKey(KeyCode.E)){
                SoundManager.instance.PlaySound(swordAttackSound);
                DamageEnemy();
                cooldownTimer = 0;
            } 
            
         }
        

    }

    private bool EnemyInRange()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null){
            if (hit.transform.tag == "brute")
                enemyHealth = hit.transform.GetComponentInChildren<Health>();
            else
                enemyHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void DamageEnemy()
    {
        anim.SetTrigger("attack");
        if (EnemyInRange())
            enemyHealth.TakeDamage(damage);
         
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


}

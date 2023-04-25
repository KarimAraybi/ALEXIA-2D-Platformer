using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private Animator anim;
    private bool hit;
    [SerializeField]private float activeTime;

    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player"){
            StartCoroutine(activateSpikeTrap());
            col.GetComponent<Health>().TakeDamage(damage);
            

        }
        
    }


    

    private IEnumerator activateSpikeTrap(){
        hit = true;
        anim.SetBool("hit", hit);
        yield return new WaitForSeconds(activeTime);
        hit = false;
        anim.SetBool("hit", hit);
    }


}

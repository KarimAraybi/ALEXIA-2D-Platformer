using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private Health playerHealth;
    [SerializeField] private AudioClip pickupSound;
    private float hp;


    
    void Update()
    {
        hp = playerHealth.currentHealth;
    }


    private void OnTriggerEnter2D(Collider2D col){

        
        if(col.tag == "Player"){
            
            
            if(hp < 3){
                SoundManager.instance.PlaySound(pickupSound);
                col.GetComponent<Health>().AddHealth(healthValue);
                gameObject.SetActive(false);
            }
            
        }

    }



}

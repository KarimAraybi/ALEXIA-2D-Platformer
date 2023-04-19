using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private Health playerHealth;
    private float hp;


    
    void Update()
    {
        hp = playerHealth.currentHealth;
    }


    private void OnTriggerEnter2D(Collider2D col){

        
        if(col.tag == "Player"){
            col.GetComponent<Health>().AddHealth(healthValue);
            
            if(hp < 3){
                gameObject.SetActive(false);
            }
            
        }

    }



}

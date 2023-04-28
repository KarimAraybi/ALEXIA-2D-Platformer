using UnityEngine;

public class Enemy_SideWays : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    public void Awake()
    {
        leftEdge = transform.position.x - distance;
        rightEdge = transform.position.x + distance;   
    }

    public void Update(){
        if(movingLeft){
            if(transform.position.x > leftEdge){
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y,transform.position.z);
            }
            else{
                movingLeft = false;
            }
        }
        else{
            
            if(transform.position.x < rightEdge){
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y,transform.position.z);
            }
            else{
                movingLeft = true;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D col){
        
        if(col.tag == "Player"){
            col.GetComponent<Health>().TakeDamage(damage);
            
        }

    }
}


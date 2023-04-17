
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator anim;
    private bool grounded;
    private bool fall;
    private bool startFall;
    private GameObject playerObj = null;
    private float currentY;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;
    
    
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
        playerObj = GameObject.Find("Player");
        boxCollider = GetComponent<BoxCollider2D>();    
    }
    
    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);

         if (horizontalInput > 0.01f && !onWall())
            transform.localScale = new Vector3(5,5,5);
        else if (horizontalInput < -0.01f && !onWall())
            transform.localScale = new Vector3(-5, 5, 5);
        
         

         if(isGrounded() || onWall()){
            fall = false;
            currentY = playerObj.transform.position.y;
         }

         if(wallJumpCoolDown>0.2f){
            

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else{
                body.gravityScale = 5;
            }

            if(Input.GetKey(KeyCode.Space)){
                Jump();
            }

         }
         else{
            wallJumpCoolDown += Time.deltaTime;
         }

         if(playerObj.transform.position.y > currentY + 1.9){
            startFall = true;
         }
         else{
            startFall = false;
         }

         if(playerObj.transform.position.y >= currentY+ 2.1 || !onWall()){
            fall = true;
         }
         
        

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("fall", fall);
        anim.SetBool("startFall", startFall);
        anim.SetBool("walled", onWall());
       
    }
        


    private void Jump()
    {
        if(isGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded()){
            
            if(horizontalInput == 0){
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*5, 0);
                //transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y,transform.localScale.z);

            }
            else{
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3, 6);
            }

            wallJumpCoolDown = 0;

        }
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
     private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

   
}


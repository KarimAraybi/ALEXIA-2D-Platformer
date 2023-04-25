
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
    private float JumpCoolDown;
    private float horizontalInput;

    [Header("Coyote time")]
    [SerializeField] private float coyoteTime;
    private float coyotecounter;

    [Header("Multiple jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Multiple jumps")]
    [SerializeField] private float WallJumpX;
    [SerializeField] private int WallJumpY;
    
    
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

         if(JumpCoolDown>0.2f){
            

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
                jumpCounter = extraJumps;
            }
            else{
                body.gravityScale = 5;
                body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);
                
                if(isGrounded()){
                    coyotecounter = coyoteTime;
                    jumpCounter = extraJumps;

                }
                else{
                    
                    coyotecounter -= Time.deltaTime;
                    
                }
            }

            if(Input.GetKeyDown(KeyCode.Space)){
                Jump();
            }
            

         }
         else{
            JumpCoolDown += Time.deltaTime;
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
        if(coyotecounter<=0 && !onWall() && jumpCounter<=0) return;
        
        if(onWall()&&horizontalInput!=0 && Mathf.Sign(horizontalInput) != Mathf.Sign(transform.localScale.x)){
            WallJump();
        }
        else{
            if(isGrounded()){
                
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetTrigger("jump");
            }
            else{
                if(coyotecounter>0){
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                else{
                    if(jumpCounter>0){
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        
                        jumpCounter--;
                    }
                }
            }

            coyotecounter = 0;
        }
        

       
    }
 
    private void WallJump()
    {
        anim.SetTrigger("jump");
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*WallJumpX,WallJumpY));
        JumpCoolDown = 0;
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


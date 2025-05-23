using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //This Script Controls the Player Movement.
    
    [SerializeField]private float movespeed; //Determines the Speed of the Player.
    //Instance Editable in unity thus serializefield applied!

    private Rigidbody2D body;
    //Initialising a Variable body of type RigidBody2D.

    private Animator anim;
    private BoxCollider2D boxcollider;
    [SerializeField] private LayerMask groundlayer;
    private float getXAxis;

    [Header("SFX")]
    [SerializeField] private AudioClip Jumpsound;
    private void Awake()//Similar to Event BeginPlay for this script.
    {
        //Give access to Animator and RigidBody to body & Anim.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    }
    private void Update()//Run Every Frame
    {
        getXAxis = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(getXAxis * movespeed, body.velocity.y);

        
        //Flip Player when move left/Right
        if(getXAxis > 0.01f)//+ve value means player moving Right             
        {
            transform.localScale = new Vector3(2.5f,2.5f,2.5f);
        }
        if (getXAxis < -0.01f)//-ve value means player moving Left               
        {
            transform.localScale = new Vector3(-2.5f,2.5f,2.5f);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())//Jump.
        { 
            Jump();
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded())//To avoid multiple calls of sound 
            {
                SoundManager.instance.PlaySound(Jumpsound);
            }
        }

        //Set Animations
        anim.SetBool("IsWalking?", getXAxis!= 0);
        anim.SetBool("OnGround?", isGrounded());
    
    }
    private void Jump() //Updated Jump Mechanic(Based on Collisions)
    {
        body.velocity = new Vector2(body.velocity.x, movespeed * 1.2f);
        anim.SetTrigger("jump");
    }

    private bool isGrounded()//Checks if player touches ground or not
    {
        //BoxCast Method to Detect Collision
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,Vector2.down,0.1f,groundlayer);//5 Arguments = (Origin, Size,Angle,Direction,Distance,layermask);
        //Layermask with raycasthit tells to focus on that particular layer only and ignore others.        
        return raycasthit.collider != null;
    }


    //For Shooting
    public bool canShoot()
    {
        return (getXAxis == 0 && isGrounded());
    }
}

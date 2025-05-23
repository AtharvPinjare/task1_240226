using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script is for Enemy Patrol from a left Edge to Right Edge!
public class Enemy_Patrol : MonoBehaviour
{
    [Header("Edge Points")]   
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;//To get pos ref of enemy.

    [Header("Movement Parameters")]
    [SerializeField] private float enemy_speed;
    private Vector3 initscale;//Initial scale, for direction of enemy
    private bool Moving_Left;

    [Header("Idle Behaviour")]
    [SerializeField] private float Idle_Duration;//For time in idle state.
    private float Idle_timer;//Keeps Track of idle time.

    [Header("Enemy Animator")]
    [SerializeField]private Animator anim;

    private void Awake()
    {
        initscale = enemy.localScale;
    }
    private void OnDisable()
    {
        //Whenever the patrol is stop, this ensures that the animation of enemy is also changed to idle!
        anim.SetBool("moving", false);
    }

    private void Update()
    {

        if (Moving_Left) 
        {
            if (enemy.position.x >= leftEdge.position.x)//Enemy hasn't reached left edge
            {
                MoveInDirection(-1);    
            }
            else //Change direction to right
            {
                Direction_Change();
            }
        }
        else 
        {
            if (enemy.position.x <= rightEdge.position.x)//Enemy hasn't reached left edge
            {
                MoveInDirection(1);
            }
            else //Change direction to right
            {
                Direction_Change();
            }
        }
    }
    /*
    *if player has to change direction from right to left,
    *it was initially pointing right, so movingleft -> false
    *then it points left, so movingleft ->true
    *thus negate
    */
    private void Direction_Change() 
    {
        anim.SetBool("moving", false);//Sets state from walk to idle
        
        Idle_timer += Time.deltaTime;
        if(Idle_timer > Idle_Duration) 
        {
            Moving_Left = !Moving_Left;
        }
    }
    
    private void MoveInDirection(int _direction) 
    {
        Idle_timer = 0;//Reset the time in every frame.
        
        anim.SetBool("moving", true);
        //Make Enemy Face Direction;
        //Init scale can be +ve or -ve thus to avoid issue we use abs.
        enemy.localScale = new Vector3(Mathf.Abs(initscale.x) * _direction, initscale.y, initscale.z); 



        //Move in that Direction;
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemy_speed,enemy.position.y,enemy.position.z);
    }

}

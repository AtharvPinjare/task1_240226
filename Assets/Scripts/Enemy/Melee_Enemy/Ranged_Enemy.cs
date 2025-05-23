using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Majority elements are same as Melee enemy.
public class Ranged_Enemy : MonoBehaviour
{
    [Header("Enemy Damage")]
    [SerializeField] private float AttackCooldown;//For interval b/w 2 attacks
    [SerializeField] private float damage;
    //Matf.Infinity will enable enemy to attack right away.
    private float cooldowntimer = Mathf.Infinity;//For ensuring if it can attack?

    [Header("Colliders")]
    //Ref of Box Collider for boxcasthit.
    [SerializeField] private BoxCollider2D boxcollider;
    //Ref for Sight Distance
    [SerializeField] private float range;
    //For how faar the raycast box is from player. 
    [SerializeField] private float colliderdistance;

    [Header("Ranged Attacks")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] FireSlashes;
    
    [Header("Enemy Ref")]
    private Animator Anim;
    private Enemy_Patrol enemPatrol;

    [Header("Player Info")]
    //Ref for layer mask in RayCast2D hit.
    [SerializeField] private LayerMask Playerlayer;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        enemPatrol = GetComponentInParent<Enemy_Patrol>();
    }


    private void Update()
    {
        cooldowntimer += Time.deltaTime;

        if (Player_In_Sight())
        {
            //Attack when player is in sight.
            if (cooldowntimer >= AttackCooldown)
            {
                //Attack;
                cooldowntimer = 0;//Reset cooldown timer;
                Anim.SetTrigger("Ranged_Attack");
            }
        }

        if (enemPatrol != null)
        {
            //When player is located, stop the patrol and attack.
            enemPatrol.enabled = !Player_In_Sight();
        }
    }

    private void RangedAttack() //for Attaacking Projectiles
    {
        cooldowntimer = 0;
        //Shoot Projectile
        FireSlashes[findFireballs()].transform.position = firepoint.position;
        //FireSlashes[findFireballs].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int findFireballs() 
    {
        for (int i = 0; i < FireSlashes.Length; i++)
        {
            if (!FireSlashes[i].activeInHierarchy) { return i; }
        }
        return 0;
    }

    private bool Player_In_Sight()//Basically checking whether the player is inside the box collider or not.
    {
        //To determine this, we use raycasthit
        RaycastHit2D hit = Physics2D.BoxCast(boxcollider.bounds.center + (transform.right * range * transform.localScale.x * colliderdistance),
            new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z),
            0, Vector2.left, 0, Playerlayer);

        /*
        *To change the scale of the box, we use vector3.
        *transform.localscale.x because the box should also invert if enemy changes direction.
        *For no tilt in direction and left is just for now.
        *Additional 2 are distance and Layer Mask.
        *If anything is hit in player layer, means player is in sight.
        */

        return hit.collider != null;
    }


    private void OnDrawGizmos()//To visualise the sight perception in realtime
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxcollider.bounds.center + (transform.right * range * transform.localScale.x * colliderdistance),
            new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z));
    }

}

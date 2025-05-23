using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackcooldown;
    
    //Creating Fireballs from ObjectPooling Method.
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] FireBall;

    //For sending the audio clip to sound manager;
    [SerializeField] private AudioClip projectilesound;
    
    private Animator Anim;
    private PlayerMovement playermovement;
    
    private float cooldowntimer = Mathf.Infinity;//To keep track amount of time passed since last shot
    //Also to allow player to attack right away

    private void Awake()
    {
        playermovement = GetComponent<PlayerMovement>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && (cooldowntimer > attackcooldown) && playermovement.canShoot())//GetMouseButtonDown(0) means Left Mouse Button. 
        {
            Attack();
        }
        cooldowntimer += Time.deltaTime;
    }
    
    //Event Called By Animation Event to Time with Animation perfectly.
    private void FireProjectile() 
    {
        
        //Pool Fireball
        FireBall[FindFireball()].transform.position = FirePoint.position;
        FireBall[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private void Attack() 
    {
        /*if (SoundManager.instance != null && projectilesound != null)
            SoundManager.instance.PlaySound(projectilesound);*/
        
        //Debugging Sound.
        if (SoundManager.instance == null)
        {
            Debug.LogError("SoundManager instance is null");
        }
        else if (projectilesound == null)
        {
            Debug.LogError("projectilesound is null");
        }
        else
        {
            Debug.Log("Calling PlaySound with " + projectilesound.name);
            SoundManager.instance.PlaySound(projectilesound);
        }

        Anim.SetTrigger("Attack");
        cooldowntimer = 0;
    }

    private int FindFireball() //Uses object Pool method to fire multiple fireballs.
    {
        for (int i = 0; i < FireBall.Length; i++) 
        {
            if (!FireBall[i].activeInHierarchy) { return i; }//Uses all the fireball in the list of fireball holder we created.
        }
        
        return 0;    
    }
}


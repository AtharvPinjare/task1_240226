using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currenthealth{  get; private set; }//This allows to acces health outside this script.
    //But also allows to set the variable only inside this script.
    private Animator anim;
    private bool dead;

    [Header("Enemy Settings")]
    [SerializeField] private bool isPlayer = false; // Uncheck this for enemy objects


    //To get all the components under the behaviour array.
    [Header("Components")]
    [SerializeField]private Behaviour[] components;

    
    [Header ("Death Sound")]
    [SerializeField] private AudioClip HurtSound;
    [SerializeField] private AudioClip DeathSound;
    
    //Invisible frames to add visual effects;
    [Header("IFrames")]
    [SerializeField] private float iframesdurations;//Defines for how long the player is invincible.
    [SerializeField] private float numberofFlashes;//Denotes for how many times player will flash before death.
    private SpriteRenderer spriteRand;    
    
    private void Awake()
    {
        currenthealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRand = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage) //Function is public, so that other component can impart damage.
    {
        currenthealth = Mathf.Clamp(currenthealth - _damage, 0, startingHealth) ;
        //To avoid negetive value and ensure that Overall health stays between 0-1.
        
        if (currenthealth > 0)
        {
            anim.SetTrigger("Hurt");
            SoundManager.instance.PlaySound(HurtSound);
            //Iframes
            StartCoroutine(Invulnerability());
        }
        else 
        {
            if (!dead) 
            {
                anim.SetTrigger("Die");
     
                //This will disable all the components in the behaviour array allocated.
                foreach (Behaviour component in components) {  component.enabled = false; }
                dead = true;
                SoundManager.instance.PlaySound(DeathSound);
            }
            
            // If this is an enemy, notify the GameManager when it dies
            if (!isPlayer && GameManager.Instance != null)
            {
                GameManager.Instance.OnEnemyDestroyed(this);
            }

        }
    }
    
    //Adding the heath to player through collectible etc.
    public void AddHealth(float _value) 
    {
        currenthealth = Mathf.Clamp(currenthealth + _value, 0, startingHealth);
        StartCoroutine(Healing());
        //anim.SetTrigger("Addon_Health");
    }
    
    //Respwan Function linked to health;
    public void RespawnH() 
    {
        dead = false;
        AddHealth(startingHealth);//As player will respawn bcoz it's health drop to zero.
        
        //Makes sures that the trigger inside the animator is not active
        anim.ResetTrigger("Die");

        anim.Play("Idle");

        StartCoroutine(Invulnerability());//Giving them some shield when respawn, looks good.

        //Also re-enable of the components we deactivated upon death.
        foreach (Behaviour component in components) { component.enabled = true; }

    }



    private IEnumerator Invulnerability() 
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);//true means collisions will be ignored.
        //Invulnerable duration

        for (int i = 0; i < numberofFlashes; i++)
        {
            spriteRand.color = new Color(1, 0, 0, 0.5f);//Changes color to light red.

            yield return new WaitForSeconds(iframesdurations/(numberofFlashes * 2));//Holds the frame to color red for 1 second then returns the control to unity. 

            spriteRand.color = Color.white;

            yield return new WaitForSeconds(iframesdurations / (numberofFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    
    private IEnumerator Healing() 
    {
        for (int i = 0; i < numberofFlashes; i++)
        {
            spriteRand.color = new Color(0, 1, 0, 0.5f);
            yield return new WaitForSeconds(iframesdurations / (numberofFlashes * 2));
            spriteRand.color = Color.white;
            yield return new WaitForSeconds(iframesdurations / (numberofFlashes * 2));
        } 
    }

    private void Disable_character()//Disables the dead character from the scene from anim event. 
    {
        gameObject.SetActive(false);
    }
    
}

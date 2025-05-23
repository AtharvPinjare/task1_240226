using UnityEngine;

public class Moving_Spikes: MonoBehaviour
{
    [SerializeField] private float spike_damage;
    
    //Defines the range of Movement of Spike;
    [SerializeField] private float Movement_Distance;
    
    //For How fast the Spike will move
    [SerializeField]private float spike_speed;
    
    //For keeping the left and Right bounds.
    private float leftedge;
    private float rightedge;

    //To define in which direction the spikes are moving.
    private bool movingleft;
    private void Awake()
    {
        leftedge = transform.position.x - Movement_Distance;
        rightedge = transform.position.x + Movement_Distance;
    }

    private void Update()
    {
        if (movingleft) //Spike Will move Left
        {
            if(transform.position.x > leftedge) 
            {
                transform.position = new Vector3(transform.position.x - spike_speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else { movingleft = false; }
        }
        else //Spike will move right.
        {
            if (transform.position.x < rightedge)
            {
                transform.position = new Vector3(transform.position.x + spike_speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else { movingleft = true; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            collision.GetComponent<Health>().TakeDamage(spike_damage);
        }
    }

}

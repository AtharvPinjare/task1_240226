using UnityEngine;

public class Spike_Trap : MonoBehaviour
{
    [SerializeField] private float spike_damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            collision.GetComponent<Health>().TakeDamage(spike_damage);
        }
    }
}

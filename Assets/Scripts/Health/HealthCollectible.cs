using System.Collections;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float Addon_health;
    [SerializeField] private AudioClip PickupSound;

    private SpriteRenderer SpriteRend;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            collision.GetComponent<Health>().AddHealth(Addon_health);
            SoundManager.instance.PlaySound(PickupSound);
            SpriteRend = collision.GetComponent<SpriteRenderer>();
            //StartCoroutine(Healing());
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Healing() 
    {
        SpriteRend.color = new Color(0, 1, 0, 1);

        yield return new WaitForSeconds(1);

        SpriteRend.color = Color.white;
    }

}


using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed = 5.0f;
    [SerializeField] private float explosionscale = 2.0f;//Just to make the Explosion Bigger!
    [SerializeField] private float ProjectileDamage;
    private bool hit;//To Check if hit or not.

    private BoxCollider2D boxcollider;
    private Animator anim;
    private float direction;//Determines the Direction of the projectile.
    private Vector3 originalscale;//Preserves the original scale of the projectile.

    private float lifetime;//Ensuring projectile dies instead of flying to infinity.
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        originalscale = transform.localScale;
    }

    private void Update()
    {
        if (hit) { return; }
        float movespeed = ProjectileSpeed * Time.deltaTime * direction;
        transform.Translate(movespeed, 0, 0);
        lifetime += Time.deltaTime;
        if(lifetime > 5f) { Deactive();}
    }

    private void OnTriggerEnter2D(Collider2D collision)//To check if collided or not
    {
        hit = true;//Set Hit to True
        boxcollider.enabled = false;//Disabling Box Collider
        transform.localScale = new Vector3(transform.localScale.x * explosionscale, transform.localScale.y * explosionscale, transform.localScale.z * explosionscale);
        
        anim.SetTrigger("Explode");
        
        if(collision.tag == "Enemy") 
        {
            collision.GetComponent<Health>().TakeDamage(ProjectileDamage);
        }
    }

    public void SetDirection(float _direction) //To Judge whether projectile goes left or right.
    {
        lifetime = 0;//Setting back to 0
        direction = _direction;//??
        gameObject.SetActive(true);//??
        hit = false;
        boxcollider.enabled = true;

        /*float localscaleX = transform.localScale.x;
        if(Mathf.Sign(localscaleX) != _direction) 
        {
            localscaleX = -localscaleX;
        }
        transform.localScale = new Vector3(localscaleX, transform.localScale.y, transform.localScale.z);*/

        // Reset to original scale (adjusted for direction)
        transform.localScale = new Vector3(
            Mathf.Abs(originalscale.x) * Mathf.Sign(_direction),
            originalscale.y,
            originalscale.z
        );
    }

    private void Deactive() 
    {
        
        gameObject.SetActive (false);
    }

}

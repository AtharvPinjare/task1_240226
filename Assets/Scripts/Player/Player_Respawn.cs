
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    //Audio to tell player when he reached checkpoint
    [SerializeField] private AudioClip CheckpointAudio;
    private Transform currentCheckpoint;//To store pos of last checkpoint
    private Health Playerhealth;//To reset the health of Playe when he respawns.
    private UIManager Uimanager;//Ref to UI manager Script


    private void Awake()
    {
        Playerhealth = GetComponent<Health>();
        Uimanager = FindObjectOfType<UIManager>();
        //returns first UI manager that unity will find, preffered for single objects with no duplicates.
    }

    public void Respawn() 
    {
        if (currentCheckpoint == null)
        {
            //Show gameover screen
            Uimanager.GameOver();
            return;
        }
            setposition();
    }
    private void setposition() 
    {
        //gameObject.SetActive(true);
        transform.position = currentCheckpoint.position;//Player position to checkpoint position.
        //Restoring health, animation.
        Playerhealth.RespawnH();
    }
    //Activate Checckpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "CheckPoints") //Player is collided with the checkpoint
        {
            Debug.LogWarning("Lawda!");
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(CheckpointAudio);
            
            collision.GetComponent<Collider2D>().enabled = false; //Disable the collider on the checkpoint.
        }
    }

}

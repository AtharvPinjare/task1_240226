using UnityEngine;

public class InLevelDoor : MonoBehaviour
{
    //For Transition in camera.
    [SerializeField] private Transform PreviousRoom;
    [SerializeField] private Transform NextRoom;

    [SerializeField] private CameraController cam;//Reference to Camera!
    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();//To attach Camera to door script.
    }

    private void OnTriggerEnter2D(Collider2D collision)//For triggering collision with transition door
    {
        if(collision.tag == "Player") 
        {
            //Debug.Log("Hello");
            if (collision.transform.position.x < transform.position.x) //Player is left to door.
            {
                cam.MoveToNextRoom(NextRoom);
            }
            else { cam.MoveToNextRoom(PreviousRoom);}
        }
    }

}

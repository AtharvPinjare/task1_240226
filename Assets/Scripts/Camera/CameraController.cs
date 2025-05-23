using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField] private float cameraspeed;
    private float currentPosX;
    private Vector3 Velocity = Vector3.zero;

    //Follow Camera (Traditional Camera)
    [SerializeField] private Transform Player;//This is the object camera has to follow.

    
    [SerializeField] private float aheadDistance;
    [SerializeField] private float camspeed;
    private float lookahead;

    private void Update()
    {
        //Room Camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y,transform.position.z), ref Velocity, cameraspeed);//Smoothing Function, main use case is Camera.   
    
        //Follow Player
        transform.position = new Vector3(Player.position.x + lookahead, transform.position.y, transform.position.z);
        lookahead = Mathf.Lerp(lookahead,(aheadDistance * Player.localScale.x), Time.deltaTime * camspeed);
        //Player.localscale.x = 1 for right, thus camera movers more to the right.
        //Player.localscale.x = -1 for left, thus camera moves more to the left
    }

    public void MoveToNextRoom(Transform _NewRoom) 
    {
        currentPosX = _NewRoom.position.x;//This is because currentPosX is the target.

    }

}

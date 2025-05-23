using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioClip testClip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(testClip, Camera.main.transform.position);
            //Debug.Log("Played test sound at camera position");
        }
    }
}
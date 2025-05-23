using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //To make this script easily accessible and this will be referenced in memory as one instance.
    public static SoundManager instance { get; private set; }
    
    private AudioSource asource;

    private void Awake()
    {
        // Singleton pattern implementation
        if(instance == null) //Checks for duplicate objects.
        {  
            instance = this;  
            DontDestroyOnLoad(gameObject);
            //Doesnot destroy this gameobject when loaded new level
            //Imp if we want to retain the same sound manager in different scenes.
            //Debug.Log("SoundManager initialized");
        }
        else
        {
            //Debug.Log("Additional SoundManager destroyed");
            Destroy(gameObject);
            return;
        }
        //instance = this; 
        asource = GetComponent<AudioSource>();
        // Make sure we have an AudioSource component
        if (asource == null)
        {
            asource = gameObject.AddComponent<AudioSource>();
            //Debug.Log("AudioSource component added to SoundManager");
        }
    }

    
    //Different functions of different scripts will send audio clip to this to be played.
    public void PlaySound(AudioClip _Sound) 
    {
        if (_Sound == null)
        {
            //Debug.LogWarning("Attempted to play a null audio clip");
            return;
        }

        //Debug.Log("Playing sound: " + _Sound.name);
        asource.PlayOneShot(_Sound);//Allows to play thee sound only once!
    }
}

using UnityEngine;

// Skript nicht genutzt im finalen Spiel

public class CS_playSound : MonoBehaviour
{

    public AudioClip SoundToPlay;
    public float volume = 1;
    AudioSource audiodoor;
    public bool played = false;


    // Start is called before the first frame update
    void Start()
    {
        // audiodoor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnTriggerEnter()
    {
        if(!played)
        {
            GetComponent<AudioSource>().PlayOneShot(SoundToPlay, volume);
            played = true;
        }
        UnityEngine.Debug.Log("Test");
    }
    */
}

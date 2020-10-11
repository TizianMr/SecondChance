using UnityEngine;

public class TM_Footsteps : MonoBehaviour
{
    public float audioStepLength = 0.5f; //Abspiellänge der Clips
    public AudioClip [] walkSounds; //Array mit den Laufsounds
    public CharacterController controller; //Unser Spieler

    private float time = 0.5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
           PlayFootsteps();
    }
   
    void PlayFootsteps()
    {
        //Alle 0.5 Sek
        timer = Time.time;
        if (time <= timer)
        {
            if (controller.isGrounded && controller.velocity.magnitude > 0.3)
            {
                //Fußschritte werden abgespielt
                AudioClip clip = walkSounds[Random.Range(0, walkSounds.Length)];
                GetComponent<AudioSource>().PlayOneShot(clip, 0.7f); ;
            }
            time = audioStepLength + timer;
        }
    }

    public void SetAudioStepLength(float amount)
    {
        audioStepLength = amount;
    }
}

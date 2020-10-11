using UnityEngine;


public class SS_Helikopter : MonoBehaviour
{
    GameObject helicopter;
    bool turn = false;
    public int speed = 2000;

    //Audio
    public AudioSource audioSource;
    public AudioClip Engine;
    public float volume = 0.5f;
    public bool played = false;

    void Start()
    {
        helicopter = GameObject.Find("Helikopter");
        audioSource = helicopter.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (turn)
        {
            helicopter.transform.Rotate(new Vector3(0, 1f, 0) * Time.deltaTime * speed);
            Debug.Log("turnbabyturn");
        }

    }

    void OnTriggerEnter()
    {
        turn = true;

        //Play Helicopter Engine Sound
        if (!played)
        {
            audioSource.PlayOneShot(Engine, volume);
            played = true;
        }
    }
}

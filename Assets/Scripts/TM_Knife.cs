using UnityEngine;

public class TM_Knife : MonoBehaviour
{
    Animator anim;
    public AudioClip hit;
    public AudioClip slash;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //Linke Maustaste
        {
            //"Woosh" Sound wird abgespielt
            audioSource.PlayOneShot(slash, 1.5f);
            anim.SetTrigger("Active"); //Messeranimation wird getriggert
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetMouseButtonDown(0)) //Linke Maustaste
        {
            if (collision.transform.CompareTag("Enemy")) //Wenn das Messer auf einen Enemy trifft
            {
                Debug.Log("Hit");
                audioSource.PlayOneShot(hit, 0.1f); //Hit-Sound wird abgespielt
                SS_EnemyAI target = collision.transform.GetComponent<SS_EnemyAI>();
                target.TakeDamage(34); //Damage wird dem Enemy hinzugefügt
            }
        }
    }
}

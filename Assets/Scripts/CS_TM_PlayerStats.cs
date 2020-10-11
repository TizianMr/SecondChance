using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CS_TM_PlayerStats : MonoBehaviour
{
    //Gesundheit Spieler
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject gotHitScreen;

    //Audio
    public AudioClip hitSound;
    public AudioClip dieSound;
    AudioSource audioSource;
    
    //Referenzen Externer Code
    public SS_HealthBar healthBar;
    SS_EnemyAI Enemy;
    

    //Tizian
    GameObject rifle;
    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        //Tizian
        rifle = GameObject.Find("rifle");
        gun = GameObject.Find("Gun");

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();

        GameObject exo = GameObject.FindWithTag("Enemy");
        Enemy = exo.GetComponent<SS_EnemyAI>();
        Enemy.isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Sophia
        if(gotHitScreen != null)
            {
                if(gotHitScreen.GetComponent<Image>().color.a > 0)
                {
                    var color = gotHitScreen.GetComponent<Image>().color;
                    color.a -= 0.08f;

                    gotHitScreen.GetComponent<Image>().color = color;
                }
            }
        
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("hit");
        healthBar.SetHealth(currentHealth);
        DamageScreen();

        audioSource.PlayOneShot(hitSound);

        if (currentHealth <= 0f)
        {
            KillPlayer();
        }
    }

    public void DamageScreen()
    {
        var color = gotHitScreen.GetComponent<Image>().color;
        color.a = 0.8f;

        gotHitScreen.GetComponent<Image>().color = color;
    }

    public void KillPlayer()
    {
        audioSource.PlayOneShot(dieSound, 0.7f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.Debug.Log("You died :(");

        //DeathScreen bei Tod des Players
        SceneManager.LoadScene("DeathScreen");
    }


    void OnTriggerEnter(Collider other)
    {
        //Code Tizian
        if (other.gameObject.CompareTag("Health"))
        {
            Debug.Log("Health");
            Destroy(other.gameObject);
            if(currentHealth <= 70)
            {
                healthBar.SetHealth(currentHealth + 30);
                currentHealth += 30;
            } else
            {
                float newHealth = 100 - currentHealth;
                healthBar.SetHealth(currentHealth + newHealth);
                currentHealth += newHealth;
            }
        }else if (other.gameObject.CompareTag("Ammo")) //Tizian
        {
            int ammo = Random.Range(10, 30);

            Debug.Log("Ammo");
            Destroy(other.gameObject);
            gun.GetComponentInChildren<TM_Gun>().SetmaxAmmo(gun.GetComponentInChildren<TM_Gun>().GetmaxAmmo() +ammo);
            rifle.GetComponentInChildren<TM_Gun>().SetmaxAmmo(rifle.GetComponentInChildren<TM_Gun>().GetmaxAmmo() + ammo);
        }
    }

}

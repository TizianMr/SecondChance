using UnityEngine;

/*
 * Teile des Codes mithilfe des Tutorials von Brackeys
 * https://www.youtube.com/watch?v=THnivyG0Mvo&t=308s
 */

public class TM_Gun : MonoBehaviour
{
    //Munition für die Waffen
    public int magazin; //sollte in Unity gesetzt werden
    public int maxAmmo; //sollte in Unity gesetzt werden
    public SS_AmmoBar ammoBar;

    //Schussgeräusch der Waffen
    public AudioClip fire;
    public AudioClip empty;
    public AudioClip reload;
    AudioSource audioSource;


    //Bewegung der Waffe beim schießen
    public float smoothAmount;
    private Vector3 initialPosition;


    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    private GameObject impactEffect;

    private float nextTimeToFire = 0f;

    //CS Externer Code
    SS_EnemyAI Enemy;

    void Start()
    {

        initialPosition = transform.localPosition;

        audioSource = GetComponent<AudioSource>();

        //CS Externer Code
        GameObject exo = GameObject.FindWithTag("Enemy");
        Enemy = exo.GetComponent<SS_EnemyAI>();
    }


    // Update is called once per frame
    void Update()
    {
        //Überprüfen ob die Waffe ein Sturmgewehr oder eine Pistole ist
        if (this.gameObject.name == "rifle")
        { //Sturmgewehr

            if (Input.GetKeyDown("r"))
            {
                GameObject.Find("AmmoGUI").GetComponent<CS_TM_changeWeapon>().reload(1);
                audioSource.PlayOneShot(reload, 0.4f);
            }

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                //Bewegung der Waffe beim schießen
                Vector3 finalPosition = new Vector3(0, 1, 0);
                transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);


                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else if (this.gameObject.name == "Gun")
        { //Pistole
            if (Input.GetKeyDown("r"))
            {
                GameObject.Find("AmmoGUI").GetComponent<CS_TM_changeWeapon>().reload(2);
                audioSource.PlayOneShot(reload, 0.1f);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                //Bewegung der Waffe beim schießen
                Vector3 finalPosition = new Vector3(0, 1, 0);
                transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);

                Shoot();
            }
        }


    }

    void Shoot()
    {
        RaycastHit hit;

        if (magazin == 0)
        {
            audioSource.PlayOneShot(empty, 0.7f);
        }
        else if (magazin > 0)
        {
            magazin--;
            ammoBar.SetAmmo(magazin);
            audioSource.PlayOneShot(fire, 0.1f);
            muzzleFlash.Play();

            //True, if we hit something
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    impactEffect = GameObject.Find("BulletImpactFleshSmallEffect");
                }
                else
                {
                    impactEffect = null;
                }

                Debug.Log(hit.transform.name);


                //CS Extern Code
                Debug.Log("Exo Health: " + Enemy.health);


                //TM_Target target = hit.transform.GetComponent<TM_Target>();
                SS_EnemyAI target = hit.transform.GetComponent<SS_EnemyAI>();

                if (target != null && target.isDead == false)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.25f);

            }
        }
    }

    public int GetMagazin()
    {
        return magazin;
    }

    public void SetMagazin(int amount)
    {
        magazin = amount;
    }

    public int GetmaxAmmo()
    {
        return maxAmmo;
    }

    public void SetmaxAmmo(int amount)
    {
        maxAmmo = amount;
    }
}

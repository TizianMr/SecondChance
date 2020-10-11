//Teile des Skripts mit Hilfe des Tutorials "Unity FPS Tutorial Enemy AI : C# Script for UFPS!" von Jayanam erstellt, erweitert und ergänzt
//https://www.youtube.com/watch?v=a1Na540p4gU

using UnityEngine;
using UnityEngine.AI;

public class SS_EnemyAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public ParticleSystem muzzleFlash;

    public bool isHit;
    public bool isDead = false;

    //Angriffs und Verfolgungs Entfernungen
    public float attackDistance = 4.0f;
    public float followDistance = 6.0f;

    //Angriffs- und Treffwahrscheinlichkeiten 
    [Range(0.0f, 1.0f)]
    public float attackProbability = 0.5f;

    [Range(0.0f, 1.0f)]
    public float hitAccuracy = 0.5f;

    //Health und Damage
    public float health = 50f;
    public float damagePoints = 10.0f;

    //Audio
    AudioSource audioSource;
    public AudioClip gunSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    void Start()
    {
        player = GameObject.Find("First Person Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        //Bugfix: Enemy fliegt Tod in Luft
        Rigidbody body = GetComponent<Rigidbody>();
        body.drag = Mathf.Infinity;
        body.angularDrag = Mathf.Infinity;
    }

    void Update()
    {
        if (navMeshAgent.enabled)
        {
            float dist = Vector3.Distance(player.transform.position, this.transform.position);  //Distanz zum Player
            bool shoot = false;
            bool follow = (dist < followDistance);  //verfolge Player, wenn Distanz kleiner als festgelegte followDistance

            if (follow)
            {
                navMeshAgent.SetDestination(player.transform.position);
                FacePlayer();

                float random = Random.Range(0.0f, 1.0f);
                if (random > (1.0f - attackProbability) && dist < attackDistance)
                {
                    shoot = true;
                }
            }

            if (!follow || shoot)
            {
                navMeshAgent.SetDestination(transform.position);
            }

            //Animations Parameter
            animator.SetBool("Shoot", shoot);
            animator.SetBool("Run", follow);
        }
    }

    //Enemy nach Player ausrichten
    //Mit Hilfe des Tutorials "ENEMY AI - Making an RPG in Unity (E10)" von Brackeys geschrieben
    //https://youtu.be/xppompv1DBg
    void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Enemy schießt
    public void ShootEvent()
    {
        //Raycast zur Erkennung, ob Player im Blickfeld
        RaycastHit Hit;

        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out Hit, Mathf.Infinity))
        {
            if (Hit.transform.gameObject.GetComponent<SS_PlayerManager>() != null)
            {
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(gunSound);
                }

                //Referenz Externer Code
                CS_TM_PlayerStats playerStat = player.GetComponent<CS_TM_PlayerStats>();

                float random = Random.Range(0.0f, 1.0f);

                if (random > 1.0f - hitAccuracy) //Trefferwahrscheinlichkeit
                {
                    playerStat.TakeDamage(damagePoints);    //Player nimmt Schaden
                }
                muzzleFlash.Play();
            }
            else
            {
                Debug.Log("PLAYER NOT SEEN");
            }
        }

    }

    //Enemy kriegt Schaden
    public void TakeDamage(float amount)
    {
        health -= amount;
        audioSource.PlayOneShot(hitSound);  

        //Enemy stirbt
        if (health <= 0f && !isDead)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            //Nav Mesh und Animationen deaktivieren
            navMeshAgent.enabled = false;

            animator.SetBool("Shoot", false);
            animator.SetBool("Run", false);
            animator.SetTrigger("Die");

            isDead = true;
            Destroy(this.gameObject, 5f);   //nach 5 Sekunden Körper löschen
        }
    }

    //Radius Gizmos der Angriffs und Verfolgungs Entfernungen
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
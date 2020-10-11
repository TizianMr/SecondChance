using UnityEngine;

/*
 * Teile des Codes mithilfe des Tutorials von Brackeys
 * https://www.youtube.com/watch?v=_QajrabyTJc
 * 
 * Teile des Codes mithilfe des Tutorials von Partum Game Tutorials (Fall Damage)
 * https://www.youtube.com/watch?v=ITYARy9dz5E
 * 
 * Teile des Codes inspiriert von Tutorials von Single Sapling Games (Sprinting)
 * https://www.youtube.com/watch?v=JUTFiyBjlnc
 */
public class TM_Movement : MonoBehaviour
{
    public float speed = 6.0f;
    public float sprintSpeed = 10.0f;
    public float stamina = 10f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private TM_Footsteps footsteps;

    //Falling
    public float maxFallForce;
    public float baseFallDamage;
    private float fallForce;
    private CS_TM_PlayerStats playerStats;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        footsteps = GetComponent<TM_Footsteps>();
        playerStats = GetComponent<CS_TM_PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float actualSpeed = 0;

        //Checkt wie "stark" der Player gerade fällt
        //Tutorial
        if(!controller.isGrounded)
        {
            float vY = Mathf.Abs(controller.velocity.y);
            fallForce = vY;
        }

        if (controller.isGrounded)
        {
            //Idee aus Tutorial, angepasst
            //Wenn die fallForce größer ist als die maximale Fall Force, die unser Player aushält, so bekommt der Schaden
            if(fallForce > maxFallForce)
            {
                float damage = Mathf.RoundToInt(fallForce * baseFallDamage);
                fallForce = 0;
                Debug.Log("Took" + damage.ToString() + " of damage");
                playerStats.TakeDamage(damage);
            }

            //Sprintfunktion
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Wenn wir noch Stamina haben wird unser Speed auf sprintSpeed gesetzt
                if(stamina > 0)
                {
                    actualSpeed = sprintSpeed;
                    footsteps.SetAudioStepLength(0.25f); //Schrittgeräusche werden verschnellert.
                    Invoke("TakeStamina", 3.0f);
                } else //Wenn wir keine Stamina mehr haben wird unser Speed auf den normalen Speed gesetzt
                {
                    actualSpeed = speed;
                    footsteps.SetAudioStepLength(0.5f);
                }
            }
            else   //Wenn wir die Sprint Taste loslassen wird unser Speed wieder auf den normalwert gestellt
            {
                actualSpeed = speed;
                footsteps.SetAudioStepLength(0.5f); //Schrittgeräusche werden wieder verlangsamert
                Invoke("AddStamina", 3.0f);
            }
            //Tutorial
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= actualSpeed;

            //Wenn gesprungen wird
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        } else
        {//Tutorial
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }
            moveDirection.y -= gravity * Time.deltaTime;

            controller.Move(moveDirection * Time.deltaTime); //Controller bewegt sich in die moveDirection
    }

    void TakeStamina()
    {
        stamina = 0;
    }

    void AddStamina()
    {
        stamina = 1;
    }
}

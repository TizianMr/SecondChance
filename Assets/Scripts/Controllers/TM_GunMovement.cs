using UnityEngine;

/*
 * Teile des Codes mithilfe des Tutorials von Yaseen Mujahid
 * https://www.youtube.com/watch?v=hifCUD3dATs
 */

public class TM_GunMovement : MonoBehaviour
{
    public float amount;
    public float maxAmount;
    public float smoothAmount;
    public float timeAmount = 6;

    private Vector3 initialPosition;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //timeAmount = GetComponent<TM_IsActive>().amount;
        initialPosition = transform.localPosition;
        player = GameObject.Find("First Person Player");
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Mouse X") * amount;
        float movementY = Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

    void OnEnable()
    {
        float movementX = Input.GetAxis("Mouse X") * amount;
        float movementY = Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);

        if(this.gameObject.name == "Gun")
        {
            timeAmount = player.GetComponent<TM_GunIsActive>().amount;
        } else if(this.gameObject.name == "rifle")
        {
            timeAmount = player.GetComponent<TM_RifleIsActive>().amount;
        } else if(this.gameObject.name == "knife")
        {
            timeAmount = player.GetComponent<TM_KnifeIsActive>().amount;
        }

        Debug.Log("TimeAmout " + timeAmount);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount * timeAmount);
    }
}

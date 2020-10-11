using UnityEngine;

/*
 * Teile des Codes mithilfe des Tutorials von Welton King
 * https://www.youtube.com/watch?v=k11IDExB-oU
 */

public class TM_Look : MonoBehaviour
{
    public Transform player;
    public Transform cam;

    public float sensitivity;

    private Quaternion camCenter;

    // Start is called before the first frame update
    void Start()
    {
        //Hide and Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camCenter = cam.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        SetY();
        SetX();
    }

    void SetY()
    {
        float input = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        Quaternion adjustment = Quaternion.AngleAxis(input, -Vector3.right);
        Quaternion delta = cam.localRotation * adjustment;

        if (Quaternion.Angle(camCenter, delta) < 90)
        {
            cam.localRotation = delta;
        }
    }

    void SetX()
    {
        float input = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        Quaternion adjustment = Quaternion.AngleAxis(input, Vector3.up);
        Quaternion delta = player.localRotation * adjustment;
        player.localRotation = delta;
    }
}

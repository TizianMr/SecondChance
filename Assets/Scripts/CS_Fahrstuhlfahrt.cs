using UnityEngine;
using TMPro;

public class CS_Fahrstuhlfahrt : MonoBehaviour
{

    TextMeshProUGUI i_text;

    bool i_active;
    bool door_close;
    bool button_pressed;

    GameObject right_out_door;
    GameObject left_out_door;
    GameObject right_in_door;
    GameObject left_in_door;

    GameObject left_upper_door;
    GameObject right_upper_door;

    GameObject elevator;


    // Start is called before the first frame update
    void Start()
    {
        button_pressed = false;
        door_close = false;
        i_active = false;

        i_text = GetComponent<TextMeshProUGUI>();
        i_text.text = "";

        right_out_door = GameObject.Find("Cube.027");
        left_out_door = GameObject.Find("Cube.026");
        right_in_door = GameObject.Find("Cube.029");
        left_in_door = GameObject.Find("Cube.028");

        left_upper_door = GameObject.Find("Cube.053");
        right_upper_door = GameObject.Find("Cube.054");

        elevator = GameObject.Find("Elevator");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            if ((i_active == true))
            {
                button_pressed = true;
                door_close = true;
                i_text.text = "";

            }
        if (door_close == true)
        {
            right_out_door.transform.position = Vector3.MoveTowards(right_out_door.transform.position, new Vector3(-9.896f, 1.006553f, 5.524f), 0.01f);
            left_out_door.transform.position = Vector3.MoveTowards(left_out_door.transform.position, new Vector3(-12.264f, 1.006553f, 5.524f), 0.01f);
            left_in_door.transform.position = Vector3.MoveTowards(left_in_door.transform.position, new Vector3(0.3f, 0f, 0f), 0.01f);
            right_in_door.transform.position = Vector3.MoveTowards(right_in_door.transform.position, new Vector3(-0.3f, 0f, 0f), 0.01f);
        }

        if (right_in_door.transform.position.x == -0.3f)
        {
            door_close = false;
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, new Vector3(0f, 3f, 0f), 0.02f);
        }
        if (elevator.transform.position.y == 3)
        {
            right_upper_door.transform.position = Vector3.MoveTowards(right_upper_door.transform.position, new Vector3(-9.902f, 4.068367f, 5.613475f), 0.01f);
            left_upper_door.transform.position = Vector3.MoveTowards(left_upper_door.transform.position, new Vector3(-12.264f, 4.068367f, 5.613475f), 0.01f);
            left_in_door.transform.position = Vector3.MoveTowards(left_in_door.transform.position, new Vector3(-0.45f, 3f, 0f), 0.01f);
            right_in_door.transform.position = Vector3.MoveTowards(right_in_door.transform.position, new Vector3(0.45f, 3f, 0f), 0.01f);
        }
    }

    // Text einblenden
    void OnTriggerEnter(Collider other)
    {

        //fahrstuhl innen
        if ((other.gameObject.name == "First Person Player") && (button_pressed == false))
        {
            i_text.text = "Fahrstuhl\nbetätigen (E)";
            i_active = true;

        }
    }

    // Text ausblenden
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "First Person Player")
        {

            i_text.text = "";
            i_active = false;
        }

    }


}

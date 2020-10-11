using UnityEngine;
using TMPro;

public class CS_Fahrstuhl_Trigger : MonoBehaviour
{
    TextMeshProUGUI f_text;

    public bool door_open;
    bool f_active;

    int dtrig = 0;
    GameObject right_out_door;
    GameObject left_out_door;
    GameObject right_in_door;
    GameObject left_in_door;

    GameObject elevator;

    Vector3 le_i;

    // Start is called before the first frame update
    void Start()
    {

        door_open = false;

        f_active = false;

        f_text = GetComponent<TextMeshProUGUI>();
        f_text.text = "";

        right_out_door = GameObject.Find("Cube.027");
        left_out_door = GameObject.Find("Cube.026");
        left_in_door = GameObject.Find("Cube.028");
        right_in_door = GameObject.Find("Cube.029");

        elevator = GameObject.Find("Elevator");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            if ((door_open == false) && (f_active == true) && (dtrig == 0))
            {
                dtrig = 1;
                door_open = true;
                f_text.text = "";
            }
        if (door_open == true) {
            
            right_out_door.transform.position = Vector3.MoveTowards(right_out_door.transform.position, new Vector3 (-9.896f, 1.006553f, 5.524f), 0.01f);
            left_out_door.transform.position = Vector3.MoveTowards(left_out_door.transform.position, new Vector3(-12.264f, 1.006553f, 5.524f), 0.01f);
            left_in_door.transform.position = Vector3.MoveTowards(left_in_door.transform.position, new Vector3(-0.45f, 0f, 0f), 0.01f);
            right_in_door.transform.position = Vector3.MoveTowards(right_in_door.transform.position, new Vector3(0.45f, 0f, 0f), 0.01f);
            Invoke("end_opening", 4);
        }

    }

    void end_opening()
    {
        door_open = false;
        UnityEngine.Debug.Log(door_open);
    }
    
    void OnTriggerEnter(Collider other)
    { 
        //fahrstuhl außen
        if ((other.gameObject.name == "First Person Player") && (door_open == false) && (dtrig == 0))
        {
            f_text.text = "Fahrstuhl öffnen (E)";
            f_active = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.name == "First Person Player")
        {

            f_text.text = "";
            f_active = false;
        }
        
    }



}

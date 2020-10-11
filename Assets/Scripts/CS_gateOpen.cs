using UnityEngine;
using TMPro;

public class CS_gateOpen : MonoBehaviour
{

    TextMeshProUGUI f_text;

    public bool door_open;
    bool f_active;

    GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        door_open = false;
        f_active = false;

        f_text = GetComponent<TextMeshProUGUI>();
        f_text.text = "";

        gate = GameObject.Find("Cylinder.165");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            if ((door_open == false) && (f_active == true))
            {
                door_open = true;
                f_text.text = "";
            }
        if (door_open == true)
        {
            gate.transform.position = Vector3.MoveTowards(gate.transform.position, new Vector3(20.018f, 2.187329f, -10.00495f), 0.01f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //fahrstuhl außen
        if ((other.gameObject.name == "First Person Player") && (door_open == false))
        {
            f_text.text = "Tor öffnen\n(E)";
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
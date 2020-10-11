using UnityEngine;
using TMPro;

public class CS_roofdoor : MonoBehaviour
{

    TextMeshProUGUI f_text;

    public bool door_open;
    bool f_active;

    GameObject door_roof;

    Vector3 le_i;

    // Start is called before the first frame update
    void Start()
    {
        door_open = false;

        f_active = false;

        f_text = GetComponent<TextMeshProUGUI>();
        f_text.text = "";

        door_roof = GameObject.Find("Cylinder.063");
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
            door_roof.transform.rotation = Quaternion.Lerp(door_roof.transform.rotation, Quaternion.Euler(-90f, 0f, 100f), Time.time * 0.01f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "First Person Player") && (door_open == false))
        {
            f_text.text = "Tür öffnen\n(E)";
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
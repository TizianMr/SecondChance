using UnityEngine;
using TMPro;

public class CS_first_door : MonoBehaviour
{

    bool locked;
    TextMeshProUGUI fd_text;
    bool d_active = false;
    bool d_open = false;
    bool s_show = true;
    bool go_open = false;

    GameObject door;
    GameObject keycard;
    CS_keycard kscript;


    // Start is called before the first frame update
    void Start()
    {

        fd_text = GetComponent<TextMeshProUGUI>();
        fd_text.text = "";

        locked = true;
        door = GameObject.Find("Cylinder.031");
        keycard = GameObject.Find("key_card_text");
        kscript = keycard.GetComponent<CS_keycard>();
    }

    // Update is called once per frame
    void Update()
    {
        locked = kscript.have_key_card;
        if (Input.GetKeyDown("e") && d_active) {
            d_open = true;
        }
        if (d_open == true)
        {
            go_open = true;
            s_show = false;
            fd_text.text = "";
        }
        if (go_open)
        {
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(-90f,0f,140f), Time.time * 0.01f);
        }
    }

    // Text einblenden
    void OnTriggerEnter(Collider other)
    {

        //fahrstuhl innen
        if ((other.gameObject.name == "First Person Player") && (s_show == true))
        {
            if (locked == false)
            {
                fd_text.text = "Tür\nverschlossen";
            }
            if (locked == true)
            {
                fd_text.text = "Aufschließen (E)";
                d_active = true;
            }
            
        }
    }

    // Text ausblenden
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "First Person Player")
        {

            fd_text.text = "";
            d_active = false;
        }

    }


}

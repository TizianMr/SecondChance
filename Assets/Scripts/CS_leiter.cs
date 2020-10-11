using UnityEngine;
using TMPro;

public class CS_leiter : MonoBehaviour
{
    TextMeshProUGUI f_text;

    public bool leiter_used;
    bool useit;
    bool f_active;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        useit = false;
        leiter_used = false;
        f_active = false;
        f_text = GetComponent<TextMeshProUGUI>();
        f_text.text = "";

        player = GameObject.Find("First Person Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            if ((leiter_used == false) && (f_active == true))
            {
                leiter_used = true;
                useit = true;
                f_text.text = "";

            }
        if (useit == true)
        {
            go_down();
            useit = false;
        }
    }
    void go_down ()
    {
        player.transform.position = new Vector3(8.8f, 2.6f, -0.7f);
        UnityEngine.Debug.Log("posi: " + player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        //fahrstuhl außen
        if ((other.gameObject.name == "First Person Player") && (leiter_used == false))
        {
            f_text.text = "Leiter \nbenutzen(E)";
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
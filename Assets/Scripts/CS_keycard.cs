using UnityEngine;
using TMPro;

public class CS_keycard : MonoBehaviour
{   

    public bool have_key_card = false;
    TextMeshProUGUI kc_text;
    bool kc_active = false;

    GameObject key_card;

    // Start is called before the first frame update
    void Start()
    {

        kc_text = GetComponent<TextMeshProUGUI>();
        kc_text.text = "";

        key_card = GameObject.Find("Plane.010");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            if ((kc_active == true) && (have_key_card == false))
            {
                kc_text.text = "";
                Destroy(key_card);
                kc_active = false;
                have_key_card = true;
            }
    }

    // Text einblenden
    void OnTriggerEnter(Collider other)
    {

        //fahrstuhl innen
        if ((other.gameObject.name == "First Person Player") && (have_key_card == false))
        {
            kc_text.text = "Schlüsselkarte\nnehmen (E)";
            kc_active = true;
        }
    }

    // Text ausblenden
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "First Person Player")
        {
            kc_text.text = "";
            kc_active = false;
        }

    }


}

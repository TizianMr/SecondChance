using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SS_HelikopterEnde : MonoBehaviour
{
    TextMeshProUGUI f_text;
    public bool escaped;
    bool f_active;

    void Start()
    {
        escaped = false;

        f_active = false;

        f_text = GetComponent<TextMeshProUGUI>();
        f_text.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
            if ((escaped == false) && (f_active == true))
            {
                escaped = true;
                Debug.Log("bye bye");
                f_text.text = "";
                SceneManager.LoadScene("EndScreen");
                Cursor.lockState = CursorLockMode.None;
            }
    }


    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "First Person Player") && (escaped == false))
        {
            f_text.text = "Einsteigen (E)";
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

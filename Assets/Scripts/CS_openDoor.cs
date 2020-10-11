using UnityEngine;

public class CS_openDoor : MonoBehaviour
{
    bool opend;

    // Start is called before the first frame update
    void Start()
    {
        
        opend = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("e"))
        {
            //UnityEngine.Debug.Log("e");
            if (opend)
            {
                this.transform.position += new Vector3(0, -5, 0);

                opend = false;
            } 
            else
            {
                this.transform.position += new Vector3(0, 5, 0);
                opend = true;
            }
            
        }

    }



}

using UnityEngine;

public class TM_RifleIsActive : MonoBehaviour
{
    public float amount;

    GameObject rifle;
 

    private float time = 10f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

        rifle = GameObject.Find("rifle");

    }

    // Update is called once per frame
    void Update()
    { 
        if (rifle.activeSelf == false)
        {
            timer = Time.time;
            if (time <= timer)
            {
                Debug.Log("It works Rifle");
                amount += 1;

                if (amount < 8)
                {
                    time = 10f + timer;
                }else if (amount > 8 && amount < 14)
                {
                    time = 6f + timer;
                } else
                {
                    time = 5f + timer;
                }
                
            }
        }
        else
        {
            amount = 4;
        }
    }
}

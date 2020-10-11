using UnityEngine;

public class TM_GunIsActive : MonoBehaviour
{
    public float amount;
    public float maxAmount = 13;

    GameObject pistol;

    private float time = 4.5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        pistol = GameObject.Find("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        if (pistol.activeSelf == false)
        {
            timer = Time.time;
            if (time <= timer)
            {
                Debug.Log("It works Gun");
                if(amount < maxAmount)
                {
                    amount += 1;
                }
                if (amount > 8)
                {
                    time = 7.75f + timer;
                }
                else
                {
                    time = 4.5f + timer;
                }
            }
        }
        else
        {
            amount = 3;
        }
    }
}

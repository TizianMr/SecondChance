using UnityEngine;

public class TM_KnifeIsActive : MonoBehaviour
{
    public float amount;
    float maxAmount = 13;
    GameObject knife;

    private float time = 3f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        knife = GameObject.Find("knife");
    }

    // Update is called once per frame
    void Update()
    {
        if (knife.activeSelf == false)
        {
            timer = Time.time;
            if (time <= timer)
            {
                Debug.Log("It works knife");
                if (amount < maxAmount)
                {
                    amount += 1;
                }
                time = 3f + timer;
            }
        }
        else
        {
            amount = 4;
        }
    }
}

using UnityEngine;

public class TM_Todeszone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Dead"))
        {
            this.GetComponent<CS_TM_PlayerStats>().currentHealth = 0;
            print("Dead");
        }
    }
}

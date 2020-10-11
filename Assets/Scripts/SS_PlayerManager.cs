using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SS_PlayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Ui;

    #region Singleton
    public static SS_PlayerManager instance;

    void Awake()
    {
        instance = this;

        //GUI Health und Monition erst nach fertig geladener Szene aktivieren
        StartCoroutine(ShowUI());
    }

    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(4); //4 Sekunden wie die Coroutine im Main Menu
        Ui.SetActive(true);
    }

    #endregion

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

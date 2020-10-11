using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SS_MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;   //Für die Master Lautstärke
    public GameObject loadingscreen;    //Ladebildschirm

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    /*Hauptmenu------------------------------------*/

    //Start
    public void PlayGameNow()
    {
        StartCoroutine(LoadSceneAsync());   //Asynchrones Laden
        Debug.Log("Game is starting");
    }

    //Optionen
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    //Beenden
    public void DoExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }


    /*Optionsmenu------------------------------------*/

    //Auflösung in externem Code SS_Resolution

    //Lautstärke Slider
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


    /*Loadingscreen------------------------------------*/
    IEnumerator LoadSceneAsync()
    {
        loadingscreen.SetActive(true);
        AsyncOperation load = SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);

        while (!load.isDone)
        {
            yield return null;
        }

        StartCoroutine(UnLoadScene());

    }
    IEnumerator UnLoadScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.UnloadSceneAsync("Menu");
    }


    /*Deathscreen------------------------------------*/

    //Nochmal Versuchen
    public void RetryGame()
    {
        SceneManager.LoadScene("Level1");
    }

    //Zum Hauptmenü
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

//Skript mit Hilfe des Tutorials "SETTINGS MENU in Unity" von Brackeys erstellt
//https://www.youtube.com/watch?v=YOaYQrN1oYQ

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SS_Resolution : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; //Auflösungsdropdown referenzieren

    Resolution[] resolutions;       //Array für Auflösungen

    void Start()
    {
        /*Optionen: Auflösung ------------------------------------*/
        resolutions = Screen.resolutions;    //Speichert alle verfügbaren Auflösungen

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();  //String Liste für Optionen im Dropdown

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //Auflösung einstellen
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

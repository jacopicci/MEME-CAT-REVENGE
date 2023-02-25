using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{

   // Elementi del menu
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;

    // Risoluzioni predefinite
    Resolution[] resolutions;
    List<string> resolutionOptions = new List<string>();

    // Inizializzazione
    void Start()
    {
        // Carica le risoluzioni supportate dal sistema
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);
        }
        // Aggiungi le risoluzioni al dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
        // Imposta la risoluzione attuale come selezionata di default
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Imposta il volume
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // Imposta la risoluzione
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Imposta la modalità a schermo intero
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Restituisce l'indice della risoluzione attuale
    int GetCurrentResolutionIndex()
    {
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width &&
                resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }
        return 0;
    }
    public void quit()
    {
        Application.Quit();
    }

}

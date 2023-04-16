using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenuScript : MonoBehaviour {
    public AudioMixer audioMixer;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void QuitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
    public void LoadLevel()
    {
        PlayerData data =SaveSystem.LoadPlayer();
        int level = data.level;
        SceneManager.LoadScene(level);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
       // Debug.Log(volume);
    }
}

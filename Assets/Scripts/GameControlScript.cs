using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class GameControlScript : MonoBehaviour {
    [Header("RestartItems")]
    public float restartDelay = 2f;
    public GameObject playercontroller;
    public GameObject deathPrefab;
    public GameObject box;
    public Animator camAnim;
    public AudioSource sourceBackground;
    public AudioSource source;
    public AudioClip deathSound;
    public AudioClip BackgroundMusic;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public static bool GameIsOver = false;
    public static bool isTutorialTime=false;
    public GameObject tutorialUI;
    public Animator startPanelAnim;
    public AudioMixer audioMixer;
    public bool initializeCheckpointNull = true;
    private void Awake()
    {
        Time.timeScale = 1f;
        if(startPanelAnim)startPanelAnim.SetTrigger("start");
        GameIsPaused = false;
        GameIsOver = false;
        isTutorialTime = false;
        SaveSystem.SavePlayer(SceneManager.GetActiveScene().buildIndex);
        if (initializeCheckpointNull) CheckPoint.ReachedPoint = Vector2.zero;
    }
    void Update()
    {
        if (isTutorialTime)
        {
            TutorialTime(2f);   
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            //BackGame();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            DeathAndRestart();
        }
        if (GameIsOver)
        {
            DeathAndRestart();
            GameIsOver = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetVolume0();
        }
    }

    public void BackGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void DeathAndRestart()
    {
        GameIsPaused = false;
        if (playercontroller) playercontroller.SetActive(false);
        GameObject DeathPrefab = Instantiate(deathPrefab, box.transform.position, Quaternion.identity);
        Destroy(DeathPrefab, 2f);
        if (box) box.SetActive(false);
        if (camAnim) camAnim.SetTrigger("cineZoomout");
        sourceBackground.Pause();
        Invoke("LevelRestart", restartDelay);
        source.clip = deathSound; source.Play(); 

    }
    private void DeleteAllLines()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("line");
        foreach(GameObject line in lines)
        {
            Destroy(line);
        }
    }
    public void SetVolume(float volume)
    {
       if(audioMixer) audioMixer.SetFloat("volume", volume);
        // Debug.Log(volume);
    }
    private void SetVolume0()
    {
        if (audioMixer) audioMixer.SetFloat("volume", -80);
        // Debug.Log(volume);
    }
    void LevelRestart()
    {
        if (CheckPoint.ReachedPoint != Vector2.zero)
        {
            
            Invoke("playBackgroundMusic", 0.6f);
            box.SetActive(true);
            box.transform.position = CheckPoint.ReachedPoint;
            box.GetComponent<Animator>().SetTrigger("idle");
            if (playercontroller) playercontroller.SetActive(true);
            if (startPanelAnim) startPanelAnim.SetTrigger("start");
            LineCreater.Fluidslimit = CheckPoint.fluids;
            DeleteAllLines();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void playBackgroundMusic()
    {
        sourceBackground.Play();
    } 
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
       // if(tutorialUI){tutorialUI.SetActive(false);}
        Time.timeScale = 1f;
        
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void TutorialTime(float x)
    {
       if(tutorialUI) tutorialUI.SetActive(true);
        StartCoroutine("Pauser", x);
        Invoke("RemoveTutorial",1f);
        isTutorialTime = false;
    }
    private IEnumerator Pauser(int p)
    {
        Time.timeScale = 0.01f;
        float pauseEndTime = Time.realtimeSinceStartup + p;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }
    private void RemoveTutorial()
    {
        if (tutorialUI) { tutorialUI.SetActive(false); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    private void Awake()
    {
        Time.timeScale = 1;
        if(instance == null)
        {
            instance = this;
        }
        SetInit();
    }

    void SetInit()
    {
        
    }

    public GameObject loadingPanel;
    public GameObject pausePanel;
    public GameObject deadPanel;
    public GameObject winPanel;
    public GameObject timeUpPanel;


    //public Button resumeGame;
    //public Button restartGame;
    public bool isPaused;
   

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        MusicManager.instance.musicEnabled = false;
        //resumeGame.onClick.RemoveAllListeners();
        //resumeGame.onClick.AddListener(() => ResumeGame());
        
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        MusicManager.instance.musicEnabled = true;

        pausePanel.SetActive(false);
    }

    public void PLayerWon()
    {
        Time.timeScale = 0;
        MusicManager.instance.musicEnabled = false;
        winPanel.SetActive(true);
    }
   
    public void TimeUp()
    {
      
        Time.timeScale = 0;

        timeUpPanel.SetActive(true);
    }

    public void PlayerDied()
    {
        Time.timeScale = 0;
        deadPanel.SetActive(true);
        //restartGame.onClick.RemoveAllListeners();
        //restartGame.onClick.AddListener(() => RestartGame());
       
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

  
    public void LoadLevel(int buildIndex)
    {
        StartCoroutine(LoadAsyncronously(buildIndex));
    }

    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        AsyncOperation loader = SceneManager.LoadSceneAsync(sceneIndex);

        loadingPanel.SetActive(true);

        while (!loader.isDone)
        {
            float progress = Mathf.Clamp01(loader.progress / .9f);
            Debug.Log(progress);
            LoadingPanel.instance.loadingPercent.text = (progress * 100 + "%");
            LoadingPanel.instance.loadingBar.value = progress;

            yield return null;
        }
    }






}

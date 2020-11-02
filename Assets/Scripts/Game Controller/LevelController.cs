using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelController : MonoBehaviour {

    //public void PlayLevel1()
    //   {
    //       SceneManager.LoadScene(2);
    //   }
    //   public void PlayLevel2()
    //   {
    //       SceneManager.LoadScene(3);
    //   }
    //   public void PlayLevel3()
    //   {
    //       SceneManager.LoadScene(4);
    //   }
    //   public void PlayLevel4()
    //   {
    //       SceneManager.LoadScene(5);
    //   }

    //   public void BackToMainMenu()
    //   {
    //       SceneManager.LoadScene(0);
    //   }

    private void Awake()
    {
        SetInit();
    }

    void SetInit()
    {
        //loadinPanel = GameObject.Find("Loading Panel");
    }


    public GameObject loadingPanel;
    public void LoadLevel(int sceneIndex)
    {
        
        StartCoroutine(LoadAsyncronously(sceneIndex));
    }

  

    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        AsyncOperation loader = SceneManager.LoadSceneAsync(sceneIndex);
        


        while (!loader.isDone)
        {
            loadingPanel.SetActive(true);
            float progress = Mathf.Clamp01(loader.progress / .9f);
            Debug.Log(progress);
            LoadingPanel.instance.loadingPercent.text = (progress * 100 + "%");
            LoadingPanel.instance.loadingBar.value = progress;

            yield return null;
        }
    }









}

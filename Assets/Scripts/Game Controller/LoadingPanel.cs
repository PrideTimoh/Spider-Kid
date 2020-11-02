using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingPanel : MonoBehaviour {

    public static LoadingPanel instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        SetInit();
    }

    private void OnEnable()
    {
        anim.Play("Loading");
    }


    public Text loadingPercent;
    public Slider loadingBar;
    Animator anim;


    void SetInit()
    {
        gameObject.SetActive(false);
        loadingBar = GetComponentInChildren<Slider>();
        anim = GetComponentInChildren<Animator>();
        
    }
}

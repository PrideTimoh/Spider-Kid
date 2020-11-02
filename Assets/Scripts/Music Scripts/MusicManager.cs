using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public static MusicManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public AudioSource musicSource;
    //public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    public AudioClip diamondCollectSound;
    public AudioClip healthCollectSound;
    public AudioClip refuseCollectSound;
    public AudioClip timerCollectSound;

    [Range(0, 1f)]
    public float musicVolume;
    [Range(0, 1f)]
    public float fxVolume;

    public bool musicEnabled;
    public bool fxEnabled;


    public AudioClip[] musicClips;
    AudioClip selectedMusic;

    AudioClip SelectedMusic(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, musicClips.Length)];
        return randomClip;
    }

    void Start () 
	{
        SetInit();
        PlayBackgroundMusic(SelectedMusic(musicClips));
	}
	
	void Update () 
	{
        UpdateMusic();
        musicSource.volume = musicVolume;
        if (musicSource.isPlaying != true)
        {
            PlayBackgroundMusic(SelectedMusic(musicClips));
        }
    }

    public void PlaySound(AudioClip sound, float additionalLoudness)
    {
        if (fxEnabled)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, fxVolume / additionalLoudness);
        }
    }

    public void UpdateMusic()
    {
        if (musicSource.isPlaying != musicEnabled)
        {
            if (musicEnabled)
            {
                PlayBackgroundMusic(SelectedMusic(musicClips));
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void ToggleFxEnabled()
    {
        fxEnabled = !fxEnabled;
    }

    public void ToggleBackGroundMusic()
    {
        
        musicEnabled = !musicEnabled;
        UpdateMusic();
    }

    void PlayBackgroundMusic(AudioClip music)
    {
        if(music == null|| musicEnabled == false || musicSource == null)
        {
            return;
        }
        else
        {
            musicSource.Stop();
            musicSource.clip = music;
            musicSource.volume = musicVolume;
            musicSource.loop = false;
            musicSource.Play();
        }
        
    }

	void SetInit()
	{
        musicEnabled = true;
        fxEnabled = true;
	}
}

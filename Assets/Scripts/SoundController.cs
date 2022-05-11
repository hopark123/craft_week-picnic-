using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{

    public static SoundController instance;
    public AudioSource bgSound;
    public AudioClip[] bglist;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        else
            Destroy(gameObject);
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; ++i)
        {
            if (arg0.name == bglist[i].name) {
                BgSoundPlay(bglist[i]);
                break ;
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject sound = new GameObject(sfxName + "sound");
        AudioSource audioSource = sound.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        if (sound)
            Destroy(sound, clip.length);
    }


    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        if (clip.name == "OpeningScene")
            bgSound.loop = false;
        else
            bgSound.loop = true;

        bgSound.volume = 1.0f;
        bgSound.Play();
    }

    public void BgSoundStop()
    {
        SoundController.instance.GetComponent<AudioSource>().volume = 0.0f;
    }

    public void BgSoundRestart()
    {
        SoundController.instance.GetComponent<AudioSource>().volume = 1.0f;
    }
}

/*
public AudioClip JumpClip;
SoundController.instance.SFXPlay("Jump", JumpClip);

 */
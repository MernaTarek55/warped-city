using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource SFX;

    public AudioClip Background;
    public AudioClip boyhurt;
    public AudioClip girlhurt;
    public AudioClip enimydamage;
    public AudioClip shoot;
    public AudioClip GameOver;
    public AudioClip Win;
    public AudioClip Button;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (Music == null)
                Music = gameObject.AddComponent<AudioSource>();

            if (SFX == null)
                SFX = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Music.clip = Background;
        Music.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
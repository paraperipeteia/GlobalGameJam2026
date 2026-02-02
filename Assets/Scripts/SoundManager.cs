using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource sfxAudioSource;
    
    [SerializeField] private MusicTimer musicTimer;
   
    [SerializeField] private AudioClip tickingClip;
    [SerializeField] private AudioClip TimesUPClip;
    [SerializeField] private AudioClip CashClip;
    [SerializeField] private AudioClip CrowdClip;
    [SerializeField] private AudioClip FacilityClip;

    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    public float MusicVolume => musicTimer.Music.volume;
    public float SfxVolume => sfxAudioSource.volume;

    private bool playingCash, playingFacility, playingPersonnel;
    private float cashTime, facilityTime, personnelTime;
    private float minTime = 0.5f;
    public void Awake()
    {
        Instance = this;
        musicSlider.onValueChanged.AddListener(AdjustMusicVolume);
        sfxSlider.onValueChanged.AddListener(AdjustSFXVolume);
        
        musicSlider.value = MusicVolume;
        sfxSlider.value = SfxVolume;
    }

    private void OnEnable()
    {
        ResourceSlider.OnSliderValueChanged += OnSliderValueChanged; 
    }

    private void OnSliderValueChanged(ResourceType t, float amount)
    {
        switch (t)
        {
            case ResourceType.Money:
                PlayCashSound();
                break;
            case ResourceType.Personnel:
                PlayCrowdSound();
                break;
            case ResourceType.Facilities:
                PlayFacilitySound();
                break;
            default:
                Debug.Log("Type not supported - cannot playback a sound");
                break;
        }
    }
    
    private void Update()
    {
        if (playingCash)
        {
            cashTime += Time.deltaTime;
            if (cashTime > minTime)
            {
                cashTime = 0; 
                playingCash = false;
            }
        }

        if (playingFacility)
        {
            facilityTime += Time.deltaTime;
            if (facilityTime > minTime)
            {
                facilityTime = 0; 
                playingFacility = false;
            }
        }

        if (playingPersonnel)
        {
            personnelTime += Time.deltaTime;
            if (personnelTime > minTime)
            {
                personnelTime = 0; 
                playingPersonnel = false;
            }
        }
    }

    public void PlayTicking15Sec()
    {
        sfxAudioSource.PlayOneShot(tickingClip);
    }

    public void PlayTimesUp()
    {
        sfxAudioSource.PlayOneShot(TimesUPClip);
    }
    public void PlayCashSound()
    {
        if (!playingCash)
        {
            sfxAudioSource.PlayOneShot(CashClip);
            playingCash =  true;
        }
    }
    public void PlayCrowdSound()
    {
        if (!playingPersonnel)
        {
            sfxAudioSource.PlayOneShot(CrowdClip);
            playingPersonnel = true;
        }
    }
    public void PlayFacilitySound()
    {
        if (!playingFacility)
        {
            sfxAudioSource.PlayOneShot(FacilityClip);
            playingFacility = true;
        }
    }
    
    public void AdjustMusicVolume(float volume)
    {
        musicTimer.AdjustMusicVolume(volume);
    }

    public void AdjustSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;        
    }

}

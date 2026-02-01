using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickingClip;
    [SerializeField] private AudioClip TimesUPClip;
    [SerializeField] private AudioClip CashClip;
    [SerializeField] private AudioClip CrowdClip;
    [SerializeField] private AudioClip FacilityClip;
    public void Awake()
    {
        Instance = this;
    }
    public void PlayTicking15Sec()
    {
        audioSource.PlayOneShot(tickingClip);
    }

    public void PlayTimesUp()
    {
        audioSource.PlayOneShot(TimesUPClip);
    }
    public void PlayCashSound()
    {
        audioSource.PlayOneShot(CashClip);
    }
    public void PlayCrownSound()
    {
        audioSource.PlayOneShot(CrowdClip);
    }
    public void PlayFacilitySound()
    {
        audioSource.PlayOneShot(FacilityClip);
    }

}

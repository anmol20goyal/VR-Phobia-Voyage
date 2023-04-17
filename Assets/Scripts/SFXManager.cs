using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region SFX Instance

    public static SFXManager sfxInstance;

    #endregion

    #region Audio Variables

    [SerializeField] private AudioSource audioS, bgmAudioS;
    [SerializeField] private AudioClip btnPressClip, menuClip, gameClip;

    #endregion

    private void Awake()
    {
        if (sfxInstance == null)
        {
            sfxInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    #region BackGround Audio Source

    private void Start() // for login BG music
    {
        bgmAudioS.clip = menuClip;
        bgmAudioS.Play();
    }

    public void ChangeBGM() // for game BG music
    {
        bgmAudioS.clip = gameClip;
        bgmAudioS.Play();
    }

    #endregion

    #region For Game Audio - Source

    public void PlayAudio(AudioClip clip, bool loop = false)
    {
        audioS.loop = loop;
        if (loop)
        {
            audioS.clip = clip;
            audioS.Play();
        }
        else
            audioS.PlayOneShot(clip);

    }

    public void IncreaseSpeed(float speed)
    {
        audioS.pitch = speed;
    }

    public void DefaultSpeed()
    {
        audioS.pitch = 1;
    }

    public void StopAudio()
    {
        audioS.Stop();
    }

    #endregion

    public void BtnPressSound()
    {
        audioS.PlayOneShot(btnPressClip);
    }
}

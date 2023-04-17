using UnityEngine;

public class PlayAudioOnDemand : MonoBehaviour
{
    [SerializeField] private AudioSource _audioS;
    [SerializeField] private AudioClip _audioClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (_audioClip != null) _audioS.PlayOneShot(_audioClip);
    }
}
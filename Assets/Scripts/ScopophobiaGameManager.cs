using UnityEngine;

public class ScopophobiaGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _bigEyeGO;
    [SerializeField] private Transform _xrOrigin;
    [SerializeField] private Transform _eyeHandler;
    [SerializeField] private ParticleSystem _fogParticles;

    [SerializeField] private AudioSource[] _audioS;

    //public Vector3 finalPos;
    //public bool canChange;

    public void InitializeScopophobia()
    {
        //_bigEyeGO.SetActive(true);
        _fogParticles.gameObject.SetActive(true);
        _fogParticles.Play();
        
        _bigEyeGO.transform.parent = null;
        _bigEyeGO.transform.position = new Vector3(_xrOrigin.position.x + 4, _bigEyeGO.transform.position.y, _bigEyeGO.transform.position.z);

        _bigEyeGO.transform.SetParent(_xrOrigin);

        //finalPos = _bigEyeGO.transform.localPosition;
        ////Debug.Log(finalPos);
        //_bigEyeGO.transform.localPosition = new Vector3(finalPos.x + 1, finalPos.y, finalPos.z);
        //Debug.Log(_bigEyeGO.transform.localPosition);

        foreach (var audioS in _audioS)
        {
            audioS.Play();
        }
    }

    //private void Update()
    //{
    //    if (canChange)
    //    {
    //        _bigEyeGO.transform.localPosition = finalPos;
    //    }
    //}

    public void EndScopophobia()
    {
        foreach (var audioS in _audioS)
        {
            audioS.Stop();
        }

        _bigEyeGO.transform.SetParent(_eyeHandler);
        _fogParticles.Stop();
    }
}
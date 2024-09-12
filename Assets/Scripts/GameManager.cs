using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region GameObjects

    [Header("*****GameObjects*****")]
    [SerializeField] private Renderer _ventRenderer;
    [SerializeField] private Transform[] _ventExtremeTrans;
    [SerializeField] private Transform _xrOriginTrans;
    [SerializeField] private TMP_Text _instrTxt;

    [Header("*****Particle System*****")]
    [SerializeField] private ParticleSystem _fogInVent;

    #endregion

    #region Sound Variables

    //elevator audio sources
    [Header("*****Elevator Audio Sources*****")]
    [SerializeField] private AudioSource _emergencyAudioS;
    [SerializeField] private AudioSource _elevatorBgmMusicAudioS;
    [SerializeField] private AudioSource _elevatorMoveAudioS;

    //player audio sources
    [Header("*****Player Audio Sources*****")]
    [SerializeField] private AudioSource _playerAudioS;
    [SerializeField] private AudioSource _playerHotTouchAudioS;
    [SerializeField] private AudioClip _playerClimbingRopeClip;
    [SerializeField] private AudioClip _playerVentBreathingClip;
    [SerializeField] private AudioClip _playerHotMetalTouchClip;

    //vent audio sources
    [Header("*****Vent Audio Sources*****")]
    [SerializeField] private AudioSource _ventEnterAudioS;
    [SerializeField] private AudioSource[] _ventAirAudioS;


    #endregion

    #region Light Variables

    [Header("*****Light Variables*****")]
    [SerializeField] private Light _elevatorTopLights;

    #endregion

    #region Variables

    private bool _once;
    private bool _changeLiftLightColor;

    [Header("*****Player Position Variables*****")]
    private Vector3 _originalPlayerPosition;
    private Quaternion _originalPlayerRotation;

    [Header("*****Vent Variables*****")]
    private float totalDistance;
    private bool startVentEffect;

    [Header("*****Vent Color Variables*****")]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Color _startFogColor;
    [SerializeField] private Color _endFogColor;

    [Header("*****TMP_Text Font Size*****")]
    [SerializeField] private float _maxFontSize_ext;

    #endregion

    private void Awake()
    {
        totalDistance = Mathf.Abs(_ventExtremeTrans[0].position.x - _ventExtremeTrans[1].position.x);
        startVentEffect = false;
        _once = true;
        _changeLiftLightColor = false;

        //set the players original position at start of the game
        _originalPlayerPosition = _xrOriginTrans.position;
        _originalPlayerRotation = _xrOriginTrans.rotation;

        // play the elevator bgm music
        _elevatorBgmMusicAudioS.Play();
        //_elevatorMoveAudioS.Play();

        // overall fog settings
        RenderSettings.fog = true;
        RenderSettings.fogColor = _startFogColor;
    }

    private void Start()
    {
        Invoke(nameof(InitializeGame), 15f);
    }

    private void InitializeGame()
    {
        // start the emergency sound and lights
        _elevatorBgmMusicAudioS.Stop();
        _elevatorMoveAudioS.Stop();

        _emergencyAudioS.Play();

        //show the instruction text in the lift
        _instrTxt.gameObject.SetActive(true);
        _instrTxt.fontSizeMax = _maxFontSize_ext;
        _instrTxt.text = "The lift is stuck!!\r\n\r\nUse the screwdriver to unscrew the top panel\r\nto exit the lift.";

        // show emergency lighting in elevator
        _changeLiftLightColor = true;
        ChangeColor();
    }

    private void Update()
    {
        // through the vent methods
        if (startVentEffect) VentColorLerp();
    }

    private void ChangeColor()
    {
        if (!_changeLiftLightColor)
        {
            _elevatorTopLights.color = _endColor;
            return;
        }
        else
        {
            _elevatorTopLights.DOColor(_endColor, 2f).OnComplete(() =>
            {
                _elevatorTopLights.DOColor(Color.white, 2f).OnComplete(ChangeColor);
            });
        }
    }

    public void DarkenRoom()
    {
        StartCoroutine(ChangeFogColor(_startFogColor, _endFogColor, 0.75f));
    }

    private IEnumerator ChangeFogColor(Color startColor, Color endColor, float duration)
    {
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            RenderSettings.fogColor = Color.Lerp(startColor, endColor, counter / duration);
            yield return null;
        }

        RenderSettings.fog = false;
    }


    #region Climing Rope Methods

    public void ClimbRopeSoundEffects()
    {
        _changeLiftLightColor = false;
        
        _playerAudioS.clip = _playerClimbingRopeClip;
        _playerAudioS.enabled = true;

        _playerAudioS.Play();
    }

    #endregion


    #region Through The Vent Methods

    public void VentAmbientSound()
    {
        foreach (var audioS in _ventAirAudioS)
        {
            audioS.Play();
        }
    }

    public void EnterVentSounds()
    {
        _fogInVent.Play();

        // stop player sounds
        _playerAudioS.Stop();
        _playerAudioS.enabled = false;

        // play crawling (short) sound
        _ventEnterAudioS.Play();

        //play in vent breathing sound
        _playerAudioS.clip = _playerVentBreathingClip;
        _playerAudioS.enabled = true;

        _playerAudioS.Play();
        
    }

    //invoked through event function used in CollisionHandler script
    public void StartVentColorEffect()
    {
        startVentEffect = true;
        RenderSettings.fogEndDistance = 30;
    }

    private float CalculateRelativePos()
    {
        var calcDiff = _ventExtremeTrans[0].position.x - _xrOriginTrans.position.x;

        return calcDiff / totalDistance;
    }

    private void VentColorLerp()
    {
        var relPos = CalculateRelativePos();

        //start player hot metal touch sound
        if (relPos > 0.5f && _once)
        {
            _once = false;
            _playerHotTouchAudioS.clip = _playerHotMetalTouchClip;
            _playerHotTouchAudioS.enabled = true;
            _playerHotTouchAudioS.Play();
        }

        _ventRenderer.material.color = Color.Lerp(_startColor, _endColor, CalculateRelativePos());
    }

    public void EndVentSounds()
    {
        _fogInVent.Stop();

        startVentEffect = false;

        _playerAudioS.Stop();
        _playerAudioS.enabled = false;

        _playerHotTouchAudioS.Stop();
        _playerHotTouchAudioS.enabled = false;

        foreach (var audioS in _ventAirAudioS)
        {
            audioS.Stop();
        }
    }

    #endregion

    #region GameEnd Teleport

    public void TeleportToElevator()
    {
        // set the players position and rotation back in the elevator
        _xrOriginTrans.position = _originalPlayerPosition;
        _xrOriginTrans.rotation = _originalPlayerRotation;

        //restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

}
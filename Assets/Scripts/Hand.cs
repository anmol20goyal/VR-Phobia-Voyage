using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    #region GameObjects

    private SkinnedMeshRenderer mesh;

    #endregion
    
    #region Animation Variables

    private Animator _animator;
    private string ANIMATOR_GRIP = "Grip";
    private string ANIMATOR_TRIGGER = "Trigger";
    
    #endregion

    #region Hand Variables

    private float gripTarget, gripCurrent;
    private float triggerTarget, triggerCurrent;

    #endregion

    #region Variables

    public float speed;

    #endregion
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        AnimateHand();
    }

    public void SetGrip(float v)
    {
        gripTarget = v;
    }

    public void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    private void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            _animator.SetFloat(ANIMATOR_GRIP, gripCurrent);
        }   
        
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            _animator.SetFloat(ANIMATOR_TRIGGER, triggerCurrent);
        }   
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    }
}
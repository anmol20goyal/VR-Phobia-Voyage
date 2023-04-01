using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    #region GameObjects

    // public Transform eyeGO;
    [SerializeField] private Transform currentCamera;

    #endregion

    #region Variables

    [SerializeField] public Vector3 SetRotationLimit_max;
    [SerializeField] public Vector3 SetRotationLimit_min;

    public bool inProximity;
    [SerializeField] private float lookAtDistance = 7;

    #endregion

    private void Start()
    {
        inProximity = false;
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, currentCamera.position);
        if (lookAtDistance > dist)
        {
            inProximity = true;
            lookAtDistance = 20;

            transform.LookAt(currentCamera.transform.position);
        }
        else
        {
            inProximity = false;
        }
    }
}
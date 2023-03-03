using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    #region GameObjects

    // public Transform eyeGO;
    [SerializeField] private Transform currentCamera;

    #endregion

    #region Variables

    public bool inProximity;
    public float lookAtDistance = 7;
    public float lookAtSpeed = 3;
    [SerializeField] private Quaternion currentRotation;
    
    #endregion

    private void Start()
    {
        inProximity = false;
        // currentCamera = Camera.main.transform;
        // startRotation = eyeGO.localRotation;
    }

    public Vector3 angle;
    
    private void Update()
    {
        // transform.LookAt(currentCamera.transform.localPosition + angle);
    }

    public Vector3 distance;

    private void LateUpdate()
    {
        if (lookAtDistance > Vector3.Distance(transform.position, currentCamera.position))
        {
            distance = currentCamera.position - transform.position;
            inProximity = true;
            currentRotation = Quaternion.Slerp(
                currentRotation,
                Quaternion.LookRotation(distance),
                Time.deltaTime * lookAtSpeed);
    
            transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles + angle);
        }
        else
        {
            // currentRotation = Quaternion.Slerp(currentRotation, eyeGO.rotation, Time.deltaTime * lookAtSpeed);
            inProximity = false;
        }
    
    }
    
    // public float rotationModifier, speed;
    // private void FixedUpdate()
    // {
    //     Vector3 vectorToTarget = currentCamera.transform.position - transform.position;
    //     float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
    //     Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    //
    // }
}

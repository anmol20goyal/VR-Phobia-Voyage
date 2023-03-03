using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    #region GameObjects

    public Transform eyeGO;
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

    private void LateUpdate()
    {
        if (lookAtDistance > Vector3.Distance(eyeGO.position, currentCamera.position))
        {
            inProximity = true;
            currentRotation = Quaternion.Slerp(
                currentRotation, 
                Quaternion.LookRotation(currentCamera.position - eyeGO.position), 
                Time.deltaTime * lookAtSpeed);
            
            eyeGO.localRotation = currentRotation;
        }
        else
        {
            // currentRotation = Quaternion.Slerp(currentRotation, eyeGO.rotation, Time.deltaTime * lookAtSpeed);
            inProximity = false;
        }

    }
}

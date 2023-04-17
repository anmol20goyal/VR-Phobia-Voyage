using UnityEngine;

public class VrNoPeek : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float sphereCheckSize = 0.15f;

    [SerializeField] private Material cameraFadeMat;
    private bool isCameraFadedOut = false;

    private void Awake()
    {
        cameraFadeMat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFade(1f);
            isCameraFadedOut = true;
        }
        else
        {
            if (!isCameraFadedOut) return;

            CameraFade(0f);
        }
    }

    private void CameraFade(float targetAlpha)
    {
        var fadeValue = Mathf.MoveTowards(cameraFadeMat.GetFloat("_AlphaValue"), targetAlpha, Time.deltaTime * fadeSpeed);
        cameraFadeMat.SetFloat("_AlphaValue", fadeValue);

        if (fadeValue <= 0.01f)
            isCameraFadedOut = false;
    }
}

using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewActivityHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody _elevatorUpDoorGO;
    [SerializeField] private GameObject _screwDriverGO;
    [SerializeField] private Transform _driverPositionTrans;
    [SerializeField] private AudioSource _screwOpeningSource;

    public void ScrewFell()
    {
        StartCoroutine(ScrewFellCoroutine());
    }

    private IEnumerator ScrewFellCoroutine() 
    {
        // attach the screwdriver to the screw and rotate it (using dotween)

        _screwDriverGO.GetComponent<Rigidbody>().isKinematic = true;
        _screwDriverGO.GetComponent<XRGrabInteractable>().enabled = false;
        _screwDriverGO.transform.DOMove(_driverPositionTrans.localPosition, 2);
        _screwDriverGO.transform.DORotate(_driverPositionTrans.localRotation.eulerAngles, 2);
        yield return new WaitForSeconds(2f);
        _screwOpeningSource.enabled = true;
        _screwDriverGO.transform.DORotate(new Vector3(0, 180, 0), 2f);
        yield return new WaitForSeconds(2f);
        _screwOpeningSource.enabled = false;
        _screwDriverGO.GetComponent<Rigidbody>().isKinematic = false;



        transform.localPosition = new Vector3(transform.localPosition.x,
                                              transform.localPosition.y - 0.1f,
                                              transform.localPosition.z);

        GetComponent<Rigidbody>().isKinematic = false;

        _elevatorUpDoorGO.isKinematic = false;
        _elevatorUpDoorGO.useGravity = true;

    }
}
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
        //StartCoroutine(ScrewFellCoroutine());
        ScrewFellCoroutine();
    }

    private void ScrewFellCoroutine() 
    {
        // attach the screwdriver to the screw and rotate it (using dotween)


        _screwDriverGO.GetComponent<Rigidbody>().isKinematic = true;
        _screwDriverGO.GetComponent<XRGrabInteractable>().enabled = false;
        _screwDriverGO.transform.DOMove(_driverPositionTrans.position, 1);
        _screwDriverGO.transform.DORotate(_driverPositionTrans.localRotation.eulerAngles, 1).OnComplete(() =>
        {
            var originRotate = _screwDriverGO.transform.localRotation.eulerAngles;

            //yield return new WaitForSeconds(1f);

            _screwOpeningSource.enabled = true;
            _screwDriverGO.transform.DOLocalRotate(new Vector3(originRotate.x, 180, originRotate.z), 1f).OnComplete(()=>
            {

                //yield return new WaitForSeconds(2f);

                _screwOpeningSource.enabled = false;

                transform.localPosition = new Vector3(transform.localPosition.x,
                                                      transform.localPosition.y - 0.1f,
                                                      transform.localPosition.z);

                GetComponent<Rigidbody>().isKinematic = false;

                _elevatorUpDoorGO.isKinematic = false;
                _elevatorUpDoorGO.useGravity = true;
                _screwDriverGO.GetComponent<Rigidbody>().isKinematic = false;
            });


        });

        

    }
}
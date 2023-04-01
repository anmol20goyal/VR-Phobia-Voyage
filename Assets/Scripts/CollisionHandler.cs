using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    #region GameObjects

    [SerializeField] private GameObject endGameTxt;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            endGameTxt.SetActive(true);
        }
    }
}

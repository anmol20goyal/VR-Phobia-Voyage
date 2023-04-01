using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EyeRotateRandom : MonoBehaviour
{
	#region Variables
	
	[SerializeField] private float timeTakenToRotate;

    #endregion

    #region GameObjects

    [SerializeField] private GameObject[] eyes;
	[SerializeField] private AnimationClip[] eyeAnims;
	[SerializeField] private MeshRenderer[] eyeMesh;
	[SerializeField] private Material[] eyeMats;

	#endregion
	
	private void Start()
	{
		for (int i = 0; i < eyes.Length; i++)
		{
			RotateEye(eyes[i]);
			ShowEyeAnimations(eyes[i]);
			StartCoroutine(ChangeEyeColor(eyeMesh[i]));
		}
	}

	private void RotateEye(GameObject eye)
	{
		var lookAt = eye.GetComponent<LookAtPlayer>();

        if (lookAt.inProximity) return;

		var randomRot = new Vector3(
			Random.Range(lookAt.SetRotationLimit_min.x, lookAt.SetRotationLimit_max.x),
		    Random.Range(lookAt.SetRotationLimit_min.y, lookAt.SetRotationLimit_max.y),
			Random.Range(lookAt.SetRotationLimit_min.z, lookAt.SetRotationLimit_max.z));

        eye.transform.DOLocalRotate(randomRot, timeTakenToRotate).SetEase(Ease.Linear).OnComplete(() => RotateEye(eye));
	}

	private void ShowEyeAnimations(GameObject eye)
	{
		var animator = eye.GetComponent<Animator>();

		animator.enabled = true;
		var animClip = eyeAnims[Random.Range(0, eyeAnims.Length)].name.ToString();
		animator.Play(animClip);
	}

	private IEnumerator ChangeEyeColor(MeshRenderer eyeMesh)
	{
		eyeMesh.material = eyeMats[Random.Range(0, eyeMats.Length)];
		yield return new WaitForSeconds(1.2f);
		StartCoroutine(ChangeEyeColor(eyeMesh));
	}
}

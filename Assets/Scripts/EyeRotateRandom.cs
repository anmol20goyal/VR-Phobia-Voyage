using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyeRotateRandom : MonoBehaviour
{
	#region Variables
	
	[SerializeField] private Vector3 maxRot, minRot;

	#endregion

	#region GameObjects

	[SerializeField] private GameObject[] eyes;
	[SerializeField] private Material[] eyeMats;

	#endregion
	
	private void Start()
	{
		foreach (var eye in eyes)
		{
			StartCoroutine(Rotate(eye));
			StartCoroutine(ChangeEyeColor(eye.transform.GetChild(0).GetComponent<MeshRenderer>()));
		}
	}

	private IEnumerator Rotate(GameObject eye)
	{
		if (eye.GetComponent<LookAtPlayer>().inProximity) yield return null;
		
		var startRot = eye.transform.localRotation;

		// Set random rotation values
		var ranRot = new Vector3(
			Random.Range(minRot.x, maxRot.x),
			Random.Range(minRot.y, maxRot.y),
			Random.Range(minRot.z, maxRot.z));
		
		var randomRot = Quaternion.Euler(ranRot);
		
		var t = 0f;
		while (t < 2)
        {
            t += Time.deltaTime;
            var xRot = Mathf.Lerp(startRot.x, randomRot.x, t / 2) % 360;
            var yRot = Mathf.Lerp(startRot.y, randomRot.y, t / 2) % 360;
            var zRot = Mathf.Lerp(startRot.z, randomRot.z, t / 2) % 360;
            var wRot = Mathf.Lerp(startRot.w, randomRot.w, t / 2) % 360;
            eye.transform.localRotation = new Quaternion(xRot, yRot, zRot, wRot);
            yield return null;
        }
		
		StartCoroutine(Rotate(eye));
	}

	private IEnumerator ChangeEyeColor(MeshRenderer eyeMesh)
	{
		// var eyeMat = eyeMesh.material;
		eyeMesh.material = eyeMats[Random.Range(0, eyeMats.Length)];
		yield return new WaitForSeconds(1.2f);
		StartCoroutine(ChangeEyeColor(eyeMesh));
	}
}

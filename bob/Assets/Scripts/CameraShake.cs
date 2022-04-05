using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public static CameraShake instance;

	private Vector3 originalPos;
	private Coroutine coroutine;

	private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }


	public static void Shake (float duration, float amount) {
		instance.originalPos = instance.gameObject.transform.localPosition;
		if(instance.coroutine != null) {
			instance.StopCoroutine(instance.coroutine);
		}
		instance.coroutine = instance.StartCoroutine(instance.cShake(duration, amount));
	}

	public IEnumerator cShake (float duration, float amount) {
		float endTime = Time.time + duration;

		while (duration > 0) {
			transform.localPosition = originalPos + Random.insideUnitSphere * amount;

			duration -= Time.deltaTime;

			yield return null;
		}

		originalPos.z = -10;
		transform.localPosition = originalPos;
	}
}
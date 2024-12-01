using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    private const float MIN_TIME_TO_APPEAR = 0.3f;
    private const float MAX_TIME_TO_APPEAR = 0.8f;

    [Tooltip("Floors have a priority when being displayed by the DecorationController")]
    public bool isFloor;

    private Vector3 targetScale;

    private void Start()
    {
        targetScale = transform.localScale;
        MakeInvisible();
    }
    private void MakeInvisible() => transform.localScale = Vector3.zero;
    public void PlaySpawnAnimation() => StartCoroutine(SpawnAnimationRoutine());
    private IEnumerator SpawnAnimationRoutine()
    {
        MakeInvisible();
        var t = 0f;
        float lerpDuration = Random.Range(MIN_TIME_TO_APPEAR, MAX_TIME_TO_APPEAR);
        
        Vector3 initialScale = transform.localScale;

        while (t < lerpDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t / lerpDuration);

            t += Time.deltaTime;
            yield return null;
        }

    }

    public void StopAnimation()
    {
        StopAllCoroutines();
        transform.localScale = targetScale;
    }
}

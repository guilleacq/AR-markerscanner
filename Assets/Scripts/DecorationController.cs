using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class DecorationController : MonoBehaviour
{
    private const float MIN_TIME_BETWEEN_APPEAR = 0.01f;
    private const float MAX_TIME_BETWEEN_APPEAR = 0.04f;

    private bool hasPlayedAnimation = false;
    private Decoration[] decorations;

    public void DisableVisibility()
    {
        foreach (var decoration in decorations)
        {
            decoration.StopAnimation();
        }
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void EnableVisibility()
    {
        gameObject.SetActive(true);

        if (!hasPlayedAnimation)
        {
            PlayAppearAnimation();
            hasPlayedAnimation = true;
        }
    }
    private void PlayAppearAnimation() => StartCoroutine(AppearAnimationRoutine());
    private IEnumerator AppearAnimationRoutine()
    {
        decorations = GetComponentsInChildren<Decoration>();

        var floors = new List<Decoration>();
        var others = new List<Decoration>();

        //Get which decorations are floors
        foreach (var decoration in decorations)
        {
            if (decoration.isFloor)
                floors.Add(decoration);
            else
                others.Add(decoration);
        }

        // ------------------------------------------

        foreach (var decoration in others)
        {
            decoration.PlaySpawnAnimation();
            yield return new WaitForSeconds(Random.Range(MIN_TIME_BETWEEN_APPEAR, MAX_TIME_BETWEEN_APPEAR));
        }

        foreach (var floor in floors)
        {
            floor.PlaySpawnAnimation();
            yield return new WaitForSeconds(Random.Range(MIN_TIME_BETWEEN_APPEAR, MAX_TIME_BETWEEN_APPEAR));
        }
        yield return null;

    }
}

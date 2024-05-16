using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WellDoneScreenManager : MonoBehaviour
{
    [SerializeField] Star[] Stars;

    [SerializeField] float EnlargeScale = 1.5f;
    [SerializeField] float ShrinkScale = 1f;
    [SerializeField] float EnlargeDuration = 0.25f;
    [SerializeField] float ShrinkDuration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        ShowStars(Stars.Length);
    }

    public void ShowStars(int numberOfStars)
    {
        StartCoroutine(ShowStarsRoutine(numberOfStars));
    }

    private IEnumerator ShowStarsRoutine(int numberOfStars)
    {
        foreach (Star star in Stars) 
        {
            star.star.transform.localScale = Vector3.zero;
        }

        for(int i = 0; i < numberOfStars; i++)
        {
            yield return StartCoroutine(EnlargeAndShrinkStar(Stars[i]));
        }
    }

    private IEnumerator EnlargeAndShrinkStar(Star star)
    {
        yield return StartCoroutine(ChangeStarScale(star, EnlargeScale, EnlargeDuration));
        yield return StartCoroutine(ChangeStarScale(star, ShrinkScale, ShrinkDuration));
        yield return StartCoroutine(startParticle(star));
    }

    private IEnumerator ChangeStarScale(Star star, float targetScale, float duration)
    {
        Vector3 initialScale = star.star.transform.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            star.star.transform.localScale = Vector3.Lerp(initialScale, finalScale, (elapsedTime/duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        star.star.transform.localScale = finalScale;
    }

    private IEnumerator startParticle(Star star)
    {
        star.StartParticle();
        yield return null;
    }
}

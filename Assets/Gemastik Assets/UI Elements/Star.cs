using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image star;
    public UIParticle particle;
    void Awake()
    {
        GameObject originalGameObject = this.gameObject;
        star = originalGameObject.transform.GetChild(1).gameObject.transform.GetComponent<Image>();
        star.transform.localScale = Vector3.zero;
        particle = originalGameObject.transform.GetChild(0).gameObject.transform.GetComponent<UIParticle>();
        particle.Stop();
    }

    public void StartParticle()
    {
        particle.Play();
    }
}

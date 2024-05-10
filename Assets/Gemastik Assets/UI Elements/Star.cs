using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image star;
    void Awake()
    {
        star = GetComponent<Image>();
        star.transform.localScale = Vector3.zero;
    }
}

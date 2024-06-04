using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    Transform car_transform;
    RectTransform car_rectTransform;

    Image bridge;
    UIParticle smoke;

    bool runCar = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject originalGameObject = this.gameObject;
        car_transform = originalGameObject.transform.GetChild(1).gameObject.GetComponent<Transform>();
        car_rectTransform = (RectTransform) car_transform;

        bridge = originalGameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        bridge.fillAmount = 0;

        smoke = originalGameObject.transform.GetChild(3).gameObject.GetComponent<UIParticle>();
        smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        bridge.fillAmount += 0.005f;
        if (bridge.fillAmount == 1.0f && !runCar)
        {
            smoke.Play();
            runCar = true;
        }
        if (runCar)
        {
            if (car_transform.position.x < Screen.width + car_rectTransform.rect.width*3)
            {
                car_transform.Translate(Vector2.right * 250 * Time.deltaTime);
                smoke.transform.Translate(Vector2.down * 250 * Time.deltaTime);
            }
            else
            {
                this.GetComponent<CanvasGroup>().alpha -= 0.01f;
            }
        }
     
    }
}

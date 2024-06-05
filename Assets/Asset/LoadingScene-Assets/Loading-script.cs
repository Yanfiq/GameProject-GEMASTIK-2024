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

    [SerializeField] float loading_time = 5.0f;

    void Start()
    {
        GameObject originalGameObject = this.gameObject;
        car_transform = originalGameObject.transform.GetChild(1).gameObject.GetComponent<Transform>();
        car_rectTransform = (RectTransform) car_transform;

        bridge = originalGameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        bridge.fillAmount = 0;

        smoke = originalGameObject.transform.GetChild(3).gameObject.GetComponent<UIParticle>();
        smoke.Stop();
        ParticleSystem ps =  smoke.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.duration = loading_time;
        main.startLifetime = loading_time/2;

        car_transform.position = new Vector2(-car_rectTransform.rect.width, car_transform.position.y);
        smoke.transform.position = new Vector2(car_transform.position.x - car_rectTransform.rect.width/2, smoke.transform.position.y);
    }

    void Update()
    {
        bridge.fillAmount += 1.0f / (loading_time/2) * Time.deltaTime;
        if (bridge.fillAmount == 1.0f && !runCar)
        {
            smoke.Play();
            runCar = true;
        }
        if (runCar)
        {
            if (car_transform.position.x < Screen.width + car_rectTransform.rect.width*2)
            {
                car_transform.Translate(Vector2.right * ((Screen.width + car_rectTransform.rect.width * 2) / (loading_time / 2)) * Time.deltaTime);
                smoke.transform.Translate(Vector2.down * ((Screen.width + car_rectTransform.rect.width * 2) / (loading_time / 2)) * Time.deltaTime);
            }
            else
            {
                this.GetComponent<CanvasGroup>().alpha -= 0.01f;
            }
        }
     
    }
}

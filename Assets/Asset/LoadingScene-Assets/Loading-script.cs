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
        car_rectTransform = (RectTransform)car_transform;

        bridge = originalGameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        bridge.fillAmount = 0;

        smoke = originalGameObject.transform.GetChild(3).gameObject.GetComponent<UIParticle>();
        smoke.Stop();
        ParticleSystem ps = smoke.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.duration = loading_time;
        main.startLifetime = loading_time / 2;

        car_transform.position = new Vector2(-car_rectTransform.rect.width, car_transform.position.y);
        smoke.transform.position = new Vector2(car_transform.position.x - car_rectTransform.rect.width / 2, smoke.transform.position.y);
    }

    void Update()
    {
        if (bridge.fillAmount < 1.0f && !runCar)
        {
            bridge.fillAmount += 1.0f / (loading_time / 2) * Time.deltaTime;
            if (bridge.fillAmount >= 1.0f)
            {
                bridge.fillAmount = 1.0f;
                runCar = true;
                smoke.Play();
                bridge.fillOrigin = (int)Image.OriginHorizontal.Right;
            }
        }
        else if (runCar)
        {
            // Move the car and smoke
            float moveDistance = (Screen.width + car_rectTransform.rect.width * 2) / (loading_time / 2) * Time.deltaTime;
            car_transform.Translate(Vector2.right * moveDistance);
            smoke.transform.Translate(Vector2.down * moveDistance);
            
            if (car_transform.position.x - car_rectTransform.rect.width >= 0)
            {
                bridge.fillAmount -= 1.0f / (loading_time / 2.7f) * Time.deltaTime;
            }

            if (car_transform.position.x >= Screen.width + car_rectTransform.rect.width)
            {
                CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= 0.01f;
                }
                else
                {
                    canvasGroup.alpha = 0;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

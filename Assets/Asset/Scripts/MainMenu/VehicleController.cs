using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    Transform SUV;
    Transform Hatchback;
    float rightBorder_x;
    float leftBorder_x;
    [SerializeField] float velocity = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject originalGameObject = this.gameObject;
        SUV = originalGameObject.transform.GetChild(0).gameObject.GetComponent<Transform>();
        Hatchback = originalGameObject.transform.GetChild(1).gameObject.GetComponent<Transform>();

        //blind spot initialization
        rightBorder_x = GameObject.Find("supermarket").GetComponent<Transform>().position.x;
        leftBorder_x = GameObject.Find("Stripped Road Node (5)").transform.GetChild(2).gameObject.GetComponent<Transform>().position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //suv controll
        if(SUV.position.x < rightBorder_x)
        {
            SUV.Translate(Vector3.down * velocity * Time.deltaTime);
        }
        else
        {
            SUV.Translate(Vector3.up * (SUV.position.x - leftBorder_x));
        }

        if(Hatchback.position.x > leftBorder_x)
        {
            Hatchback.Translate(Vector3.down * velocity * Time.deltaTime);
        }
        else
        {
            Hatchback.Translate(Vector3.up * (rightBorder_x - Hatchback.position.x));
        }

        // tires muter-muter
        for(int i=1; i<=4; i++)
        {
            this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).GetComponent<Transform>().Rotate(new Vector3(velocity, 0, 0));
            this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).GetComponent<Transform>().Rotate(new Vector3(velocity, 0, 0));
        }
    }
}

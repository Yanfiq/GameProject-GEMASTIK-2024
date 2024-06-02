using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour
{
    Camera cam;
    Node currentModule;

    [SerializeField] Transform ghostTransform;
    [SerializeField] GameObject spawnObj;

    [SerializeField] List<PlateRoad> plateRoads = new List<PlateRoad>();

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (spawnObj.transform.rotation.y >= 360) spawnObj.transform.rotation = Quaternion.Euler(Vector3.zero);
            spawnObj.transform.Rotate(0, 90, 0);
            ghostTransform.GetChild(0).Rotate(0, 90, 0);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.GetComponent<Node>() != null /*&&
            hitInfo.collider.gameObject.GetComponent<Node>().isWalkable*/)
            {
                currentModule = hitInfo.collider.gameObject.GetComponent<Node>();
                ghostTransform.position = currentModule.transform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    Node newNode = Instantiate(spawnObj, new Vector3(currentModule.transform.position.x, .03f, currentModule.transform.position.z), spawnObj.transform.rotation).GetComponent<Node>();
                    newNode.Build();
                }
            }
        }
    }

    public void ChangeModule(int i)
    {
        spawnObj = plateRoads[i].roadNode.gameObject;
        Destroy(ghostTransform.GetChild(0).gameObject);
        Instantiate(plateRoads[i].plate, ghostTransform);
    }

}

[Serializable]
public struct PlateRoad
{
    public GameObject plate;
    public Node roadNode;
}
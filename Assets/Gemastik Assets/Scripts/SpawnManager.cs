using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour
{
    Camera cam;
    Transform currentModule;

    [SerializeField] Transform ghostTransform;
    [SerializeField] Transform plateParent;
    [SerializeField] Transform spawnObjParent;
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
            spawnObjParent.transform.Rotate(0, 90, 0);
            plateParent.Rotate(0, 90, 0);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.GetComponent<BuildNode>()/*&&
            hitInfo.collider.gameObject.GetComponent<Node>().isWalkable*/)
            {
                currentModule = hitInfo.collider.transform;
                ghostTransform.position = currentModule.position;

                if (Input.GetMouseButtonDown(0))
                {
                    Node newNode = Instantiate(spawnObj, new Vector3(currentModule.position.x, .03f, currentModule.position.z), spawnObjParent.transform.rotation).GetComponent<Node>();
                    newNode.Build();
                }
            }
        }
    }

    public void ChangeModule(int i)
    {
        plateParent.rotation = Quaternion.Euler(Vector3.zero);
        spawnObjParent.rotation = Quaternion.Euler(Vector3.zero);
        spawnObj = plateRoads[i].roadNode.gameObject;
        Destroy(plateParent.GetChild(0).gameObject);
        Instantiate(plateRoads[i].plate, plateParent);
    }

}

[Serializable]
public struct PlateRoad
{
    public GameObject plate;
    public Node roadNode;
}
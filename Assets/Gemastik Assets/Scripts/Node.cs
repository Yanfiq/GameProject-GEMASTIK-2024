using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isWalkable = true;
    public List<Node> neighbours = new List<Node>();

    Color currColor;
    MeshRenderer mr;

    public float g, h;
    //public Node parent;

    void Awake()
    {
        mr = GetComponentInChildren<MeshRenderer>();
        currColor = mr.material.color;
    }

    public void addNeighbour(Node newNeighbour)
    {
        neighbours.Add(newNeighbour);
    }

    public Vector3 getLoc() { return transform.position; }
    public void ChangeColor(Color c) { mr.material.color = c; currColor = c; }

    void OnMouseEnter()
    {
        mr.material.color = Color.red;
    }

    void OnMouseExit()
    {
        mr.material.color = currColor;
    }
}

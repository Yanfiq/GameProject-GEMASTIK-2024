using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public List<Transform> pathNode = new List<Transform> ();

    void Update()
    {
        
    }

    public void AddPath(Transform t)
    {
        pathNode.Add(t);
    }
}

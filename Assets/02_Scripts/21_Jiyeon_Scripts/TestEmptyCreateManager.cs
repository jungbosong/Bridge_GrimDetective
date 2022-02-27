using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEmptyCreateManager : MonoBehaviour
{
    public GameObject testEmptyPrefab;
    public GameObject child;
    public GameObject parent;
    public void CreateEmptyChild()
    {
        child = Instantiate(testEmptyPrefab);
        child.transform.SetParent(parent.transform);
    }
}

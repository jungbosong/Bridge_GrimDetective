using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class layer : MonoBehaviour
{
    public Transform ChoiceScrollView;
    public void layerorder(){
        ChoiceScrollView.position = new Vector3(0, 0, -10);
        Debug.Log("ChoiceScrollview 뒤로 빼기");
    }
}
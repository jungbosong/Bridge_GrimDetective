using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChecker : MonoBehaviour
{
    void Awake() 
    {
        RectTransform rectTransform;
        rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;  
        Debug.Log("name: " + this.gameObject.name);
        Debug.Log("width: " + width);
        Debug.Log("height: " + height);
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    public ScrollRect ParentScroll;
    public void OnBeginDrag(PointerEventData e){
        ParentScroll.OnBeginDrag(e);
    }
    public void OnDrag(PointerEventData e){
        ParentScroll.OnDrag(e);
    }
    public void OnEndDrag(PointerEventData e){
        ParentScroll.OnEndDrag(e);
    }
    public void OnScroll(PointerEventData eventData){
        ParentScroll.OnScroll(eventData);      
    }
}
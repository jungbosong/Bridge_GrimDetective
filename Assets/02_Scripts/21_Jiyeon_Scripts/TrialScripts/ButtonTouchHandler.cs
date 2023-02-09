using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTouchHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject DialogueArea;
    [SerializeField] Scrollbar SB;
    [SerializeField] ChatCanvas chatCanvas;
    ScrollRect SR;
    float startX, startY, endX, endY;
    int contentSize;
    

    void Awake() 
    {
        SR = DialogueArea.transform.GetComponentInChildren<ScrollRect>();
        SB = SB.GetComponent<Scrollbar>();
        chatCanvas = chatCanvas.GetComponent<ChatCanvas>();
        contentSize = SR.content.transform.childCount;
    }

    public void OnBeginDrag(PointerEventData e) 
    {
        SR.OnBeginDrag(e);
    }
    
    public void OnDrag(PointerEventData e) 
    {
        SR.OnDrag(e);
    }

    public void OnEndDrag(PointerEventData e) 
    {
        SR.OnEndDrag(e);
    }

    public void OnPointerDown(PointerEventData e) 
    {
        startX = e.position.x;
        startY = e.position.y;
    }
        
    public void OnPointerUp(PointerEventData e) 
    {
        endX = e.position.x;
        endY = e.position.y;

        if(endX-startX <= 1.0 && endY-startY <= 1.0)
        {
            Debug.Log("터치입니다");
            chatCanvas.OnClickedDialogueBtn();
            Canvas.ForceUpdateCanvases();
            SR.verticalNormalizedPosition = 0f;
            SR.verticalScrollbar.value = 0;
            Canvas.ForceUpdateCanvases();
        }
        else
        {
            Debug.Log("드래그입니다");
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvasData : MonoBehaviour
{
    public MoveUIManager moveUIManager;
    public List<GameObject> roomCanvaseObjects = new List<GameObject>();

    void Start()
    {
        int count = 0;
        for(int i = 0; i<moveUIManager.maxFloor; i++) {
            for(int j = 0; j<moveUIManager.maxRoom; j++){
                if(roomCanvaseObjects[count] != null) {
                    moveUIManager.roomCanvases[i, j] = roomCanvaseObjects[count];
                   // Debug.Log(moveUIManager.roomCanvases[i, j]);
                    count++;
                }
                else{
                    count++;
                }
            }
        }
        moveUIManager.InvisileRoom();
        moveUIManager.VisibleRoom();
    }
}

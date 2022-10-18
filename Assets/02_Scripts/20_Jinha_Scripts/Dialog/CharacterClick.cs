using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClick : MonoBehaviour
{
    Characterid Characterid;
    public Transform Canvas;
    public GameObject ChatPanel;
    public GameObject newChatPanel;
    public int CharacterId;
    public void CharacterButtonClick()
    {
        newChatPanel = Instantiate(ChatPanel,Canvas); 
        //Characterid.CharacterId=CharacterId;
    }
}
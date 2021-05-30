using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour, Interaction
{
    [SerializeField] Textman textman;

    public void Interact()
    { 

        StartCoroutine(TextManager.instance.ShowText(textman));
        Debug.Log("interacting with npc");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;

public class RaycastToCheck : MonoBehaviour
{
    private ButtonReciever reciever;
    private UIPrompt textPrompt;

    private void Update()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        if(Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.position ,out RaycastHit hit))
        {
            if (!hit.collider.CompareTag("ScaleButton")) return;
            
            reciever = hit.collider.gameObject.GetComponent<ButtonReciever>();
            CheckSize();
        }
        else
        {
            textPrompt = FindObjectOfType<UIPrompt>();
            textPrompt.CloseTextPrompt();
        }
    }

    private void CheckSize()
    {
        reciever.CheckPlayerSize();
    }
}

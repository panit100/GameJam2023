using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;

public class RaycastToCheck : MonoBehaviour
{
    private ButtonReciever reciever;

    private void Start()
    {
        
    }

    private void Update()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        if(Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.position ,out RaycastHit hit))
        {
            // Debug.Log($"Obj : {hit.collider.gameObject.name}");
            reciever = hit.collider.gameObject.GetComponent<ButtonReciever>();
            CheckSize();
        }
    }

    private void CheckSize()
    {
        reciever.CheckPlayerSize();
    }
}

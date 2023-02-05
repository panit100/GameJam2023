using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnding : MonoBehaviour
{
    private GetAchievement _achievement;

    private void Start()
    {
        _achievement = FindObjectOfType<GetAchievement>();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;
        
        Debug.Log("End");
        _achievement.GetEnding();
    }
}

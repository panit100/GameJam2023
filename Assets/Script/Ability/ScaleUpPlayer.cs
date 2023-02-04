using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;

public class ScaleUpPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;
            
        var _multiply = FindObjectOfType<ScaleSetting>().scaleUpMultiply;
            
        _col.gameObject.transform.localScale = new Vector3(_multiply,_multiply,_multiply);
        
        Destroy(this.gameObject);
    }
}

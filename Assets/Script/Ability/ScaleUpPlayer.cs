using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;
using GameJam.Utilities;


public class ScaleUpPlayer : MonoBehaviour
{
    SoundManager soundManager;
    
    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;
            
        var _multiply = FindObjectOfType<ScaleSetting>().scaleUpMultiply;
            
        soundManager.PlayExtendSFX();

        _col.gameObject.transform.localScale = new Vector3(_multiply,_multiply,_multiply);
        
        Destroy(this.gameObject);
    }
}

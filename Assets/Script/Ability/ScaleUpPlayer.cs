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
        
        var _scaleSet = FindObjectOfType<ScaleSetting>();

        if (_scaleSet.currentScale < 2)
        {
            _scaleSet.currentScale++;
        }

        var _multiply = _scaleSet.scaleValue[_scaleSet.currentScale];
        _col.gameObject.transform.localScale = new Vector3(_multiply,_multiply,_multiply);
        
        var curPos = transform.position;
        _col.gameObject.transform.position = new Vector3(curPos.x,curPos.y + 2f,curPos.z);
        
        _scaleSet.UpdateScaleLabelUi();

        //if (soundManager != null)
        //    soundManager.PlayShrinkSFX();


        Destroy(this.gameObject);
    }
}

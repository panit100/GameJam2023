using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;
using GameJam.Utilities;

public class ScaleDownPlayer : MonoBehaviour
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
            _scaleSet.currentScale--;
        }
        
        var _multiply = _scaleSet.scaleValue[_scaleSet.currentScale];
        _col.gameObject.transform.localScale = new Vector3(_multiply,_multiply,_multiply);

        soundManager.PlayShrinkSFX();

        Destroy(this.gameObject);
    }
}

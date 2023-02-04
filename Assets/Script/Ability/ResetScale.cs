using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;
using GameJam.Utilities;

public class ResetScale : MonoBehaviour
{
    SoundManager soundManager;
    
    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;
        
        var _multiply = FindObjectOfType<ScaleSetting>().normalScale;
            
        if(_col.gameObject.transform.localScale.x > _multiply)
            soundManager.PlayShrinkSFX();
        else
            soundManager.PlayExtendSFX();
        
        _col.gameObject.transform.localScale = new Vector3(_multiply,_multiply,_multiply);
        Destroy(this.gameObject);
    }
}

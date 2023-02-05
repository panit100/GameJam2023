using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;
using GameJam.Utilities;


public class ScaleUpPlayer : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField] float waitSetActiveTime = 5f;
    [SerializeField] GameObject[] model;

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

        foreach (GameObject obj in model)
        {
            obj.SetActive(false);
            this.GetComponent<Collider>().enabled = false;
        }
        StartCoroutine(waitSetActiveAgain(waitSetActiveTime));
    }
    IEnumerator waitSetActiveAgain(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject obj in model)
        {
            obj.SetActive(true);
            this.GetComponent<Collider>().enabled = true;
        }
    }
}

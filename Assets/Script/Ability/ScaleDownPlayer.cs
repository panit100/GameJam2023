using System.Collections;
using System.Collections.Generic;
using GameJam.GameData;
using UnityEngine;
using GameJam.Utilities;

public class ScaleDownPlayer : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField] bool setActiveAgain;
    [SerializeField] float waitSetActiveTime = 5f;


    void Start() 
    {
        soundManager = SharedObject.Instance.Get<SoundManager>();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(!_col.CompareTag("Player")) return;
        
        var _scaleSet = FindObjectOfType<ScaleSetting>();
        
        if (_scaleSet.currentScale < 3)
        {
            _scaleSet.currentScale--;
        }
        
        var _multiply = _scaleSet.scaleValue[_scaleSet.currentScale];
        _col.gameObject.transform.localScale = new Vector3(_multiply,_multiply,_multiply);

        if (soundManager != null)
            soundManager.PlayShrinkSFX();

        if (setActiveAgain == true)
        {
            this.gameObject.SetActive(false);
            StartCoroutine(waitSetActiveAgain(waitSetActiveTime));
            setActiveAgain = false;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator waitSetActiveAgain(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

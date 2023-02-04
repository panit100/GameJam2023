using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiMaterialsInstance : MonoBehaviour
{
    //private MaterialExposeController image;
    private Material imagemat;
     
    void Start()
    {
        if (TryGetComponent(out TextMeshProUGUI oof ))
        {
            imagemat = oof.materialForRendering;
            oof.fontMaterial = Instantiate(imagemat);
        }
      
        
    
    }
}
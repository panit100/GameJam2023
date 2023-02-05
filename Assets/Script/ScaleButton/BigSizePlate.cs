using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSizePlate : MonoBehaviour
{
    [SerializeField] private GameObject objToBeClear;
    private GameObject playerRef;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision _col)
    {
        if(!_col.gameObject.CompareTag("Player")) return;

        if (playerRef.transform.localScale == Vector3.one * 2f)
        {
            DestroyObstacle();
        }
    }

    private void DestroyObstacle()
    {
        Destroy(objToBeClear);
    }
}

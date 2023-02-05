using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] bool card;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            if (other.GetComponent<PlayerMovementController>() != null)
            {
                if (card == true)
                {
                    SharedObject.Instance.Get<GameplayController>().CollectCard();
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}

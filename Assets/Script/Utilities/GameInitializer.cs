using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.GameData;

namespace GameJam.Utilities
{
    public class GameInitializer : MonoBehaviour
    {
        private bool isInitialized;

        [SerializeField]
        private GameDataManager gameDataManager;

        void Start()
        {
            StartInitialize();
        }

        private void StartInitialize()
        {
            StartCoroutine(Initialze());
        }

        IEnumerator Initialze()
        {
            gameDataManager.StartInitialize();

            yield return new WaitUntil(() => gameDataManager.IsIniialized);

            isInitialized = true;

            Debug.Log("==================   GAME INITIALIZE COMPLETE   ====================");
        }
    }
}

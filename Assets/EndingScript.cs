using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;

public class EndingScript : MonoBehaviour
{

    public void EndCutscene()
    {
        SharedObject.Instance.Get<GameplayController>().OnLoadSceneMainMenu();
    } 
}

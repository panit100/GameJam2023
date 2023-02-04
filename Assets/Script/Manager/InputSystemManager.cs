using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using GameJam.Utilities;

public class InputSystemManager : MonoBehaviour
{
    private const string PLAYER_ACTIONMAP = "PlayerControl";
    private const string MENU_NAVIGATION_ACTIONMAP = "UI";

    public InputActionAsset playerInputAction;

    public InputSystemUIInputModule inputSystemUIInputModule;

    //UnityAction
    public UnityAction<Vector2> onMove;
    public UnityAction<bool> onPressMove;
    public UnityAction<bool> onPressRun;
    public UnityAction onFire;
    public UnityAction onClose;
    public UnityAction<bool> onInteract;
    public UnityAction onCheckClock;



    //InputActionMap
    InputActionMap playerControlMap;
    InputActionMap uiControlMap;

    //input state verification
    bool globleInputEnable = false;
    bool playerControlEnable = true;
    bool uiControlEnable = true;

    bool isInitlize;

    void Awake() 
    {
        Initilize();
    }

    void Start()
    {
        playerControlMap = playerInputAction.FindActionMap(PLAYER_ACTIONMAP);
        uiControlMap = playerInputAction.FindActionMap(MENU_NAVIGATION_ACTIONMAP);
        EnableGlobalInput();
    }

    void Initilize()
    {
        SharedObject.Instance.Add(this);

        isInitlize = true;
    }

    void UpdateInputState()
    {
        if(globleInputEnable && playerControlEnable) playerControlMap.Enable();
        else playerControlMap.Disable();

        if(globleInputEnable && uiControlEnable) uiControlMap.Enable();
        else uiControlMap.Disable();
    }

    public void EnableGlobalInput()
    {
        globleInputEnable = true;
        UpdateInputState();
    }

    public void DisableGlobalInput()
    {
        globleInputEnable = false;
        UpdateInputState();
    }

    public void SetPlayerControl(bool enable)
    {
        playerControlEnable = enable;
        UpdateInputState();
    }

    public void SetUIControl(bool enable)
    {
        uiControlEnable = enable;
        UpdateInputState();
    }

    #region ControlFunction
    //UI
    void OnClose(InputValue value)
    {
        if(value.isPressed)
        {
            onClose?.Invoke();
        }
    }

    //Player
    void OnMove(InputValue value)
    {
        if(value.Get<Vector2>() != Vector2.zero)
        {
            onMove?.Invoke(value.Get<Vector2>());
        }
    }

    void OnFire(InputValue value)
    {
        if(value.isPressed)
        {
            onFire?.Invoke();
        }
    }

    void OnPressMove(InputValue value)
    {
        if(value.isPressed)
            onPressMove?.Invoke(true);
        else
            onPressMove?.Invoke(false);
    }

    void OnPressRun(InputValue value)
    {
        if(value.isPressed)
            onPressRun?.Invoke(true);
        else
            onPressRun?.Invoke(false);
    }

    void OnInteract(InputValue value)
    {
        if (value.isPressed)
            onInteract?.Invoke(true);
        else
            onInteract?.Invoke(false);
    }
    
    void OnOpenClock(InputValue value)
    {
        if (value.isPressed)
            onCheckClock?.Invoke();
    }
    
    #endregion
}

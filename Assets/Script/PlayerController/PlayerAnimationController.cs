using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private PlayerMovementController movementController;

    public void Init(PlayerMovementController movementController)
    {
        this.movementController = movementController;
    }

    public float currentAnimatorStateInfoTime
    {
        get { return anim.GetCurrentAnimatorStateInfo(1).normalizedTime % 1; }
    }

    public bool currentAnimatorStateBaseIsName(string paramName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(paramName);
    }

    public void AnimationState(string paramName)
    {
        List<string> movementParams = new List<string> { "walk", "run", "idle" };

        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            if (movementParams.Contains(parameter.name))
            {
                anim.SetBool(parameter.name, false);
            }
        }

        anim.SetBool(paramName, true);
    }

    public void UseClockAnimation()
    {
        anim.SetTrigger("useClock");
        anim.SetBool("clock", true);
    }

    public void UnUseClockAnimation()
    {
        anim.SetBool("clock", false);
    }

    public void PlayJumpAnimation()
    {
        anim.SetBool("ground", false);
        anim.SetTrigger("jump");
    }

    public void Landing()
    {
        anim.SetBool("ground", true);
    }

    public void ResetAnimBoolean()
    {
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
    }
}

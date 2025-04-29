using UnityEngine;

public abstract class NpcController : GenericSingleton<NpcController>
{
    protected Animator MyAnimator;
    public virtual void Animate(string animationState) 
    {
        MyAnimator.SetTrigger(animationState);
    }
}

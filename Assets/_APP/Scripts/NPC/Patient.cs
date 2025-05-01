using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Patient : GenericSingleton<Patient>
{
    private Animator MyAnimator;

    private void Start()
    {
        MyAnimator = GetComponent<Animator>();
    }

    public virtual void Animate(string animationState)
    {
        print(gameObject.name + animationState);
        MyAnimator.SetTrigger(animationState);
    }
}

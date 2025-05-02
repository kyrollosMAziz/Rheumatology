using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class Doctor : GenericSingleton<Doctor>
{
    [FormerlySerializedAs("transformDetials")] [SerializeField] private List<TransformDetials> transformDetails;

    private Animator MyAnimator;

    private void Start()
    {
        MyAnimator = GetComponent<Animator>();
    }

    public  void Animate(string animationState)
    {
        print(gameObject.name + animationState);
        MyAnimator.SetTrigger(animationState);
    }

    public void UpdateTransform(GameSequencePhase phase)
    {
        MyAnimator.enabled = false;
        var targetTransform = FetchPositions(phase);
        transform.position = targetTransform.position;
        MyAnimator.enabled = true;
    }

    private Transform FetchPositions(GameSequencePhase phase)
    {
        foreach (var transformDetail in transformDetails)
        {
            if (transformDetail.gamePhase == phase)
            {
                return transformDetail.targetTransform;
            }
        }

        return transform;
    }
}

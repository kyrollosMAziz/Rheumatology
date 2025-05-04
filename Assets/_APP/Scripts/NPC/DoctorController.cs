using System.Collections.Generic;
using UnityEngine;

public class DoctorController : GenericSingleton<DoctorController>
{
    [SerializeField] private Animator defaultAnimator;
    [SerializeField] private GameObject defaultGameObject;
    [SerializeField] private List<TargetDetials> transformDetails;


    public void Animate(string animationState)
    {
        print(gameObject.name + animationState);
        defaultAnimator.SetTrigger(animationState);
    }

    public void UpdateTransform(GameSequencePhase phase)
    {
        var targetDetails = FetchPositions(phase);
        
        if (targetDetails.sequenceGameObject.name == defaultGameObject.name)
            return;
        
        targetDetails.sequenceGameObject.SetActive(true);
        defaultAnimator = targetDetails.sequenceGameObject.GetComponent<Animator>();
        defaultGameObject.SetActive(false);
        defaultGameObject = targetDetails.sequenceGameObject;
    }

    private TargetDetials FetchPositions(GameSequencePhase phase)
    {
        foreach (var transformDetail in transformDetails)
        {
            if (transformDetail.gamePhase == phase)
                return transformDetail;
        }

        return null;
    }
}
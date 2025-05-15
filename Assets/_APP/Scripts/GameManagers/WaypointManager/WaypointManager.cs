using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class WaypointManager : GenericSingleton<WaypointManager>
{
    [SerializeField] private List<WaypointSerialized> _waypoints;

    public void InitiateWaypointMovement(GameSequencePhase _sequencePhase, Transform objectToMove, UnityAction unityAction = null)
    {
        WaypointSerialized _waypointSerialized = _waypoints.FirstOrDefault<WaypointSerialized>(w => w.sequencePhase == _sequencePhase);
        ChangeLookAtTarget(_waypointSerialized);
        if (_waypointSerialized == null)
        {
            return;
        }
        PlayerManager.Instance.CameraContainer.MoveToTarget(_waypointSerialized, 0, unityAction);
    }
    private void ChangeLookAtTarget(WaypointSerialized _waypointSerialized) 
    {
        if (_waypointSerialized.DoctorTarget != null)
        {
            PlayerManager.Instance.CameraContainer.PatientPosition = _waypointSerialized.DoctorTarget;
        }
        else
        {
            PlayerManager.Instance.CameraContainer.PatientPosition = null;
        }
    }
}

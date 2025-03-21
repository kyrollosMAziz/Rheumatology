using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class WaypointManager : GenericSingleton<WaypointManager>
{
    [SerializeField] private List<WaypointSerialized> _waypoints;
    [SerializeField] private float _fixedDistance;
    [SerializeField] private float _smoothSpeed;

    private LinkedList<WaypointSerialized> waypointsLinkedList;
    private LinkedListNode<WaypointSerialized> waypointsNode;

    private void Awake()
    {
        waypointsLinkedList = new LinkedList<WaypointSerialized>(_waypoints);
        waypointsNode = waypointsLinkedList.First;
    }
    public void InitiateWaypointMovement(GameSequencePhase _sequencePhase, Transform objectToMove)
    {
        WaypointSerialized _waypointSerialized = _waypoints.FirstOrDefault<WaypointSerialized>(w => w.sequencePhase == _sequencePhase);
        if (_waypointSerialized == null)
        {
            return;
        }
        StartCoroutine(StartMoving());
        IEnumerator StartMoving()
        {
            Transform target = waypointsNode.Value.waypoints.First<Transform>();
            while (Vector3.Distance(objectToMove.position, target.position) > _fixedDistance)
            {
                objectToMove.Translate(target.position);
                Vector3 direction = target.position - objectToMove.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                objectToMove.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _smoothSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            if (CheckWaypointReached()) 
            {
                StartCoroutine(StartMoving());
            }
        }
    }
    private bool CheckWaypointReached()
    {
        waypointsNode = waypointsNode.Next;
        return waypointsNode == null;
    }

}

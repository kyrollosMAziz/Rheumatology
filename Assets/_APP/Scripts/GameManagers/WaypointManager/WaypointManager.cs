using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : GenericSingleton<WaypointManager>
{
    [SerializeField] List <WaypointSerialized> _waypoints;
    public void InitiateWaypointMovement() 
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSequencePhase
{
    phase0 = 0,
    phase1 = 1,
    phase2 = 2,
    phase3 = 3,
    phase4 = 4,
    phase5 = 5,
    phase6 = 6,
    phase7 = 7,
    phase8 = 8,
    phase9 = 9,
    phase10 = 10
};

[Serializable]
public class WaypointSerialized
{
    public GameSequencePhase sequencePhase;
    public List<Transform> waypoints = new List<Transform>();
}
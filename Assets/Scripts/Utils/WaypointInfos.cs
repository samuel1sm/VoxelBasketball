using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum LaunchTypes
{
    Curve, Line
}

[Serializable]
public class WaypointInfos
{
    public Transform pathPoints;
    public LaunchTypes isAnErrorPath;
}

[Serializable]
public class Waypoint
{
    public WaypointInfos[] waypoint;
}

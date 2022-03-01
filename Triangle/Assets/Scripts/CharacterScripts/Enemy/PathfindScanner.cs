using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfindScanner : MonoBehaviour
{

    public void ScanForPathfinding()
    {
        AstarPath.active.Scan();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfindScanner : MonoBehaviour
{

    public void ScanForPathfinding()
    {
        StartCoroutine("ScanDelay");
    }

    IEnumerator ScanDelay()
    {
        yield return new WaitForSeconds(1f);
        AstarPath.active.Scan();
    }
}

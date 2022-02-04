using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    private int moveingTo;
    public List<Vector3> pathPoints;
    private PlayerControl player;

    public void SetPath(List<Vector3> path)
    {
        pathPoints = path;
        Debug.Log("first point" + path[path.Count - 1]);
        moveingTo = pathPoints.Count - 2;
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerControl>();
        Debug.Log("player" + player.transform);
        //player.PlayerStart(this);
    }

    public void OnDrawGizmos()
    {
        if (pathPoints != null)
        {
            for (int i = 1; i < pathPoints.Count; i++)
            {
                Gizmos.DrawLine(pathPoints[i - 1], pathPoints[i]);
            }

            
        }
    }

    public IEnumerator<Vector3> GetNextPoint()
    {
        while (true)
        {
            yield return pathPoints[moveingTo];
            Debug.Log(moveingTo);
            Debug.Log(pathPoints[moveingTo]);
            Debug.Log(pathPoints[moveingTo - 1]);
            moveingTo = moveingTo - 1;
        }
    }
}

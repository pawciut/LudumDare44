using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPath : MonoBehaviour
{
    public enum PathTypes //Types of movement paths
    {
        linear,
        loop
    }

    public PathTypes PathType; //Idicates type of path (Linear or Looping)
    public int movementDirection = 1; //1 clockwise/forward ||-1counter clockwise/backwards
    public int movingTo = 0; //used to identyfy point in PathSequence we are moving to 
    public Transform[] PathSequence; //Arry of all point in the path


    private void Update()
    {

    }
    public void OnDrawGizmos()
    {
        if (PathSequence == null || PathSequence.Length < 2)
        {
            return;
        }



        for (var i = 1; i < PathSequence.Length; i++)

        {
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);

        }

        {
            if (PathType == PathTypes.loop) ;

        }
        Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length - 1].position);
    }
    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathSequence == null || PathSequence.Length < 1)
        {
            yield break;
        }
        while (true)
        {
        }
    }
}


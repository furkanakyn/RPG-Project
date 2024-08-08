using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    const float wayPointRadius = 0.5f;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i);
            Gizmos.DrawSphere(GetWayPointPosition(i), wayPointRadius);
            Gizmos.DrawLine(GetWayPointPosition(i), GetWayPointPosition(j));
        }
    }

    public int GetNextIndex(int i)
    {
        if (i + 1 == transform.childCount)
        {
            return 0;
        }
        return i + 1;
    }

    public Vector3 GetWayPointPosition(int i)
    {
        return transform.GetChild(i).position;
    }
}

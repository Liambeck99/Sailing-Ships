using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : ShipBehaviour
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(ShipAgent agent, List<Transform> context, Fleet fleet)
    {
        // get distance from center as fraction of radius
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}

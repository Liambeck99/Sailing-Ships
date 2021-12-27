using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Fleet/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredShipBehaviour
{
    public override Vector2 CalculateMove(ShipAgent agent, List<Transform> context, Fleet fleet)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        // add all points together and average
        Vector2 cohesionMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        return cohesionMove;
    }
}

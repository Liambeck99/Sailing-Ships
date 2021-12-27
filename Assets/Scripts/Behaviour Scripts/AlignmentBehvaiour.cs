using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet/Behaviour/Alignment")]
public class AlignmentBehvaiour : FilteredShipBehaviour
{
    public override Vector2 CalculateMove(ShipAgent agent, List<Transform> context, Fleet fleet)
    {
        // if no neighbours, maintain alignment
        if (context.Count == 0)
            return agent.transform.up;

        // add all points together and average
        Vector2 alignmentMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}

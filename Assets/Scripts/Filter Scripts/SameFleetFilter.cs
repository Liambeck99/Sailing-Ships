using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet/Filter/Same Fleet")]
public class SameFleetFilter : ContextFilter
{
    public override List<Transform> Filter(ShipAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in original)
        {
            ShipAgent itemAgent = item.GetComponent<ShipAgent>();
            if (itemAgent != null && itemAgent.AgentFleet == agent.AgentFleet)
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}

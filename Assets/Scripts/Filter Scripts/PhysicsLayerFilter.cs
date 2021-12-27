using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(ShipAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in original)
        {
            // checks if mask and game object layer are equal, if true on same layer
            if (mask == (mask | (1 << item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipBehaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(ShipAgent agent, List<Transform> context, Fleet fleet);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet/Behaviour/Composite")]
public class CompositeBehaviour : FilteredShipBehaviour
{
    public ShipBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(ShipAgent agent, List<Transform> context, Fleet fleet)
    {
        // handle data mismatch
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        // set up move
        Vector2 move = Vector2.zero;

        // iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, fleet) * weights[i];

            // check a move has been returned
            if (partialMove != Vector2.zero)
            {
                // check not exceeded limit if so set to max
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                // update move
                move += partialMove;
            }
        }

        return move;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collider used for detecting ship location
[RequireComponent(typeof(Collider2D))]
public class ShipAgent : MonoBehaviour
{
    Fleet agentFleet;
    public Fleet AgentFleet { get { return agentFleet;  } } 

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Fleet fleet)
    {
        agentFleet = fleet;
    }

    public void Move(Vector2 velocity)
    {
        // Rotate Agent
        transform.up = velocity;

        // Move agent
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}

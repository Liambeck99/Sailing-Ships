using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public ShipAgent agentPrefab;
    List<ShipAgent> agents = new List<ShipAgent>();
    public ShipBehaviour behaviour;

    [Range(10, 400)]
    public int shipCount = 200;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        // Some paramaters needed later calc now to avoid doing multiple times later
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // create all ships
        for (int i = 0; i < shipCount; i++)
        {
            // instantiate ship using prefab somewhere inside circle
            ShipAgent newShip = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * shipCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );

            newShip.name = "Agent " + i;

            // tell ship which fleet it belongs to
            newShip.Initialize(this);

            agents.Add(newShip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ShipAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behaviour.CalculateMove(agent,
                                                   context,
                                                   this);

            move *= driveFactor;

            // if excedes max speed then set equal to max
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(ShipAgent agent)
    {
        List<Transform> context = new List<Transform>();

        // return list of other colliders that overlap with our agents position and given radius
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);

        // 
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider) // ignore self agent
            {
                context.Add(c.transform);  // store space vector of other agent
            }
        }

        return context;
    }
}

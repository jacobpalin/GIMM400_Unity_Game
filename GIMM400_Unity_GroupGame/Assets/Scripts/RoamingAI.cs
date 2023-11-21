using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//FOV video https://www.youtube.com/watch?v=j1-OyLo77ss
//Patrol Points documentation https://docs.unity3d.com/Manual/nav-AgentPatrol.html
public class RoamingAI : MonoBehaviour
{
    //Variables that probably don't need to be called from other scripts.
    [SerializeField] private Transform moveToThisObject; //transform of object to move to or away from (away in this case)
    [SerializeField] private Transform moveToThisObject2;
    private NavMeshAgent navMeshAgent; //the AI object with a nav mesh agent component
    [SerializeField] private float playerDistance; //distance from player to activate run away
    public int touched; //shows how many times the AI has been touched

    [SerializeField] public List<Transform> points; //empty game objects in the scene, manually drag them into inspector
    private int destPoint = 0; //current patrol point selected

    public float radius; //radius of FOV
    [Range(0, 360)] //FOV slider for angle of FOV
    public float angle; 

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;

    public bool canSeePlayer;

    void Awake()
    {
        //moveToThisObject = GameObject.FindGameObjectWithTag("Player").transform; //finds object to move to/away from
        navMeshAgent = GetComponent<NavMeshAgent>(); //grabs nav mesh component
        touched = 0; //resets touched

        // Disabling auto-braking allows for continuous movement between points (ie, the agent doesn't slow down as it approaches a destination point).
        navMeshAgent.autoBraking = false;

        GotoNextPoint();
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(moveToThisObject.position, transform.position); //calculates distance from player in vector2 for LOS
        Vector3 distance = transform.position - moveToThisObject.position; //calculates distance from player in vector3 for movement
        Vector3 newPos = transform.position + distance; //calculates position AI should run to

        float distanceFromPlayer2 = Vector2.Distance(moveToThisObject2.position, transform.position);
        Vector3 distance2 = transform.position - moveToThisObject2.position;
        Vector3 newPos2 = transform.position + distance2;
        // Choose the next destination point when the agent gets close to the current one.
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            GotoNextPoint();
        //tells AI run away if in line of sight
        if (canSeePlayer && distanceFromPlayer > playerDistance)
        {
            navMeshAgent.SetDestination(newPos);
        }
        else if (!canSeePlayer && distanceFromPlayer < playerDistance)
        {
            navMeshAgent.SetDestination(newPos);
        }
        if (canSeePlayer && distanceFromPlayer2 > playerDistance)
        {
            navMeshAgent.SetDestination(newPos2);
        }
        else if (!canSeePlayer && distanceFromPlayer2 < playerDistance)
        {
            navMeshAgent.SetDestination(newPos2);
        }

        //destroys after touched 3 times
        if (touched >= 3)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }

    //if AI collides with player, teleports to random patrol point and adds to touched counter
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform teleportToThis = points[Random.Range(0, points.Count)];
            this.transform.position = teleportToThis.position;
            Counter();
        }
    }

    //adds 1 to touched
    void Counter()
    {
        touched++;
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        navMeshAgent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }

    //waits a bit before calling FOV check
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask); //checks for objects with the player layer in a sphere

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform; //sets target to first player seen
            Vector3 directionToTarget = (target.position - transform.position).normalized; //calculates direction of the player

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position); //calculates distance from player

                //checks if AI can see player with direction, distance, and if any obstacles are in the way
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
        }
    }
}
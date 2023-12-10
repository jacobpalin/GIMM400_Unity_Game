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

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    void Awake()
    {
        //moveToThisObject = GameObject.FindGameObjectWithTag("Player").transform; //finds object to move to/away from
        navMeshAgent = GetComponent<NavMeshAgent>(); //grabs nav mesh component
        touched = 0; //resets touched

        // Disabling auto-braking allows for continuous movement between points (ie, the agent doesn't slow down as it approaches a destination point).
        navMeshAgent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        
        // Choose the next destination point when the agent gets close to the current one.
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            GotoNextPoint();
        //tells AI run away if in line of sight
        
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
        if(other.gameObject == player1 && touched >= 3)
        {
            player1.GetComponent<PlayerController>().playerSpeed = player1.GetComponent<PlayerController>().playerSpeed * 2;
        }
        else if (other.gameObject == player2 && touched >= 3)
        {
            player2.GetComponent<ArduinoControls>().playerSpeed = player2.GetComponent<ArduinoControls>().playerSpeed * 2;
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

        destPoint = Random.Range(0, points.Count);

        // Set the agent to go to the currently selected destination.
        navMeshAgent.destination = points[destPoint].position;
    }
}
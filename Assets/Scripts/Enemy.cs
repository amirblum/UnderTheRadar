using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public Vector3 targetPos;
    public float minDistanceTarget = 10.0f;
    public float maxDistanceTarget = 40.0f;

    [SerializeField] float _moveSpeed;
    private Rigidbody _rigidBody;
    private NavMeshAgent _navMeshAgent;
    private Radarable _radarable;

    public bool isRadarOn;

    private PlayerController player;
    private BoxCollider myCollider;
    private Vector3 topRightCnr;
    private Vector3 lowLeftCnr;

    private static BoxCollider[] obstacles;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _radarable = GetComponent<Radarable>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        myCollider = GetComponent<BoxCollider>();
        targetPos = transform.position;
        topRightCnr = GameObject.Find("TopRightCnr").GetComponent<Transform>().position;
        lowLeftCnr = GameObject.Find("LowLeftCnr").GetComponent<Transform>().position;
    }

    private void Start()
    {
        _radarable.OnRadarHitEvent += TurnRadarStateOn;
        _radarable.OnRadarEndEvent += TurnRadarStateOff;
        if (obstacles == null)
        {
            //int k;
            //int numObstacles = GameObject.FindGameObjectsWithTag("Obstacle").Length;
            // initialize the walls in the scene
            //rocks = new GameObject[numObstacles];
            //obstacles = new Collider[numObstacles];
            GameObject[] rocks;
            rocks = GameObject.FindGameObjectsWithTag("Obstacle");
            obstacles = new BoxCollider[rocks.Length];
            for (int k = 0; k < rocks.Length; k++) {
                obstacles[k] = rocks[k].GetComponent<BoxCollider>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the player in an interval of time after the radar was on
        if (isRadarOn) {
            MoveToPlayer();
        }
        // MoveRandomaly according to behaviour
        else
        {
            ResumeNormalBehavior();
        }
    }


    private void TurnRadarStateOn()
    {
        isRadarOn = true;
    }

    private void TurnRadarStateOff()
    {
        isRadarOn = false;
    }

    public void MoveToPlayer()
	{
        // moves the enemy straight towards the player
        var myPos = transform.position;
        targetPos = player.transform.position;
        //_rigidBody.velocity =  (targetPos - myPos).normalized * _moveSpeed * Time.deltaTime;
        _navMeshAgent.destination = targetPos;
    }

    public void ResumeNormalBehavior()
	{
        var myPos = transform.position;
        // Check if reached the destination
        if (Vector3.Distance(myPos, targetPos) < myCollider.size.x || !isValidInTerrain(targetPos)) {
            // if reached the goal destination (and the player isn't there, o.w will game over), choose a new destination
            Vector2 onUnityCircle = Random.insideUnitCircle;
            float distance = Random.Range(minDistanceTarget, maxDistanceTarget);
            targetPos = myPos + new Vector3(onUnityCircle.x,0.0f,onUnityCircle.y) * distance;
            _navMeshAgent.destination = targetPos;
        }
        Debug.DrawLine(targetPos + new Vector3(-1, 0, 0), targetPos + new Vector3(1, 0, 0));
        // continue in the target point direction
        //_rigidBody.velocity = (targetPos - myPos).normalized * _moveSpeed * Time.deltaTime;

    }


    private bool isValidInTerrain(Vector3 pos)
    {
        // Ruterns true if is inside the board and is not inside an obstacle

        if (lowLeftCnr.x > pos.x || pos.x > topRightCnr.x || lowLeftCnr.z > pos.z || pos.z > topRightCnr.z) { 
            return false;
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].bounds.Contains(pos)) {
                return false;
            }
        }
        return true;
    }

}





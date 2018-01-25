using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public float minDistanceTarget, maxDistanceTarget;
    public float waitTimer;
    public float minTime = 5, maxTime = 10;
    public bool isGoalChangeLocation = true;

    private Vector3 topRightCnr;
    private Vector3 lowLeftCnr;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    private static BoxCollider[] obstacles;

    private void Awake()
    {
        topRightCnr = GameObject.Find("TopRightCnr").GetComponent<Transform>().position;
        lowLeftCnr = GameObject.Find("LowLeftCnr").GetComponent<Transform>().position;
        minDistanceTarget = 10.0f;
        maxDistanceTarget = Mathf.Max(Mathf.Abs(topRightCnr.x - transform.position.x), Mathf.Abs(lowLeftCnr.x - transform.position.x));
        waitTimer = Random.Range(minTime, maxTime);
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        if (obstacles == null)
        {
            GameObject[] rocks;
            rocks = GameObject.FindGameObjectsWithTag("Obstacle");
            obstacles = new BoxCollider[rocks.Length];
            for (int k = 0; k < rocks.Length; k++)
            {
                obstacles[k] = rocks[k].GetComponent<BoxCollider>();
            }
        }
    }

    private void OnDestroy()
    {
        obstacles = null;
    }

    // Update is called once per frame
	void Update () {
        if (isGoalChangeLocation)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                Vector3 nextGoalPos;
                var myPos = transform.position;
                // move the goal randomly on the screen to a valid place
                Vector2 onUnityCircle = Random.insideUnitCircle;
                float distance = Random.Range(minDistanceTarget, maxDistanceTarget);
                nextGoalPos = myPos + new Vector3(onUnityCircle.x, 0.0f, onUnityCircle.y) * distance;
                if (isValidInTerrain(nextGoalPos))
                {
                    //transform.position = nextGoalPos;
                    _navMeshAgent.destination = nextGoalPos;
                    waitTimer = Random.Range(minTime, maxTime);
                }
            }
        }
    }

    
    

    private bool isValidInTerrain(Vector3 pos)
    {
        // Ruterns true if is inside the board and is not inside an obstacle

        if (lowLeftCnr.x > pos.x || pos.x > topRightCnr.x || lowLeftCnr.z > pos.z || pos.z > topRightCnr.z)
        {
            return false;
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].bounds.Contains(pos))
            {
                return false;
            }
        }
        return true;
    }


}

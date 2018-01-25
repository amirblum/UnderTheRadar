using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector3 targetPos;
    public float minDistanceTarget = 10.0f;
    public float maxDistanceTarget = 40.0f;

    [SerializeField] float _moveSpeed;
    private Rigidbody _rigidBody;
    private Radarable _radarable;

    public bool isRadarOn;

    private PlayerController player;
    private BoxCollider myCollider;
    private Vector3 lastPlayerPos;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _radarable = GetComponent<Radarable>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        myCollider = GetComponent<BoxCollider>();
        targetPos = transform.position;
    }

    private void Start()
    {
        _radarable.OnRadarHitEvent += TurnRadarStateOn;
        _radarable.OnRadarEndEvent += TurnRadarStateOff;
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
        _rigidBody.velocity =  (targetPos - myPos).normalized * _moveSpeed * Time.deltaTime;
    }

    public void ResumeNormalBehavior()
	{
        var myPos = transform.position;
        // Check if reached the destination
        if (Vector3.Distance(myPos, targetPos) < myCollider.size.x/2) {
            // if reached the goal destination (and the player isn't there, o.w will game over), choose a new destination
            Vector2 onUnityCircle = Random.insideUnitCircle;
            float distance = Random.Range(minDistanceTarget, maxDistanceTarget);
            targetPos = myPos + new Vector3(onUnityCircle.x,0.0f,onUnityCircle.y) * distance;
        }
        // continue in the target point direction
        _rigidBody.velocity = (targetPos - myPos).normalized * _moveSpeed * Time.deltaTime;
    }
}





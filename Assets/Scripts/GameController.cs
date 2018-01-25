using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool radarOn;
	public float radarFrequency;
	public float radarDuration;
	private float _radarTimer;
	
	public event Action OnRadarEvent;

	public static GameController Instance;

	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		// show menu
	}
	
	// Update is called once per frame
	void Update () {
		
        // check end of the game conditions:
        //      check with player if reached the goal
        //      check with player if it hit any enemy

        // if player won, show Mazal Tov and ask if wants to move to the next level or exit

        // if player lost, decrease life. exit and show the main menu if finished life, otherwise
        // start again the level
        
		
		// Do radar
		if (_radarTimer > radarFrequency)
		{
			if (OnRadarEvent != null)
			{
				OnRadarEvent();
			}
			_radarTimer = 0.0f;
		}
		_radarTimer += Time.deltaTime;
	}
    
    void WinGame () {

    }

    void LoseGame () {

    }



}


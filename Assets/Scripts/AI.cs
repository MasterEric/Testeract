using UnityEngine;
using System.Collections;

public class TAI : MonoBehaviour {

	public enum botState {

		Idle = 0,
		DefendingFlag = 1,
		TrackingEnemy = 2,
		AttemptingFlagCapture = 3,
		CapturingFlag = 4,
		DefendingTeammate = 5,
		DefendingFlagHolder = 6,
		AttackingFlagThief = 7

	}

	void Start () {



	}
	
	void Update () {
	
	}

	public void addBot(string botName){

	}
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	PlayerStatus status;
	PlayerAction action;
	int[,] statusData; 

	void Awake(){
		status = new PlayerStatus ();
		action = GetComponent<PlayerAction>();
		statusData = FileLoader.readTextAsInt ("Player/exptable");
		status.level = 0;
		status.initStatus (statusData [status.level, 1], statusData [status.level, 0], 0.0f, 100, 0, statusData[status.level,2]);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			status.exp = statusData[(status.level+1),2];
		}
		if (status.exp >= statusData [(status.level + 1), 2]) {
			levelup();
			Debug.Log(status.level + "," + statusData [status.level, 1] + "," + statusData [status.level, 0] + "," + statusData[status.level,2]);
		}


//		if (Input.GetKeyDown (KeyCode.D)) {
//			action.move(BaseBehaviourScript.DIRECTION.RIGHT,1);
//		}
//		if (Input.GetKeyDown (KeyCode.C)) {
//			action.move(BaseBehaviourScript.DIRECTION.DOWN_RIGHT,1);
//		}
//		if (Input.GetKeyDown (KeyCode.E)) {
//			action.move(BaseBehaviourScript.DIRECTION.UP_RIGHT,1);
//		}
//		if (Input.GetKeyDown (KeyCode.A)) {
//			action.move(BaseBehaviourScript.DIRECTION.LEFT,1);
//		}
//		if (Input.GetKeyDown (KeyCode.Z)) {
//			action.move(BaseBehaviourScript.DIRECTION.DOWN_LEFT,1);
//		}
//		if (Input.GetKeyDown (KeyCode.Q)) {
//			action.move(BaseBehaviourScript.DIRECTION.UP_RIGHT,1);
//		}
//		if (Input.GetKeyDown (KeyCode.W)) {
//			action.move(BaseBehaviourScript.DIRECTION.UP,1);
//		}
//		if (Input.GetKeyDown (KeyCode.X)) {
//			action.move(BaseBehaviourScript.DIRECTION.DOWN,1);
//		}

	}

	void levelup(){
		if (status.level < 70) {
			float percentage = (float)(status.hp / statusData[status.level,1]);
			status.level++;
			status.hp = statusData[status.level,1];
			status.hp = (int)(status.hp * percentage);
			status.ap = statusData[status.level,0];
			status.hungry = 100;
		}
	}
}

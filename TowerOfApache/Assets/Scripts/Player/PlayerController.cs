using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	PlayerStatus status;
	int[,] statusData; 

	void Awake(){
		status = new PlayerStatus ();
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

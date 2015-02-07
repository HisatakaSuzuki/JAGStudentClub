using UnityEngine;
using System.Collections;

public class Stairway : MonoBehaviour {
	GameObject player;
	public static bool goup; 
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		goup = false;
	}
	/*
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("Enter");
		if (col.gameObject.tag == "Player") {
			playerflag = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		Debug.Log ("Exit");
		if (col.gameObject.tag == "Player") {
				playerflag = false;
		}
	}*/

	void OnGUI(){
		if (player.transform.position == this.transform.position) {
			if(GUI.Button(new Rect(0,0,250,250),"のぼる")){
				goup = true;
			}
		}
	}
}

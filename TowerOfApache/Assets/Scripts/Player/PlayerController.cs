using UnityEngine;
using System.Collections;

//プレイヤーを操作するためのメインスクリプト
public class PlayerController : MonoBehaviour {
	PlayerStatus status;
	PlayerAction action;
	bool actionFlag;
	Animator animator;
	ItemPorch itemporch;

	void Awake(){
		actionFlag = true;
		status = new PlayerStatus ();
		action = GetComponent<PlayerAction>();
		status.initStatusData ();
		status.initStatus ();
		animator = GetComponent<Animator> ();
		itemporch = GetComponent<ItemPorch> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (actionFlag) {
			FlickContoroller.flick ();
			string dir = FlickContoroller.dir;
			switch(dir){
			case "": break;
			case "a": 
				playerMove(BaseBehaviourScript.DIRECTION.RIGHT);
				break;
			case "b":
				playerMove(BaseBehaviourScript.DIRECTION.UP_RIGHT);
				break;
			case "c": 
				playerMove(BaseBehaviourScript.DIRECTION.UP);
				break;
			case "d": 
				playerMove(BaseBehaviourScript.DIRECTION.UP_LEFT);
				break;
			case "e": 
				playerMove(BaseBehaviourScript.DIRECTION.LEFT);
				break;
			case "f": 
				playerMove(BaseBehaviourScript.DIRECTION.DOWN_LEFT);
				break;
			case "g": 
				playerMove(BaseBehaviourScript.DIRECTION.DOWN);
				break;
			case "h": 
				playerMove(BaseBehaviourScript.DIRECTION.DOWN_RIGHT);
				break;
			default: break;
			}
			FlickContoroller.dir = "";
		}
	}

	//プレイヤーの移動時に呼ばれる
	void playerMove(BaseBehaviourScript.DIRECTION dir){
		bool flag = action.move(dir,1);
		if (flag) {
			animator.SetFloat("DirectionX",action.getDir(action.currentDirection()).x);
			animator.SetFloat("DirectionY",action.getDir(action.currentDirection()).y);
		}
	}
}

using UnityEngine;
using System.Collections;

public class BaseBehaviourScript : MonoBehaviour {
	//方向を定義
	public enum DIRECTION{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		UP_LEFT,
		UP_RIGHT,
		DOWN_LEFT,
		DOWN_RIGHT
	};

	//方向のパターンの単位ベクトル
	protected Vector2[] Direction = new Vector2[]{
		new Vector2(0,1.0f), 
		new Vector2 (0,-1.0f), 
		new Vector2 (-1.0f,0), 
		new Vector2 (1.0f,0),
		new Vector2 (-1.0f,1.0f), 
		new Vector2 (1.0f,1.0f),
		new Vector2 (-1.0f,-1.0f), 
		new Vector2 (1.0f,-1.0f)
	};

	protected DIRECTION cutternDir;	//現在の向き

	protected Vector2 moveDir;		//移動方向	
	protected Vector2 attackDir;		//攻撃方向

	//ヒットチェック用終始点
	protected Transform startpos,endpos;
	
	//攻撃対象のオブジェクト
	protected GameObject target;

	//現在の向きを返す
	protected DIRECTION currentDirection(){
		return this.cutternDir;
	}
	
	//準備した方向を返す
	protected Vector2 getDir(DIRECTION d){
		return this.Direction[(int)d];
	}
	
	protected void turn(){
		switch(cutternDir){
		case DIRECTION.UP: 
			cutternDir = DIRECTION.UP_RIGHT; 
			break;
		case DIRECTION.UP_RIGHT: 
			cutternDir = DIRECTION.RIGHT; 
			break;
		case DIRECTION.RIGHT: 
			cutternDir = DIRECTION.DOWN_RIGHT; 
			break;
		case DIRECTION.DOWN_RIGHT: 
			cutternDir = DIRECTION.DOWN; 
			break;
		case DIRECTION.DOWN: 
			cutternDir = DIRECTION.DOWN_LEFT; 
			break;
		case DIRECTION.DOWN_LEFT: 
			cutternDir = DIRECTION.LEFT; 
			break;
		case DIRECTION.LEFT: 
			cutternDir = DIRECTION.UP_LEFT; 
			break;
		case DIRECTION.UP_LEFT: 
			cutternDir = DIRECTION.UP; 
			break;
		}
	}

	// Use this for initialization
	void Start () {
		cutternDir = DIRECTION.UP;
	}

}

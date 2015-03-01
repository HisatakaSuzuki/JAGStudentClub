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

	protected DIRECTION currentDir;	//現在の向き

	protected Vector2 moveDir;		//移動方向	
	protected Vector2 attackDir;		//攻撃方向

	//ヒットチェック用終始点
	public Transform startpos,endpos;
	
	//攻撃対象のオブジェクト
	public GameObject target;

	//現在の向きを返す
	public DIRECTION currentDirection(){
		return this.currentDir;
	}
	
	//準備した方向を返す
	public Vector2 getDir(DIRECTION d){
		return this.Direction[(int)d];
	}

	/// <summary>
	/// 右回りに回る
	/// </summary>
	protected void turn(){
		switch(currentDir){
		case DIRECTION.UP: 
			currentDir = DIRECTION.UP_RIGHT; 
			break;
		case DIRECTION.UP_RIGHT: 
			currentDir = DIRECTION.RIGHT; 
			break;
		case DIRECTION.RIGHT: 
			currentDir = DIRECTION.DOWN_RIGHT; 
			break;
		case DIRECTION.DOWN_RIGHT: 
			currentDir = DIRECTION.DOWN; 
			break;
		case DIRECTION.DOWN: 
			currentDir = DIRECTION.DOWN_LEFT; 
			break;
		case DIRECTION.DOWN_LEFT: 
			currentDir = DIRECTION.LEFT; 
			break;
		case DIRECTION.LEFT: 
			currentDir = DIRECTION.UP_LEFT; 
			break;
		case DIRECTION.UP_LEFT: 
			currentDir = DIRECTION.UP; 
			break;
		}
	}

	// Use this for initialization
	void Start () {
		currentDir = DIRECTION.UP;
	}
}

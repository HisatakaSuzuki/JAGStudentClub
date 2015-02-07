using UnityEngine;
using System.Collections;

//移動と攻撃の処理をプレイヤーとエネミーに付加する
//Player/EnemyControllerから参照される
public class BaseBehaviour : MonoBehaviour {
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

	DIRECTION cutternDir;	//現在の向き
	Vector2 moveDir;		//移動方向	
	Vector2 attackDir;		//攻撃方向

	//ヒットチェック用終始点
	public Transform startpos,endpos;

	//攻撃対象のオブジェクト
	public GameObject target;

	//方向のパターンの単位ベクトル
	Vector2[] Direction = new Vector2[]{
		new Vector2(0,1.0f), 
		new Vector2 (0,-1.0f), 
		new Vector2 (-1.0f,0), 
		new Vector2 (1.0f,0),
		new Vector2 (-1.0f,1.0f), 
		new Vector2 (1.0f,1.0f),
		new Vector2 (-1.0f,-1.0f), 
		new Vector2 (1.0f,-1.0f)
	};

	// Use this for initialization
	void Start () {
		cutternDir = DIRECTION.UP;
	}
	
	// Update is called once per frame
	void Update () {
	}

	//攻撃関数
	//攻撃が当たればtrue,当たらなければfalse
	public bool attack(DIRECTION d){
		//返り値を初期状態でfalseに
		bool result = false;
		
		//移動ベクトルを決定
		attackDir = Direction [(int)d];
		
		//現在の座標を取得
		Vector2 pos = transform.position;
		
		//移動方向にstartpos設定
		startpos.localPosition = moveDir * 0.5f;
		//移動方向にendpos設定
		endpos.localPosition = attackDir;
		//ヒットチェック
		//trueになれば移動処理.falseなら終了.
		result = attackCheck ();
		
		return result;
	}

	bool attackCheck(){
		//bool flag;
		bool flag = false;
		Debug.DrawLine (startpos.position, endpos.position);

		//プレイヤーによる攻撃
		if (this.gameObject.tag == "Player") {
			if (Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Enemy"))) {
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach(GameObject e in enemies){
					if(e.transform.position == (this.transform.position+(Vector3)attackDir)){
						target = e;
						Debug.Log(e.name);
						flag = true;
					}
				}
			}
		}

		//エネミーによる攻撃
		if (this.gameObject.tag == "Enemy") {
			if (Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Player"))) {
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				if(player.transform.position == (this.transform.position+(Vector3)attackDir)){
					target = player;
					Debug.Log("player");
					flag = true;
				}
			}
		}
		return flag;
	}

	//移動関数
	//移動できればtrue,できなければfalse
	public bool move(DIRECTION d){
		//返り値を初期状態でfalseに
		bool result = false;

		//移動ベクトルを決定
		moveDir = Direction [(int)d];
		
		//現在の座標を取得
		Vector2 pos = transform.position;

		//移動方向にstartpos設定
		startpos.localPosition = moveDir * 0.5f;
		//移動方向にendpos設定
		endpos.localPosition = moveDir;
		//ヒットチェック
		//trueになれば移動処理.falseなら終了.
		result = moveCheck ();

		//resultがtrueなので移動可能
		if (result) {
			transform.position = pos + moveDir;
			cutternDir = d;
		}
		
		return result;
	}

	//移動関数
	//移動できればtrue,できなければfalse
	public bool move(DIRECTION d, int len){
		//返り値を初期状態でfalseに
		bool result = false;
		
		//移動ベクトルを決定
		moveDir = Direction [(int)d] * len;
		
		//現在の座標を取得
		Vector2 pos = transform.position;
		
		//移動方向にstartpos設定
		startpos.localPosition = moveDir * 0.5f;
		//移動方向にendpos設定
		endpos.localPosition = moveDir;
		//ヒットチェック
		//trueになれば移動処理.falseなら終了.
		result = moveCheck ();
		
		//resultがtrueなので移動可能
		if (result) {
			transform.position = pos + moveDir;
			cutternDir = d;
		}
		
		return result;
	}

	public bool moveCheck(){
		//bool flag;
		bool flag = true;
		Debug.DrawLine (startpos.position, endpos.position);
		if (Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Wall"))) {
			flag = false;
		}else if(Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Enemy"))){
			flag = false;
		}
		else if(Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Player"))){
			flag = false;
		}
		return flag;
	}
	
	//現在の向きを返す
	public DIRECTION currentDirection(){
		return this.cutternDir;
	}

	//準備した方向を返す
	public Vector2 getDir(DIRECTION d){
		return this.Direction[(int)d];
	}

	public void turn(){
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

}

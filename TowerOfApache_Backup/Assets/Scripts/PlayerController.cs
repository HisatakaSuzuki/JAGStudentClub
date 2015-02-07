using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//コンポーネント取得
	BaseBehaviour bb;	//移動/攻撃
	Animator anim;		//アニメーション
	Status status;		//レベル/HP/AP/DP/満腹度/状態異常/経験値
	ItemPorch item;
	Equipment equipment;

	//現在いる部屋
	public int currentRoom;

	//ステータス配列(0:レベル,1:攻撃力,2:HP,3:EXP)
	string[,] STATUS = new string[70,4];
	int currentlevel;	//現在のレベル

	void Awake(){
		bb = this.GetComponent<BaseBehaviour> ();
		anim = this.GetComponent<Animator>();
		status = this.GetComponent<Status>();
		item = this.GetComponent<ItemPorch> ();
		equipment = this.GetComponent<Equipment>();
	}


	// Use this for initialization
	void Start () {
		STATUS = FileManager.readTextFile ("exptable");
		status.setStatus(int.Parse(STATUS[0,0]),int.Parse(STATUS[0,1]),int.Parse(STATUS[0,2]),0.0f,5,0,int.Parse(STATUS[0,3]));
		Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
		currentlevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.playerturn) {
			moveController ();
		}
		//レベルアップできるかチェックしてレベルアップ
		if (currentlevel < 70) {
			levelup();
		}

		//装備デバッグ用
		if(Input.GetKeyDown(KeyCode.H)){
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
			if(equipment.setWeapon(1)){
				status.ap += equipment.attackPoint;
			}
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
		}else if(Input.GetKeyDown(KeyCode.J)){
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
			if(equipment.setArmor(9)){
				status.dp += equipment.DR;
			}
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
		}

		//装備外しデバッグ
		if(Input.GetKeyDown(KeyCode.Y)){
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
			status.ap -= equipment.attackPoint;
			equipment.takeWeapon();
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
		}else if(Input.GetKeyDown(KeyCode.U)){
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
			status.dp -= equipment.DR;
			equipment.takeArmor();
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
		}

		//レベルアップデバッグ用
		if (Input.GetKeyDown (KeyCode.L)) {
			status.exp = int.Parse(STATUS[currentlevel+1,3]);
			Debug.Log(status.hp+","+status.ap+","+status.dp+","+status.exp);
		}
	}

	void OnGUI(){
		bool flag;
		if (GUI.Button (new Rect(0,Screen.height-100,Screen.width/2,Screen.height-(Screen.height-100)),"こうげき")) {
			flag = bb.attack(bb.currentDirection());
			if(flag){
				Status s = bb.target.GetComponent<Status>();
				s.hp = s.hp - this.status.ap;
				//s.hp = s.hp - (int)(this.status.ap*(1-s.dp));
				Debug.Log("敵HP:"+s.hp);
			}
			Debug.Log("攻撃しています:"+flag);
		}
		if (GUI.Button (new Rect(Screen.width/2,Screen.height-100,Screen.width/2,Screen.height-(Screen.height-100)),"回転")) {
			bb.turn();
			anim.SetFloat ("DirectionX", bb.getDir (bb.currentDirection ()).x);
			anim.SetFloat ("DirectionY", bb.getDir (bb.currentDirection ()).y);
		}
	}

	//フリック入力/移動
	void moveController(){
		FlickContoroller.flick ();
		if (FlickContoroller.dir == "c") {
			playerAction(BaseBehaviour.DIRECTION.UP);
		} else if (FlickContoroller.dir == "g") {
			playerAction(BaseBehaviour.DIRECTION.DOWN);
		} else if (FlickContoroller.dir == "e") {
			playerAction(BaseBehaviour.DIRECTION.LEFT);
		} else if (FlickContoroller.dir == "a") {
			playerAction(BaseBehaviour.DIRECTION.RIGHT);
		} else if (FlickContoroller.dir == "d") {
			playerAction(BaseBehaviour.DIRECTION.UP_LEFT);
		} else if (FlickContoroller.dir == "f") {
			playerAction(BaseBehaviour.DIRECTION.DOWN_LEFT);
		} else if (FlickContoroller.dir == "b") {
			playerAction(BaseBehaviour.DIRECTION.UP_RIGHT);
		} else if (FlickContoroller.dir == "h") {
			playerAction(BaseBehaviour.DIRECTION.DOWN_RIGHT);
		}

		FlickContoroller.dir = "";

		if(Input.GetKeyDown(KeyCode.R)){
			bb.turn();
			anim.SetFloat ("DirectionX", bb.getDir (bb.currentDirection ()).x);
			anim.SetFloat ("DirectionY", bb.getDir (bb.currentDirection ()).y);
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			Debug.Log("攻撃しています");
			bb.attack(bb.currentDirection());
		}
	}

	//プレイヤーの入力による移動を処理
	void playerAction(BaseBehaviour.DIRECTION dir){
		bool flag = false;
		flag = bb.move (dir);
		if (flag) {
			anim.SetFloat ("DirectionX", bb.getDir (bb.currentDirection ()).x);
			anim.SetFloat ("DirectionY", bb.getDir (bb.currentDirection ()).y);

			GameManager.enemyturn = true;
			GameManager.playerturn = false;
		}
	}

	//敗北したとき
	void destroy(){
		//負けエフェクト
		//ゲームオーバー画面に
	}

	//自然治癒
	void naturalHeal(){
		status.hp += (int)(status.hp * 0.02f);
	}

	//満腹度を減らす
	void getHungry(){
		status.hungry -= 1;
	}

	void getFull(){
		status.hungry += 1;
	}

	void levelup(){
		if (status.exp >= int.Parse(STATUS [currentlevel+1, 3])) {
			currentlevel++;
			status.setStatus(int.Parse(STATUS[currentlevel,0]),int.Parse(STATUS[currentlevel,1]),int.Parse(STATUS[currentlevel,2]),status.dp,status.hungry,status.state,status.exp);
		}
	}
}

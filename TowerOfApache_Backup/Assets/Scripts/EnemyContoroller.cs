using UnityEngine;
using System.Collections;

public class EnemyContoroller : MonoBehaviour {
	BaseBehaviour bb;	//移動/攻撃
	Animator anim;		//アニメーション
	Status status;		//レベル/HP/AP/DP/満腹度/状態異常/経験値
	//SpriteRenderer srender;	//スプライトのレンダー用
	//public Sprite[] monster_sprites;//	モンスターのスプライト
	public int id;	//このモンスターのid
	static string[,] enemydata;	//monset.csvの中身

	int throughturn;

	public int moveID;
	public static int currentactid;	//エネミー全体で現在行動できるエネミーのID
	public bool lastflag;	//最終エネミーかどうか

	void Awake(){
		bb = this.GetComponent<BaseBehaviour> ();
		anim = this.GetComponent<Animator>();
		status = this.GetComponent<Status> ();
//		srender = this.GetComponent<SpriteRenderer> ();
		lastflag = false;
		currentactid = 0;
		throughturn = 0;
	}
	// Use this for initialization
	void Start () {
		enemydata = FileManager.readTextFile ("monster");
		id = 0;	//仮
		status.setStatus(0,/*int.Parse(enemydata[id,1])*/10,int.Parse(enemydata[id,2]),float.Parse(enemydata[id,3]),0,0,int.Parse(enemydata[id,4]));
//		srender.sprite = monster_sprites [id];
		this.name = enemydata[id,0];
		anim.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("enemyanim/Enemies/"+enemydata[id,5]+"_0"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			id++;
//			srender.sprite = monster_sprites [id];
		}

		if(Input.GetKeyDown(KeyCode.R)){
			id++;
//			srender.sprite = monster_sprites [id];
			this.name = enemydata[id,0];
			anim.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("enemyanim/Enemies/"+enemydata[id,5]+"_0"));
		}

		if (this.status.hp <= 0) {
			this.destroy();
		}
		if (GameManager.enemyturn) {
			enemyAction();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
				Debug.Log (currentactid);
		}
	}

	void enemyAction(){
		bool flag = false;

		if (currentactid == moveID) {
			//プレイヤーが攻撃範囲なら攻撃
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			//攻撃に失敗した場合は移動
			if(!flag){
				flag = enemyMove((BaseBehaviour.DIRECTION)(Random.Range(0,8)));
			}
			throughturn++;
			if(throughturn > 8){
				bb.turn();
				anim.SetFloat ("DirectionX", bb.getDir (bb.currentDirection ()).x);
				anim.SetFloat ("DirectionY", bb.getDir (bb.currentDirection ()).y);
				flag = true;
			}
		}

		//行動が完了された
		if (flag) {
			currentactid++;
			//この敵が最後なら
			if(lastflag){
				GameManager.playerturn = true;
				GameManager.enemyturn = false;
				currentactid = 0;
			}
		}
	}

	//プレイヤーの入力による移動を処理
	bool enemyMove(BaseBehaviour.DIRECTION dir){
		bool flag = false;

		flag = bb.move (dir);
		if (flag) {
			anim.SetFloat ("DirectionX", bb.getDir (bb.currentDirection ()).x);
			anim.SetFloat ("DirectionY", bb.getDir (bb.currentDirection ()).y);
		}

		return flag;
	}

	public void setID(int id){
		this.moveID = id;
	}

	public void setLastFlag(bool f){
		this.lastflag = f;
	}

	//敗北したとき
	void destroy(){
		this.gameObject.tag = "Finish";
		GameObject[] en = GameObject.FindGameObjectsWithTag ("Enemy");
		EnemyContoroller econ;
		for (int i=0; i < en.Length; i++) {
			econ = en[i].GetComponent<EnemyContoroller>();
			econ.setID(i);
			econ.setLastFlag(false);
			if(i==en.Length-1){
				econ.setLastFlag(true);
			}
		}
		//負けエフェクト
		Destroy (this.gameObject);
	}

}

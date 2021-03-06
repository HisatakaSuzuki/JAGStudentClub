﻿using UnityEngine;
using System.Collections;

public class PlayerAction : BaseBehaviourScript {
	/// <summary>
	/// 攻撃を行う. 当たればtrue,外れればfalse
	/// </summary>
	/// <param name="d">D.</param>
	public bool attack(DIRECTION d){
		//返り値を初期状態でfalseに
		bool result = false;
		
		//攻撃ベクトルを決定
		attackDir = Direction [(int)d];
		
		//攻撃方向にstartpos設定
		startpos.localPosition = attackDir * 0.5f;
		//攻撃方向にendpos設定
		endpos.localPosition = attackDir;
		//ヒットチェック
		//trueになれば移動処理.falseなら終了.
		result = attackCheck ();
		
		return result;
	}

	bool attackCheck(){
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
		return flag;
	}

	/// <summary>
	/// 移動できればtrue,できなければfalse
	/// </summary>
	/// <param name="d">D.</param>
	/// <param name="len">Length.</param>
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
			currentDir = d;
		}
		
		return result;
	}
	
	public bool moveCheck(){
		bool flag = true;
		Debug.DrawLine (startpos.position, endpos.position);
		if (Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Wall"))) {
			flag = false;
		}else if(Physics2D.Linecast (startpos.position, endpos.position, 1 << LayerMask.NameToLayer ("Enemy"))){
			flag = false;
		}
		return flag;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

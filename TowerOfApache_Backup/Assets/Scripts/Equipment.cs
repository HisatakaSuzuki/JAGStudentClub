using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Equipment/装備
/// </summary>
public class Equipment : MonoBehaviour {
	public string[,] csvData;
	public int attackPoint { get; private set; }	//　現在装備している武器の攻撃力(計算用変数)
	public int defendPoint { get; private set; }	//　現在装備している防具の防御力
	public float DR { get; private set; }			//　現在装備している防具の計算用値(計算用変数)
	public int weaponId { get; private set; }		// 現在装備している武器のID
	public int armorId { get; private set; }		// 現在装備している防具のID

	// Use this for initialization
	void Start () 
	{
		//　初期化　武器防具ともに装備なし
		weaponId = 0;
		armorId = 0;

		attackPoint = 0;
		defendPoint = 0;

		//CSV read
		csvData = FileManager.readTextFile("equipments");
	}

	/// <summary>
	/// 武器を装備するメソッド、成功時にはTRUEを返す。
	/// 防具は装備できない。失敗時はFALSEを返す。
	/// </summary>
	public bool setWeapon(int num) {
		bool result = false;
		//もうセットされていたらfalseを返す
		if(weaponId != 0){
			return false;
		}
		if(0 <= num && num < csvData.GetLength(0)) { 	//数値が装備なのか判定
			if(int.Parse(csvData[num,2]) == 0) {				//武器なのか判定
				weaponId = num;
				attackPoint = int.Parse(csvData[weaponId,1]);
				result = true;
			}
		}
		return result;
	}

	///　<summary>
	/// 防具を装備するメソッド。成功時にはTRUEを返す。
	/// 武器は装備できない。失敗時はFALSEを返す。
	/// </summary>
	public bool setArmor(int num) {
		bool result = false;
		//もうセットされていたらfalseを返す
		if(armorId != 0){
			return false;
		}
		if(0 <= num && num < csvData.GetLength(0)) { 	//数値が装備なのか判定
			if(int.Parse(csvData[num,1]) == 0) {				//防具なのか判定
				armorId = num;
				defendPoint = int.Parse(csvData[armorId,2]);
				DR = 1.0f - (float.Parse(csvData[armorId,2]) / 100.0f);
				result = true;
			}
		}
		return result;
	}

	/// <summary>
	/// 武器を外すメソッド・
	/// 何も装備していないときに呼ばれるとfalseを返す
	/// </summary>
	/// <returns><c>true</c>, if weapon was taken, <c>false</c> otherwise.</returns>
	public bool takeWeapon(){
		//まだセットされていなかったらfalseを返す
		if(weaponId == 0){
			Debug.Log("武器がセットされていません");
			return false;
		}
		weaponId = 0;
		attackPoint = 0;
		return true;
	}

	/// <summary>
	/// 防具を外すメソッド
	/// 何も装備されてないときに呼ばれるとfalseを返す
	/// </summary>
	/// <returns><c>true</c>, if armor was taken, <c>false</c> otherwise.</returns>
	public bool takeArmor(){
		//まだセットされていなかったらfalseを返す
		if(armorId == 0){
			Debug.Log("防具がセットされていません");
			return false;
		}
		armorId = 0;
		defendPoint = 0;
		DR = 0.0f;
		return true;
	}

	///　<summary>
	/// 装備の名前を取得する。成功時には武器名を返す。失敗時にはnullを返す。
	/// </summary>
	public string getEquipmentsName(int num) {
		string equipmentName = null;
		if(0 <= num && num < csvData.GetLength(0)) { 	//数値が装備なのか判定
			equipmentName = csvData[num,0];	
		}
		return equipmentName;
	}

	///　<summary>
	/// 装備の攻撃力を取得する。成功時には攻撃力を返す。失敗時には-1を返す。
	/// </summary>
	public int getEquipmentsAP(int num) {
		int equipmentAP = -1;
		if(0 <= num && num < csvData.GetLength(0)) { 	//数値が装備なのか判定
			equipmentAP = int.Parse(csvData[num,1]);	
		}
		return equipmentAP;
	}

	///　<summary>
	/// 装備の防御力を取得する。成功時には防御力を返す。失敗時には-1を返す。
	/// </summary>
	public int getEquipmentsDP(int num) {
		int equipmentDP = -1;
		if(0 <= num && num < csvData.GetLength(0)) { 	//数値が装備なのか判定
			equipmentDP = int.Parse(csvData[num,2]);	
		}
		return equipmentDP;
	}
}
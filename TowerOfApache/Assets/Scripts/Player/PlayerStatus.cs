using UnityEngine;
using System.Collections;

//プレイヤーのステータス
public class PlayerStatus{
	//ステータスデータの定義
	public int level{ get; set; }
	public int hp{ get; set; }
	public int ap{ get; set; }
	public float dp{ get; set; }
	public int hungry{ get; set; }
	public int state{ get; set; }
	public int exp{ get; set; }
	//----ステータスデータの定義終わり
	//用意されたデータを格納
	public int[,] statusData; 

	/// <summary>
	/// ステータスの初期化
	/// </summary>
	public void initStatus(){
		level = 0;
		hp = statusData [level, 1];
		ap = statusData [level, 0];
		dp = 0.0f;
		hungry = 100;
		state = 0;
		exp = statusData[level,2];
	}

	/// <summary>
	/// ステータス情報の格納
	/// </summary>
	public void initStatusData(){
		statusData = FileLoader.readTextAsInt ("Player/exptable");
	}

	/// <summary>
	/// レベルアップによるステータスの更新
	/// </summary>
	public void levelup(){
		if (level < 70) {
			float percentage = (float)(hp / statusData[level,1]);
			level++;
			hp = statusData[level,1];
			hp = (int)(hp * percentage);
			ap = statusData[level,0];
			hungry = 100;
		}
	}
}

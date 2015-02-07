using UnityEngine;
using System.Collections;

/// <summary>
/// Dungeon creator.
/// ダンジョンの構築/破棄と,その他オブジェクトの設置と破棄
/// </summary>
public class DungeonCreator : MonoBehaviour {
	//ダンジョンの元
	public GameObject obj;
	public Sprite[] floor;	//床素材
	public Sprite[] wall;	//壁素材
	SpriteRenderer s_render;	//スプライトを変更するため
	public GameObject player,stairway,trap,item,enemy;	//ダンジョンに設置するもの
	int maxroom;	//現在の階層の最大部屋数
	
	public int currentFloor;	//現在の階層
	public int[] appearmonster;	//出現するエネミーの情報
	string[,] monster_floor;	//モンスターと階層の関係



	//最初の初期化
	void Awake(){
		s_render = obj.GetComponent<SpriteRenderer>();
		maxroom = 1;	//部屋ができるたびに加算
		currentFloor = 0;	//初期は0階
	}
	
	void Start(){
		//各階に出現するエネミーの関係をcsvから取得
		monster_floor = FileManager.readTextFile ("monster_floor");
	}

	//ダンジョンを生成するようの関数
	public void createDungeon (string[,] dungeon) {
		//生成するたびに階層はあがる
		currentFloor++;

		for(int i=0;i<dungeon.GetLength(0);i++){
			for(int j=0;j<dungeon.GetLength(1);j++){
				GameObject o;

				//"*"は通路,"0"は壁,その他の数字は部屋
				switch(dungeon[i,j]){
				case "*":
					//通路
					s_render.sprite = floor[Random.Range(0,floor.Length)];	//床のスプライトを充てる
					o = Instantiate(obj,new Vector3(j,-i,0), Quaternion.identity) as GameObject;	//ゲーム空間に配置
					o.layer = LayerMask.NameToLayer("Floor");	//レイヤー名
					o.tag = "Floor";
					break;
				case "0":
					//壁
					s_render.sprite = wall[Random.Range(0,wall.Length)];	//壁のスプライトを充てる
					o = Instantiate(obj,new Vector3(j,-i,0), Quaternion.identity) as GameObject;	//ゲーム空間に配置
					o.layer = LayerMask.NameToLayer("Wall");	//レイヤー名
					o.tag = "Wall";
					break;
				default:
					//部屋：数字は部屋番号になる
					s_render.sprite = floor[Random.Range(0,floor.Length)];	//床のスプライトを充てる
					o = Instantiate(obj,new Vector3(j,-i,0), Quaternion.identity) as GameObject;	//ゲーム空間に配置
					o.layer = LayerMask.NameToLayer("Floor");	//レイヤー名
					o.tag = "Room" + dungeon[i,j];
					//数値が大きければmaxroomに代入
					if(maxroom < int.Parse(dungeon[i,j])){
						maxroom = int.Parse(dungeon[i,j]);
					}
					break;
				}
				o.transform.parent = this.transform;	//ゲームマネージャの子要素にする
			}
		}
	}

	//ダンジョン/その他のオブジェクトを破棄する
	public void destroyDungeon(){
		//階段,アイテム,トラップ,敵の残りを削除
		GameObject s = GameObject.FindGameObjectWithTag ("Stairway");
		//GameObject[] i = GameObject.FindGameObjectsWithTag ("Item");
		//GameObject[] t = GameObject.FindGameObjectsWithTag ("Trap");
		GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");

		Destroy (s);	//階段の消去
		/*foreach (GameObject obj in i) {
			Destroy(obj);		
		}
		foreach (GameObject obj in t) {
			Destroy(obj);		
		}
		*/
		//エネミーの消去
		foreach (GameObject obj in e) {
			Destroy(obj);		
		}
		//ダンジョンの破棄
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}


	//プレイヤー,エネミー,階段,アイテム,トラップを配置
	public void putObjects(){
		int p, s, e;
		Debug.Log (maxroom);
		//pとsが同じ部屋になってしまったら行けないので,別になるまでループ
		do{
			p = Random.Range (1, maxroom);  //プレイヤーを配置する部屋番号
			s = Random.Range (1, maxroom);	//階段を配置する部屋番号
		}while(p == s);

		//出現する敵をチェックする
		appearmonster = new int[int.Parse(monster_floor[currentFloor,1])];	//初期化
		int k = 0;	//雑用変数
		for (int j = 2; j < monster_floor.GetLength(1); j++) {
			if(monster_floor[currentFloor,j] == "1"){
				//
				appearmonster[k] = int.Parse(monster_floor[currentFloor,j]);
				k++;
			}
			if(k >= appearmonster.Length){
				break;
			}
		}

		for(int j = 0; j < appearmonster.Length; j++){
			e = Random.Range(1,maxroom);	//エネミーを配置する部屋の番号
			GameObject[] enemyPos = GameObject.FindGameObjectsWithTag ("Room"+e);		//エネミー用の配置位置候補
			Debug.Log("j="+j+", room="+e+", "+enemyPos.Length);
			Vector2 ENEMY = enemyPos[Random.Range(0,enemyPos.Length-1)].transform.position;	//エネミーの位置候補から一つ選ぶ
			GameObject obj = Instantiate(enemy,ENEMY,Quaternion.identity) as GameObject;	//エネミーの配置
			//エネミーのテクスチャの準備
			//エネミーのアニメーションの準備
		}

		GameObject[] playerPos = GameObject.FindGameObjectsWithTag ("Room"+p);		//プレイヤー用の配置位置候補
		GameObject[] stairwayPos = GameObject.FindGameObjectsWithTag ("Room"+s);	//階段用の配置位置候補

		Vector2 PLAYER = playerPos [Random.Range (0, playerPos.Length-1)].transform.position;		//プレイヤーの位置候補から一つ選ぶ
		Vector2 STAIRWAY = stairwayPos [Random.Range (0, stairwayPos.Length-1)].transform.position;	//階段にも同じく

		player.transform.position = PLAYER;		//プレイヤーの配置
		GameObject o = Instantiate (stairway,STAIRWAY,Quaternion.identity) as GameObject; 	//階段の配置
	}
}

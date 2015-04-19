using UnityEngine;
using System.Collections;

//ダンジョンの構築と破棄
public class Dungeon : MonoBehaviour {
	public GameObject obj;
	public Sprite[] floor;	//床素材
	public Sprite[] wall;	//壁素材
	SpriteRenderer srender;	//スプライトをあてる用

	void Awake(){
		obj = Resources.Load("Dungeon/dungeonBase") as GameObject;
		srender = obj.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	/// <summary>
	/// ダンジョンを生成する
	/// </summary>
	/// <param name="dungeon">Dungeon.</param>
	/// <param name="max">Max.</param>
	public void creatDungeon(string[,] dungeon, ref int max){
		for(int i=0;i<dungeon.GetLength(0);i++){
			for(int j=0;j<dungeon.GetLength(1);j++){
				GameObject o;
				switch(dungeon[i,j]){
				case "*": //通路なら
					srender.sprite = floor[Random.Range(0,floor.Length)];	//床のスプライトを充てる
					srender.sortingLayerName = "floor";		//描画順
					o = Instantiate(obj,new Vector3(j,-i,0), Quaternion.identity) as GameObject;	//ゲーム空間に配置
					break;
				case "0": //壁なら
					srender.sprite = wall[Random.Range(0,wall.Length)];	//壁のスプライトを充てる
					srender.sortingLayerName = "active";	//描画順
					o = Instantiate(obj,new Vector3(j,-i,0), Quaternion.identity) as GameObject;	//ゲーム空間に配置
					break;
				default: //部屋なら
					srender.sprite = floor[Random.Range(0,floor.Length)];	//床のスプライトを充てる
					srender.sortingLayerName = "floor";		//描画順
					o = Instantiate(obj,new Vector3(j,-i,0), Quaternion.identity) as GameObject;	//ゲーム空間に配置
					//数値が大きければmaxに代入
					if(max < int.Parse(dungeon[i,j])){
						max = int.Parse(dungeon[i,j]);
					}
					break;
				}
				o.transform.parent = this.transform;	//ダンジョンの子要素にする
			}
		}
	}

	/// <summary>
	/// ダンジョンを破棄する
	/// </summary>
	public void destroyDungeon(){
		//ダンジョンの破棄
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}
}

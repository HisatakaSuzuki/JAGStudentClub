using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//----------ダンジョン関連
	int floorNo = 0;			//階層
	int[] maps = new int[29];  //使用するマップを格納しておく
	public string[,] currentmap = new string[25,25];	//現在のマップの状況
	Dungeon dungeon ;	//ダンジョンクラス
	public GameObject dungeonObject;
	int maxroomNo;				//現在の階層の部屋数

	void dungeonInit(){
		dungeonObject.transform.position = Vector3.zero;
		dungeonObject.transform.parent = this.transform;

		dungeon = dungeonObject.GetComponent<Dungeon>();
		//マップを選択
		for (int i=0; i<29; i++) {
			maps[i] = Random.Range(0,50);
			Debug.Log(maps[i]);
		}
		currentmap = FileLoader.readTextFileAsString ("Dungeon/maps_small" + maps[floorNo]);
	}

	//----------ダンジョン関連終わり

	

	void Awake(){
		//----------ダンジョン関連
		dungeonInit ();
		//----------ダンジョン関連終わり
	}

	// Use this for initialization
	void Start () {
		dungeon.creatDungeon (currentmap, ref maxroomNo);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S)){
			dungeon.destroyDungeon();
			floorNo++;
			currentmap = FileLoader.readTextFileAsString ("Dungeon/maps_small" + maps[floorNo]);
			dungeon.creatDungeon(currentmap, ref maxroomNo);
		}
	}
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Game manager.
/// ダンジョンの構築と破棄を行う(処理はDungeonCreatorに記述.)
/// 以下のステートでゲームの流れを処理する
///   STATE_DUNGEONCREATE  ダンジョンを構築する
///   STATE_PLACEMENT	   プレイヤー,エネミー,アイテム,階段,トラップを配置する
///   STATE_PLAYING		   プレイヤーとエネミーが行動できるフェーズ
///   STATE_PAUSE		   マップやアイテムなどの別画面を開いたとき
///   STATE_DESTROY        ダンジョンとダンジョンに配置されているプレイヤー以外のものを破棄
/// 
/// 各ステートでの実行内容は
/// delegate関数の配列m_stateHandlersに一任されています.
/// 配列の添え字を変えるだけで別の関数を実行します.
/// 以下の関数がdelegateに登録されています.
///   onStateDungeonCraete
///   onStatePlacement
///   onStatePlaying
///   onStatePause
///   onStateDestroy
/// </summary>
public class GameManager : MonoBehaviour {
	//ダンジョン用のデータ----------------------
	byte dungeon_num;		//ダンジョン番号
	string[,] dungeon_data;	//num番のダンジョンデータ格納
	DungeonCreator dungeonCrator;	//コンポーネント
	//ダンジョン用のデータ----------------ここまで

	//ステート処理関係----------------------
	//ゲームに必要であろうステート
	enum GAMESTATE{
		STATE_DUNGEONCREATE,
		STATE_PLACEMENT,
		STATE_PLAYING,
		STATE_PAUSE,
		STATE_DESTROY,
		_NUM
	};

	//最初の状態
	GAMESTATE gamestate = GAMESTATE.STATE_DUNGEONCREATE;

	//デリゲート関数
	delegate void StateHandler();

	//関数の配列
	StateHandler[] m_stateHandlers;
	//ステート処理関係----------------ここまで

	//プレイヤー/エネミーのターン処理----------------------
	public static bool playerturn{ get; set;} //プレイヤーターンのフラグ
	public static bool enemyturn{ get; set;} //エネミーターンのフラグ
	GameObject[] enemies;	//エネミーオブジェクトの取得
	EnemyContoroller enemycontroller;	//エネミーのIDや処理順用
	//プレイヤー/エネミーのターン処理------------------ここまで



	//各種ステートの中身----------------------
	//ダンジョン構築ステート
	void onStateDungeonCraete(){
		//ダンジョンをランダムで選ぶ
		dungeon_num = (byte)(Random.Range(1,50));
		//dungeon_num = 26;
		//csvファイルを読み込む
		dungeon_data = FileManager.readTextFile ("dungeon/maps_small" + dungeon_num);
		//ダンジョンの構築
		dungeonCrator.createDungeon (dungeon_data);
		//ステートを変更する
		changeState (GAMESTATE.STATE_PLACEMENT);
	}

	//オブジェクト配置ステート
	void onStatePlacement(){
		//オブジェクトの配置
		dungeonCrator.putObjects ();
		
		//エネミーの初期化
		initEnemy ();
		
		//ステートを変更する
		changeState (GAMESTATE.STATE_PLAYING);
	}

	//プレイステート
	void onStatePlaying(){
		//テスト用です
		if (Input.GetKeyDown (KeyCode.M)) {
			changeState(GAMESTATE.STATE_DESTROY);
		}
		if (Stairway.goup) {
			changeState(GAMESTATE.STATE_DESTROY);
		}
		/*if (enemyturn) {
			GameObject e = GameObject.FindGameObjectWithTag("Enemy");
			if(e == null){
				enemyturn = false;
				playerturn = true;
			}
		}*/

	}

	//ポーズステート
	void onStatePause(){
		//ステートを変更する
		changeState (GAMESTATE.STATE_PLAYING);
	}

	//ダンジョンを破棄するステート
	void onStateDestroy(){
		//ダンジョンの破棄
		dungeonCrator.destroyDungeon ();
		//ステートを変更する
		changeState (GAMESTATE.STATE_DUNGEONCREATE);
	}
	//各種ステートの中身----------------------ここまで
	

	//一番最初の初期化
	void Awake(){
		//ステート関数の初期化
		m_stateHandlers = new StateHandler[(int)GAMESTATE._NUM]{
			new StateHandler(onStateDungeonCraete),
			new StateHandler(onStatePlacement),
			new StateHandler(onStatePlaying),
			new StateHandler(onStatePause),
			new StateHandler(onStateDestroy)
		};

		//ダンジョン生成用
		dungeonCrator = this.GetComponent<DungeonCreator>();

		//プレイヤーから始まる
		playerturn = true;
		enemyturn = false;
	}
	
	// Use this for initialization
	void Start () {

	}

	//デリゲート関数の実行
	void FixedUpdate(){
		m_stateHandlers[(int)gamestate]();
	}

	// Update is called once per frame
	void Update () {

	}

	//ステートを変更する
	void changeState(GAMESTATE gs){
		gamestate = gs;
	}

	//エネミーの初期化
	void initEnemy(){
		//配置されたエネミーオブジェクトを取得
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		for (int i=0; i < enemies.Length; i++) {
			enemycontroller = enemies[i].GetComponent<EnemyContoroller>();
			//エネミーの行動用にIDを割り振る
			enemycontroller.setID(i);
			//もし配列の最後のエネミーなら最終フラグを立てる
			if(i == enemies.Length - 1){
				enemycontroller.setLastFlag(true);
			}
		}
	}
}

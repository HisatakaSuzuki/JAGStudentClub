using UnityEngine;
using System.Collections;

public class ItemPorch : MonoBehaviour {
	ItemBase[] items;
	//配列の最後を指す
	int last;

	// Use this for initialization
	void Start () {
		last = 0;
//		item = new Item_meat ();
//		item.init ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Z)) {
//			Debug.Log(item.name + "," + item.shikibetu + "," + item.limit);
//		}
//		if(Input.GetKeyDown(KeyCode.X)){
//			item.dispose(ref item);
//		}
	}

	/// <summary>
	/// アイテムを取得したとき
	/// </summary>
	/// <param name="item">Item.</param>
	void setitem(ItemBase item){
		items [last] = item;
		last++;
	}

	/// <summary>
	/// アイテムを破棄/使い切ったとき
	/// </summary>
	void throwitem(int i){
		for (int j = i; j < last; j++) {
			items[i] = items[i+1];
		}
		last--;
	}

	/// <summary>
	/// アイテムの使用
	/// </summary>
	/// <param name="i">The index.</param>
	void itemuse(int i){
		items [i].use ();
		if (items [i].limit <= 0) {
			items[i].dispose(ref items[i]);
			throwitem(i);
		}
	}
}

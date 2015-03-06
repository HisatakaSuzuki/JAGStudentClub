using UnityEngine;
using System.Collections;

public class ItemPorch : MonoBehaviour {
	static ItemBase[] items;
	//配列の最後を指す
	static int last;

	// Use this for initialization
	void Start () {
		last = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// アイテムを取得したとき
	/// </summary>
	/// <param name="item">Item.</param>
	public static void setitem(ItemBase item){
		items [last] = item;
		last++;
	}

	/// <summary>
	/// アイテムを破棄/使い切ったとき
	/// </summary>
	public static void throwitem(int i){
		for (int j = i; j < last; j++) {
			items[i] = items[i+1];
		}
		if (last > 0) {
			last--;
		}
	}

	/// <summary>
	/// アイテムの使用
	/// </summary>
	/// <param name="i">The index.</param>
	public void itemuse(int i){
		items [i].use ();
		if (items [i].limit <= 0) {
			items[i].dispose(ref items[i]);
			throwitem(i);
		}
	}
}

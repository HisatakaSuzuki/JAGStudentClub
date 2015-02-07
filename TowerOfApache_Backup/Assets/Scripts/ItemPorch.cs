using UnityEngine;
using System.Collections;

public class ItemPorch : MonoBehaviour {
	private const int max = 30;
	Item[] items;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	//アイテムを使う
	void spend(int index){
		items [index].effect ();
		cleanUp ();
	}

	//アイテムを捨てる
	void discard(int index){
		items [index] = null;
		cleanUp ();
	}

	//ポーチの整理
	void cleanUp(){
		for(int i=0;i<max-1;i++){
			if(items[i] == null){
				items[i] = items[i+1];
				items[i+1] = null;
			}
		}
	}

	//アイテムを交換
	void change(Item item, int index){
		items [index] = item;
	}
}

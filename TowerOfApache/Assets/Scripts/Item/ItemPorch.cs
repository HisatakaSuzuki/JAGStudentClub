using UnityEngine;
using System.Collections;

public class ItemPorch : MonoBehaviour {
	ItemBase item;

	// Use this for initialization
	void Start () {
		item = new Item_meat ();
		item.init ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			Debug.Log(item.name + "," + item.shikibetu + "," + item.limit);
		}
		if(Input.GetKeyDown(KeyCode.X)){
			item = null;
		}
	}
}

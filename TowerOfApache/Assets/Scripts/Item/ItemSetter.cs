using UnityEngine;
using System.Collections;

public class ItemSetter : MonoBehaviour {
	ItemBase item;
	// Use this for initialization
	void Start () {
		int itemnum = Random.Range (0, 8);
		switch (itemnum) {
		case 0: 
			item = new Item_meat();
			break;
		case 1: 
			item = new Item_goodmeat();
			break;
		case 2: 
			item = new Item_verygoodmeat();
			break;
		case 3: 
			item = new Item_rikunshito();
			break;
		case 4: 
			item = new Item_portion();
			break;
		case 5: 
			item = new Item_goodportion();
			break;
		case 6: 
			item = new Item_verygoorportion();
			break;
		case 7: 
			item = new Item_kakonto();
			break;
		default: break;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void itemSetter(){
		ItemPorch.setitem (item);
		Destroy (this.gameObject);
	}
}

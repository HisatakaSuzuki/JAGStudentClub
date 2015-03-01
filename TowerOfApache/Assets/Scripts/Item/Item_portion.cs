using UnityEngine;
using System.Collections;

public class Item_portion : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "回復薬";
		shikibetu = 1;
		limit = 1;
	}
	
	public override void use ()
	{
		base.use ();
		Debug.Log ("HPを50回復させる");
		limit--;
	}
	
	public override void dispose (ref ItemBase itself)
	{
		base.dispose (ref itself);
		//インスタンスの破棄はnullを代入して行う
		itself = null;
	}
}

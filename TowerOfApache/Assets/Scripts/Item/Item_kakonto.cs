using UnityEngine;
using System.Collections;

public class Item_kakonto : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "葛根湯";
		shikibetu = 1;
		limit = 1;
	}
	
	public override void use ()
	{
		base.use ();
		Debug.Log ("HPの上限を10%上昇させる");
		limit--;
	}
	
	public override void dispose (ref ItemBase itself)
	{
		base.dispose (ref itself);
		//インスタンスの破棄はnullを代入して行う
		itself = null;
	}
}

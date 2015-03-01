using UnityEngine;
using System.Collections;

public class Item_goodmeat : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "いいお肉";
		shikibetu = 1;
		limit = 1;
	}
	
	public override void use ()
	{
		base.use ();
		Debug.Log ("満腹度を全回復");
		limit--;
	}
	
	public override void dispose (ref ItemBase itself)
	{
		base.dispose (ref itself);
		//インスタンスの破棄はnullを代入して行う
		itself = null;
	}
}

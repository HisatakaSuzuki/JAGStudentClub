using UnityEngine;
using System.Collections;

public class Item_verygoodmeat : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "The Very Good Meat";
		shikibetu = 1;
		limit = 1;
	}
	
	public override void use ()
	{
		base.use ();
		Debug.Log ("満腹度を全回復、満足度の上限を5%あげる");
		limit--;
	}
	
	public override void dispose (ref ItemBase itself)
	{
		base.dispose (ref itself);
		//インスタンスの破棄はnullを代入して行う
		itself = null;
	}
}

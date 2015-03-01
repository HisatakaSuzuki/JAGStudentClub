using UnityEngine;
using System.Collections;

public class Item_rikunshito : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "六君子湯";
		shikibetu = 1;
		limit = 1;
	}
	
	public override void use ()
	{
		base.use ();
		Debug.Log ("満腹度の上限を10%上昇させる");
		limit--;
	}
	
	public override void dispose (ref ItemBase itself)
	{
		base.dispose (ref itself);
		//インスタンスの破棄はnullを代入して行う
		itself = null;
	}
}

using UnityEngine;
using System.Collections;

public class Item_meat : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "お肉";
		shikibetu = 1;
		limit = 1;
	}

	public override void use ()
	{
		base.use ();
		Debug.Log ("プレイヤーの満腹度を50%回復");
		limit--;

	}

	public override void dispose ()
	{
		base.dispose ();
		//インスタンスの破棄はnullを代入して行う
	}
}

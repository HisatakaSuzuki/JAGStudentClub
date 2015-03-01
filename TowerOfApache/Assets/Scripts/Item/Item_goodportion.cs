﻿using UnityEngine;
using System.Collections;

public class Item_goodportion : ItemBase {
	public override void init ()
	{
		base.init ();
		name = "いい回復薬";
		shikibetu = 1;
		limit = 1;
	}
	
	public override void use ()
	{
		base.use ();
		Debug.Log ("HPを100回復させる");
		limit--;
	}
	
	public override void dispose (ref ItemBase itself)
	{
		base.dispose (ref itself);
		//インスタンスの破棄はnullを代入して行う
		itself = null;
	}
}

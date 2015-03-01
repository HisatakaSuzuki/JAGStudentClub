using UnityEngine;
using System.Collections;

public class ItemBase{
	public string name;     //アイテム名
	public int shikibetu;	//識別状態
	public int limit;		//効果のターン数カウント

	//初期化
	public virtual void init(){}
	public virtual void use(){}
	public virtual void dispose(ref ItemBase itself){}
}

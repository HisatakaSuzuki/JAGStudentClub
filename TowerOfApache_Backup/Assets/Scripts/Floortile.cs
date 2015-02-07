using UnityEngine;
using System.Collections;

public class Floortile : MonoBehaviour {
	//Gameobject準備
	GameObject player;

	// Use this for initialization
	void Start () {
		//GameobjectにプレイヤーをFindtagする
		GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void Update () {
		//プレイヤーの位置と自分のいちが一緒なら
		//プレイヤーのルーム番号を変更する
		if (player.transform.position == this.transform.position) {
			player.GetComponent<PlayerController>().currentRoom = int.Parse(this.tag.Substring(4));
			}

	}
}
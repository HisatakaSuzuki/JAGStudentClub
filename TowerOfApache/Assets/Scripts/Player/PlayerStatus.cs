using UnityEngine;
using System.Collections;

public class PlayerStatus{
	public int level{ get; set; }
	public int hp{ get; set; }
	public int ap{ get; set; }
	public float dp{ get; set; }
	public int hungry{ get; set; }
	public int state{ get; set; }
	public int exp{ get; set; }
	
	public void initStatus(int hp, int ap, float dp, int hungry, int state,int exp){
		this.hp = hp;
		this.ap = ap;
		this.dp = dp;
		this.hungry = hungry;
		this.state = state;
		this.exp = exp;
	}
}

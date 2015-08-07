using UnityEngine;
using System.Collections;

public enum HeroStyle{
	Swordman,
	Magician

}
public class PlayerStatus : MonoBehaviour {

	public HeroStyle heroType;
	public string name="默认名称";
	public int HP=100;
	public int MP=100;
	public float hp_remain=100.0f;
	public float mp_remain = 100.0f;
	public int coins=200;
	public int level=1;//100+level*30
	public float exp=0.0f;

	public float speed=20;
	public int speedPlus = 0;
	public float def=20;
	public int defPlus=0;
	public float  attack=20;
	public int attackPlus=0;
	public int pointRemain=0;

	void Start(){
		GetExp (0);//初始化为0经验值
	}



	public void GetDrug(int hp,int mp){
		hp_remain += hp;
		mp_remain += mp;
		if (hp_remain > this.HP) {
			hp_remain=this.HP;		
		}
		if (mp_remain > this.MP) {
			mp_remain=this.MP;		
		}

		FaceTextureUI._instance.Updateshow ();
	}

	public bool GetPoint(int point=1){
		if (pointRemain >= point) {
						pointRemain -= point;
						return true;
				} else {
			return false;		
		}
	}

	public void GetExp(int exp){
		this.exp += exp;
		int totalexp = 100 + level * 30;
		while (this.exp>=totalexp) {
			this.level++;
			//经验归零
			this.exp-=totalexp;
			totalexp=100+level*30;//一级的经验值重置
		}
		//进度条显示
		ExpBar._instance.Setvalue (this.exp / totalexp);
	}
	public bool TakeMp(int count ){
		if (mp_remain >= count) {
						mp_remain -= count;
			FaceTextureUI._instance.Updateshow();
						return true;
				} else {
			return false;
		}
	}
}

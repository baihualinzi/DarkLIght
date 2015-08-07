using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsInfo : MonoBehaviour
{
		public static SkillsInfo _instance;
		public TextAsset SkillListInfo;
		private Dictionary<int,SkillInfo> skillInfoDict = new Dictionary<int ,SkillInfo> ();

		// Use this for initialization
		void Awake ()
		{
				_instance = this;
				InitSkillInfo ();//初始化技能信息字典
		}
		//我们可以通过在这个方法，根据id查找到一个技能信息
		public SkillInfo GetSkillInfoById (int id)
		{
				SkillInfo info = null;
		skillInfoDict.TryGetValue (id, out info);
				return info;
		}
	
		void InitSkillInfo ()
		{
				string text = SkillListInfo.text;
				string[] skillinfoArray = text.Split ('\n');
				foreach (string skillinfoStr in skillinfoArray) {
						string[] pa = skillinfoStr.Split (',');
						SkillInfo info = new SkillInfo ();
						info.id = int.Parse (pa [0]);
						info.name = pa [1];
						info.icon_name = pa [2];
						info.Des = pa [3];
						string str_applytype = pa [4];
						switch (str_applytype) {
						case "Passive":
								info.applytype = ApplyType.Passive;
								break;
						case "Buff":
								info.applytype = ApplyType.Buff;
								break;
						case "SingleTarget":
								info.applytype = ApplyType.SingleTarget;
								break;
						case "MultiTarget":
								info.applytype = ApplyType.MultiTarget;
								break;
						}
						string str_applypro = pa [5];
						switch (str_applypro) {
						case "Attack":
								info.applyPro = ApplyProperty.Attack;
								break;
						case "Def":
								info.applyPro = ApplyProperty.Def;
								break;
						case "Speed":
								info.applyPro = ApplyProperty.Speed;
								break;
						case "AttackSpeed":
								info.applyPro = ApplyProperty.AttackSpeed;
								break;
						case "HP":
								info.applyPro = ApplyProperty.HP;
								break;
						case "MP":
								info.applyPro = ApplyProperty.Mp;
								break;
						}
						info.applyValue = int.Parse (pa [6]);
						info.applyTime = int.Parse (pa [7]);
						info.mp = int.Parse (pa [8]);
						info.coldTime = int.Parse (pa [9]);
						switch (pa [10]) {
						case "Swordman":
								info.applyRole = ApplyRole.Swordman;
								break;
						case "Magician":
								info.applyRole = ApplyRole.Magician;
								break;
						}
						info.level = int.Parse (pa [11]);
						switch (pa [12]) {
						case "Self":
								info.releaseType = ReleaseType.Self;
								break;
						case "Enemy":
								info.releaseType = ReleaseType.Enemy;
								break;
						case "Position":
								info.releaseType = ReleaseType.Position;
								break;
						}
						info.distance = float.Parse (pa [13]);
						info.efx_name = pa [14];
						info.aniname = pa [15];
						info.anitime = float.Parse (pa [16]);
						skillInfoDict.Add (info.id, info);
				}
		}
}

public enum ApplyRole
{
		Swordman,
		Magician
}
//作用类型
public enum ApplyType
{
		Passive,
		Buff,
		SingleTarget,
		MultiTarget
}

public enum ApplyProperty
{
		Attack,
		Def,
		Speed,
		AttackSpeed,
		HP,
		Mp
}

public enum ReleaseType
{
		Self,
		Enemy,
		Position
}

public class SkillInfo
{
		public int id;
		public string name;
		public string icon_name;
		public string Des;
		public ApplyType applytype;
		public ApplyProperty applyPro;
		public int applyValue;
		public int applyTime;
		public int mp;
		public int coldTime;
		public ApplyRole applyRole;
		public int level;
		public ReleaseType releaseType;
		public float distance;
		public string efx_name;//特效名称
		public string aniname;//动画
		public float anitime = 0;//动画时间
	
}

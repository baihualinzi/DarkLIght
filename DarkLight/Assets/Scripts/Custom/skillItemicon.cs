using UnityEngine;
using System.Collections;

public class skillItemicon : UIDragDropItem{

	private int skillId;
	protected override void  OnDragDropStart(){//在克隆的icon上调用
				base.OnDragDropStart ();

		skillId = transform.parent.GetComponent<skillItem> ().id;
				transform.parent = transform.root;
				this.GetComponent<UISprite> ().depth = 40;
		}
	protected override void OnDragDropRelease(GameObject surface){//重写OnDragDropRelease方法
				base.OnDragDropRelease (surface);
		if (surface != null && surface.tag == Tags.shortcut) {//技能拖到快捷方式
			surface.GetComponent<Shortcutkey>().setskill(skillId);
		}


		}
}
using UnityEngine;
using System.Collections;

public class CharacterCreation : MonoBehaviour {

	public GameObject[] characterObjects;
	public UIInput Inputname;
	private GameObject[] characterxu;
	private int selectIndex=0;
	private int length;
	// Use this for initialization
	void Start () {
		length = characterObjects.Length;
		characterxu = new GameObject[length];
		for (int i=0; i<length; i++) {
			characterxu[i]=GameObject.Instantiate(characterObjects[i],transform.position,transform.rotation)  as GameObject;
		}
		ShowCharacter ();
	}
	
	void ShowCharacter(){
		characterxu [selectIndex].SetActive (true);
		for (int i=0; i<length; i++) {
			if(i!=selectIndex)
				characterxu[i].SetActive(false);

		}

	}
	public void OnNext(){
		selectIndex++;
		selectIndex %= length;//为0时，selectIndex=1；为1时，selectIndex为0
		ShowCharacter ();

	}
	public void OnPrev(){
		selectIndex--;
		if (selectIndex == -1)
						selectIndex = length-1;
		ShowCharacter ();
	}
	public void OnOk(){
		//存储名字和角色
		PlayerPrefs.SetInt ("SelectCharacterIndex", selectIndex);
		PlayerPrefs.SetString ("name", Inputname.value);
		//加载下一个场景
		Application.LoadLevel(2);
	}

}

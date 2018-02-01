using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoticeScript : MonoBehaviour {

	public void Show() {
		this.gameObject.SetActive (true);
	}

	public void Hide() {
		this.gameObject.SetActive (false);
	}

	public bool IsVisible() {
		return this.gameObject.activeSelf;
	}
}

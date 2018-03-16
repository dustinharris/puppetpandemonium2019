using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour {

	[SerializeField]
	private GameObject Alert;
	[SerializeField]
	private GameObject Correct;

	private UINoticeScript AlertScript;
	private UINoticeScript CorrectScript;

	void Awake () {
		this.AlertScript = Alert.GetComponent<UINoticeScript> ();
		this.CorrectScript = Correct.GetComponent<UINoticeScript> ();
	}

	public void Show(bool alert) {
		if (alert && AlertScript != null) {
			AlertScript.Show ();
			CorrectScript.Hide ();
		} else if (CorrectScript != null) {
			CorrectScript.Show ();
			AlertScript.Hide ();
		}
	}

	public void Hide(bool alert) {
		if (alert && AlertScript != null) {
			AlertScript.Hide ();
		} else if (CorrectScript != null) {
			CorrectScript.Hide ();
		}
	}

	public bool IsVisible(bool alert) {
		if (alert && AlertScript != null) {
			return AlertScript.IsVisible ();
		} else if (CorrectScript != null) {
			return CorrectScript.IsVisible ();
		} else {
			return false;
		}
	}
}
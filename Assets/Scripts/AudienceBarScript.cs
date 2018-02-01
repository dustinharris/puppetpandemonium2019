using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceBarScript : MonoBehaviour {

	/*
	 * Convinence script for dealing with all audience sections at once
	*/

	[SerializeField]
	private GameObject[] AudienceSections;
	private AudienceUIScript[] Scripts;

	void Start () {
		// Array should be length 5
		Scripts = new AudienceUIScript[AudienceSections.Length];

		for (int i = 0; i < AudienceSections.Length; i++) {
			Scripts [i] = AudienceSections [i].GetComponent<AudienceUIScript> ();
		}
	}

	public void Show(int section, AudienceUIScript.Notice which, bool red, int howLong = -1) {
		Scripts [section].Show (which, red, howLong);
	}

	public void Show(int section, AudienceUIScript.Notice which, bool red, System.Func<bool> until) {
		Scripts [section].Show (which, red, until);
	}

	public void ShowAll(AudienceUIScript.Notice which) {
		for (int i = 0; i < Scripts.Length; i++) {
			Scripts [i].Show (which, true);
			Scripts [i].Show (which, false);
		}
	}

	public void ShowAll(AudienceUIScript.Notice which, bool red) {
		for (int i = 0; i < Scripts.Length; i++) {
			Scripts [i].Show (which, red);
		}
	}

	public void Hide(int section, bool red) {
		Scripts [section].Hide (red);
	}

	public void HideGlow(int section, bool red) {
		Scripts [section].HideGlow (red);
	}

	public void ResetAll() {
		for (int i = 0; i < Scripts.Length; i++) {
			Scripts [i].Hide (true);
			Scripts [i].Hide (false);
			Scripts [i].HideGlow (true);
			Scripts [i].HideGlow (false);
		}
	}

	public int Size() {
		return Scripts.Length;
	}

	public bool IsVisible(int section, AudienceUIScript.Notice which, bool red) {
		return Scripts[section].IsVisible(which, red);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceUIScript : MonoBehaviour {

	[SerializeField]
	private GameObject AlertRed;
	[SerializeField]
	private GameObject CorrectRed;
	[SerializeField]
	private GameObject IncorrectRed;
	[SerializeField]
	private GameObject AlertBlue;
	[SerializeField]
	private GameObject CorrectBlue;
	[SerializeField]
	private GameObject IncorrectBlue;
	[SerializeField]
	private GameObject GlowRed;
	[SerializeField]
	private GameObject GlowBlue;

	private UINoticeScript AlertRedScript;
	private UINoticeScript CorrectRedScript;
	private UINoticeScript IncorrectRedScript;
	private UINoticeScript AlertBlueScript;
	private UINoticeScript CorrectBlueScript;
	private UINoticeScript IncorrectBlueScript;
	private UINoticeScript GlowRedScript;
	private UINoticeScript GlowBlueScript;

	public enum Notice {
		Alert, Correct, Incorrect, Glow
	}

	void Awake () {
		AlertRedScript = AlertRed.GetComponent<UINoticeScript> ();
		CorrectRedScript = CorrectRed.GetComponent<UINoticeScript> ();
		IncorrectRedScript = IncorrectRed.GetComponent<UINoticeScript> ();
		AlertBlueScript = AlertBlue.GetComponent<UINoticeScript> ();
		CorrectBlueScript = CorrectBlue.GetComponent<UINoticeScript> ();
		IncorrectBlueScript = IncorrectBlue.GetComponent<UINoticeScript> ();
		GlowBlueScript = GlowBlue.GetComponent<UINoticeScript> ();
		GlowRedScript = GlowRed.GetComponent<UINoticeScript> ();
	}

	private UINoticeScript GetScript(Notice notice, bool red) {
		switch (notice) {
		case Notice.Alert:
			return red ? AlertRedScript : AlertBlueScript;
		case Notice.Correct:
			return red ? CorrectRedScript : CorrectBlueScript;
		case Notice.Incorrect:
			return red ? IncorrectRedScript : IncorrectBlueScript;
		case Notice.Glow:
			return red ? GlowRedScript : GlowBlueScript;
		default:
			return null;
		}
	}

	private void HideOthers(Notice notice, bool red) {
		UINoticeScript script;
		if (notice != Notice.Alert) {
			script = this.GetScript (Notice.Alert, red);
			if (script != null) {
				script.Hide ();
			}
		}
		if (notice != Notice.Correct) {
			script = this.GetScript (Notice.Correct, red);
			if (script != null) {
				script.Hide ();
			}
		}
		if (notice != Notice.Incorrect) {
			script = this.GetScript (Notice.Incorrect, red);
			if (script != null) {
				script.Hide ();
			}
		}
	}

	// Shows notice and hides all other notices
	// If positive howLong is given hides after that many seconds
	public void Show(Notice which, bool red, int howLong = -1) {
		UINoticeScript script = this.GetScript (which, red);
		if (script != null) {
			if (which != Notice.Glow) {
				this.HideOthers (which, red);
			}

			script.Show();

			if (howLong > 0) {
				StartCoroutine(Hide(script, howLong));
			}
		}
	}

	// Shows notice until function return false
	public void Show(Notice which, bool red, System.Func<bool> until) {
		UINoticeScript script = this.GetScript (which, red);
		if (script != null) {
			if (which != Notice.Glow) {
				this.HideOthers (which, red);
			}

			script.Show ();

			if (until != null) {
				StartCoroutine (Hide (script, until));
			}
		}
	}

	// Hides all notices except glow
	public void Hide(bool red) {
		this.HideOthers (Notice.Glow, red);
	}

	public void HideGlow(bool red) {
		UINoticeScript glow = this.GetScript (Notice.Glow, red);
		if (glow != null) {
			glow.Hide ();
		}
	}

	public bool IsVisible(Notice which, bool red) {
		UINoticeScript script = this.GetScript (which, red);
		if (script != null) {
			return script.IsVisible();
		} else {
			return false;
		}
	}

	private IEnumerator Hide(UINoticeScript which, int time) {
		yield return new WaitForSeconds (time);
		which.Hide ();
	}

	private IEnumerator Hide(UINoticeScript which, System.Func<bool> until) {
		yield return new WaitUntil (until);
		which.Hide ();
	}
}
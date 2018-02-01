using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogController : MonoBehaviour {

	public enum Puppet {
		Red, Blue
	}
		
	[SerializeField]
	private GameObject DialogObject;
	[SerializeField]
	private GameObject RedPuppet;
	[SerializeField]
	private GameObject BluePuppet;
	[SerializeField]
	private string FilePath;

	private Puppet puppet = Puppet.Red;

	private const string RED = "[Red]";
	private const string BLUE = "[Blue]";

	private const string RED_BUTTON = "RedPuppet";
	private const string BLUE_BUTTON = "BluePuppet";

	void Start () {

		if (DialogObject == null || RedPuppet == null || BluePuppet == null || FilePath == null) {
			throw new UnityException ("Missing parameter(s)");
		}

		//StartCoroutine (ReadFile());
	}

	public IEnumerator ReadFile() {
		Debug.Log ("Entering read file coroutine");
		using (StreamReader reader = new StreamReader (FilePath)) {
			Debug.Log ("Using stream reader");
			Text text = DialogObject.GetComponent<Text> ();
			if (text == null) {
				throw new UnityException ("Missing Text component");
			}

			string line = "";
			string button = "";
			while ((line = reader.ReadLine ()) != null) {
				if (line.StartsWith (RED)) {
					puppet = Puppet.Red;
					line = line.TrimStart (RED.ToCharArray());
					RedPuppet.SetActive (true);
					BluePuppet.SetActive (false);
					button = RED_BUTTON;
				} else if (line.StartsWith (BLUE)) {
					puppet = Puppet.Blue;
					line = line.TrimStart (BLUE.ToCharArray());
					BluePuppet.SetActive (true);
					RedPuppet.SetActive (false);
					button = BLUE_BUTTON;
				} else {
					// Do something.
				}

				text.text = line;

				yield return new WaitForSeconds (1);
				yield return WaitForInput (button);
			}
		}
	}

	private IEnumerator WaitForInput(string button) {
		while (!Input.GetButtonDown (button)) {
			yield return null;
		}
	}
}
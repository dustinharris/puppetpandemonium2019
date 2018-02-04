using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancementController : MonoBehaviour {

	[SerializeField]
	private GameObject StartCanvas;
	[SerializeField]
	private GameObject DialogCanvas;
	[SerializeField]
	private GameObject AudienceBar;

	private StartScreenController StartController;
	private DialogController DialogController;
	private AudienceBarScript AudienceScript;

	// Use this for initialization
	void Start () {
		StartController = GetComponent<StartScreenController> ();
		DialogController = GetComponent<DialogController> ();
		AudienceScript = AudienceBar.GetComponent<AudienceBarScript> ();

		StartCoroutine (WaitForReady ());
	}

	private IEnumerator WaitForReady() {
		while (!StartController.IsReady ()) {
			yield return null;
		}

		AudienceScript.ResetAll ();

		// Hide Start Canvas
		// Show Dialog Canvas
		StartCanvas.SetActive(false);
		DialogCanvas.SetActive (true);

		StartCoroutine (DialogController.ReadFile ());
	}

	// Update is called once per frame
	void Update () {
		
	}
}

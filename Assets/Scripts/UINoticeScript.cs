using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoticeScript : MonoBehaviour {

    Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Show() {
        img.enabled = true;
	}

	public void Hide() {
        img.enabled = false;
	}

	public bool IsVisible() {
        return img.isActiveAndEnabled;
	}
}

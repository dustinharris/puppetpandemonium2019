using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderEnabler : MonoBehaviour {

    [SerializeField] Renderer[] children;

	public void SetEnabled(bool active)
    {
        foreach (Renderer child in children)
        {
            child.enabled = active;
        }
    } 
}

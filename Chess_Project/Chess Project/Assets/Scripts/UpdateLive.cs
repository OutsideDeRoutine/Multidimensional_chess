using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLive : MonoBehaviour {
	
	public void UpdateMe()
    {
        this.GetComponent<CharData>().tile.GetComponentInChildren<UnityEngine.UI.Text>().text = GetComponent<CharData>().vida.ToString();
    }
}

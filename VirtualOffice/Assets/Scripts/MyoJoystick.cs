using UnityEngine;
using System.Collections;

public class MyoJoystick : MonoBehaviour {

	private Vector3 origin;

	public Vector3 offset = new Vector3(0,0,0);

	private bool hasPinned = false;

	// Use this for initialization
	void Start () {
		origin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasPinned) offset = this.transform.position - origin;
	}

	public void PinOrigin () {
		hasPinned = true;
		origin = this.transform.position;
	}

}

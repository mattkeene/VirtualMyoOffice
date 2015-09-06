using UnityEngine;
using System.Collections;

public class MyoMovement : MonoBehaviour {

	public GameObject box;
	public GameObject forwardDirection;
	public GameObject joint;

	public float speedScale = 1.0f;
	public float degreesPerFrame = 1.0f;
	private Vector3 velocity = new Vector3(0,0,0);

	private MyoJoystick joystickScript;

	// Use this for initialization
	void Start () {
		joystickScript = box.GetComponent<MyoJoystick> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (joystickScript.HasPinned()) {
			velocity = joystickScript.offset;

			Vector3 positionChange = new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime * speedScale;
			transform.position += positionChange;
			joystickScript.UpdateOrigin(positionChange);

			if (joystickScript.RotateLeft()) {
				transform.Rotate(0.0f, -1.0f * degreesPerFrame, 0.0f);
				joint.transform.Rotate(0.0f, -1.0f * degreesPerFrame, 0.0f);
			} else if (joystickScript.RotateRight()) {
				transform.Rotate(0.0f, 1.0f * degreesPerFrame, 0.0f);
				joint.transform.Rotate(0.0f, 1.0f * degreesPerFrame, 0.0f);
			}

		}
	}
	
}

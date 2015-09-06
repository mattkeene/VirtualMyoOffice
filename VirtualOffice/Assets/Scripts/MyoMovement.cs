using UnityEngine;
using System.Collections;

public class MyoMovement : MonoBehaviour {

	public GameObject myo = null;
	public GameObject box = null;
	public GameObject forwardDirection = null;
	public GameObject joint = null;

	public float threshold = 0.25f;
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
			Vector3 positionChange = new Vector3(0,0,0);

			velocity = joystickScript.offset;

			if (velocity.magnitude > threshold) {
				positionChange = new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime * speedScale;
				transform.position += positionChange;
			}

			if (joystickScript.RotateLeft()) {
				transform.Rotate(0.0f, -1.0f * degreesPerFrame, 0.0f);

				joint.GetComponent<CustomJointOrientation>()._antiYaw = Quaternion.FromToRotation (
						new Vector3 (myo.transform.forward.x, 0, myo.transform.forward.z),
					forwardDirection.transform.forward);

			} else if (joystickScript.RotateRight()) {
				transform.Rotate(0.0f, 1.0f * degreesPerFrame, 0.0f);

				joint.GetComponent<CustomJointOrientation>()._antiYaw = Quaternion.FromToRotation (
					new Vector3 (myo.transform.forward.x, 0, myo.transform.forward.z),
					forwardDirection.transform.forward);
			}

			joystickScript.UpdateOrigin(positionChange);
		}
	}
	
}

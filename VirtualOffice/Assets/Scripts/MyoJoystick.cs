using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyoJoystick : MonoBehaviour {

	private Vector3 origin;
	public Vector3 offset = new Vector3(0,0,0);
	private bool hasPinned = false;
	private bool rotateLeft = false;
	private bool rotateRight = false;

	// Use this for initialization
	void Start () {
		origin = this.transform.position;
	}

	// Myo game object to connect with.
	// This object must have a ThalmicMyo script attached.
	public GameObject myo = null;
	
	// The pose from the last update. This is used to determine if the pose has changed
	// so that actions are only performed upon making them rather than every frame during
	// which they are active.
	private Pose _lastPose = Pose.Unknown;
	
	// Update is called once per frame.
	void Update ()
	{
		// First we take care up updating the position of the origin in case the player moved
		if (hasPinned) offset = this.transform.position - origin;

		// Access the ThalmicMyo component attached to the Myo game object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		// Check if the pose has changed since last update.
		// The ThalmicMyo component of a Myo game object has a pose property that is set to the
		// currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
		// detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
		// is not on a user's arm, pose will be set to Pose.Unknown.
		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
			rotateLeft = false;
			rotateRight = false;
			
			// Vibrate the Myo armband when a fist is made.
			if (thalmicMyo.pose == Pose.Fist) {
				thalmicMyo.Vibrate (VibrationType.Medium);
			
				ExtendUnlockAndNotifyUserAction (thalmicMyo);

			} else if (thalmicMyo.pose == Pose.WaveIn) {
				rotateLeft = true;
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			} else if (thalmicMyo.pose == Pose.WaveOut) {
				rotateRight = true;
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			} else if (thalmicMyo.pose == Pose.DoubleTap) {
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			}
		}
	}

	public void PinOrigin () {
		hasPinned = true;
		origin = this.transform.position;
	}
	
	public bool HasPinned() {
		return hasPinned;
	}

	public bool RotateLeft() {
		return rotateLeft;
	}

	public bool RotateRight() {
		return rotateRight;
	}
	
	public void UpdateOrigin(Vector3 positionChange) {
		origin += positionChange;
	}
	
	// Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
	// recognized.
	void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
	{
		ThalmicHub hub = ThalmicHub.instance;
		
		if (hub.lockingPolicy == LockingPolicy.Standard) {
			myo.Unlock (UnlockType.Timed);
		}
		
		myo.NotifyUserAction ();
	}

}

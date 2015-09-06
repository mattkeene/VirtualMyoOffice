using UnityEngine;
using System.Collections;

public class RayCasterObject : MonoBehaviour
{
		

	public GameObject emailCanvas = null;
	public GameObject calendarCanvas = null;
	public float detectionDistance = 20.0f;

	// Use this for initialization
	void Start ()
	{
		//emailCanvas = GameObject.Find ("Email");
		//calendarCanvas = GameObject.Find ("CalendarItems");

	}
		
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;

		// get the forward vector of the player's camera 
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		//create a ray using fwd vector as direction and a max size of 20.0f 
		//hit is the out parameter if something is detected
		if (Physics.Raycast (transform.position, fwd, out hit, detectionDistance)) 
		{
			//if there something in our front, check if it's the emails or calenday
			if (hit.collider.gameObject.name.Equals ("Emails")) {
				hit.collider.gameObject.SetActive(true);
				print (hit.collider.gameObject.name);
			} else if (hit.collider.gameObject.name.Equals ("Ca;endar")) {
				hit.collider.gameObject.SetActive(true);
				print (hit.collider.gameObject.name);
			}
		} 
		else 
		{
		}
	}
}
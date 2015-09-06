using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class QueryEmails : MonoBehaviour {

	public GameObject myoEvents = null;

	private List<JSONReader.MailObject> mailObjects = null;

	private UnityEngine.UI.Text textbox;

	// Use this for initialization
	void Start () {
		mailObjects = myoEvents.GetComponent<JSONReader> ().mailList;
		textbox = gameObject.GetComponent<UnityEngine.UI.Text>();

		textbox.text = "Hello World TYhis is maillll";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}

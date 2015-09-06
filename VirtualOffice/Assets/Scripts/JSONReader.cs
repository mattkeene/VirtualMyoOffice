using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class JSONReader : MonoBehaviour {
	string inp_ln = "";

	public List<MailObject> mailList = null;

	// Use this for initialization
	void Start () {
	
		string mailJsonString = readTextFile ("Assets/Scripts/mail.json");

		Debug.Log (mailJsonString);

		var mailJson = JSON.Parse(mailJsonString);
		mailList = new List<MailObject>();
		for (int i = 0; i<10; i++) {
			MailObject mail = new MailObject();
			mail.email = mailJson[i]["email"];
			mail.subject = mailJson[i]["subject"];
			mail.company = mailJson[i]["company"];
			mail.date = mailJson[i]["date"];
			mail.name = mailJson[i]["name"]["first"] + " " + mailJson[i]["name"]["last"];
			Debug.Log(mail.email);
			mailList.Add(mail);
		}
		//Free up the memory
		mailJsonString = null;
		mailJson = null;


		string todoJsonString = readTextFile ("Assets/Scripts/todo.json");
		var todoJson = JSON.Parse (todoJsonString);
		List<TodoObject> todoList = new List<TodoObject> ();
		for (int i = 0; i<10; i++) {
			TodoObject todo = new TodoObject();
			todo.date = todoJson[i]["date"];
			todo.task = todoJson[i]["task"];
			todo.done = bool.Parse(todoJson[i]["status"]);
			Debug.Log(todo.done);
			todoList.Add(todo);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	string readTextFile(string file_path)
	{
		StreamReader inp_stm = new StreamReader(file_path);
		string outputString = "";
		while(!inp_stm.EndOfStream)
		{
			inp_ln = inp_stm.ReadLine ();
			outputString += inp_ln;
		}
		
		inp_stm.Close( );  
		return outputString;
	}

	public class MailObject {
		public string email;
		public string company;
		public string subject;
		public string sender;
		public string date;
		public string name;

	}

	public class TodoObject {
		public string date;
		public string task;
		public bool done;
	}
}

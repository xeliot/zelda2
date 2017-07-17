using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatInitlaizer : MonoBehaviour {
    // Used for initializing a chat conversation
	[SerializeField]
	private Dialouge dialouge;
	[SerializeField]
	private bool isRepeatable;
	[SerializeField]
	private bool canChat;
	[SerializeField]
	private List<DialougeNode> dNodes;
	
	void OnTriggerEnter2D (Collider2D Col) 
	{
		if(Col.tag == "Player" && canChat == true)
		{
			dialouge.ChangeDialouge (dNodes);
			if (!isRepeatable) 
			{
				canChat = false;
			} 
		}
	}
}

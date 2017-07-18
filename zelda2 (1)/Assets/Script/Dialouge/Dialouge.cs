using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.CodeDom.Compiler;

[System.Serializable]
public class DialougeNode
{
    // A single side of the dialouge.. string these together to create a full converstation
	[Header("Chat information")]
	[TextArea]
	public string Dialouge;
	[Header("Who is talking?")]
	public string npcName;

	public Sprite npcImage;
}

public class Dialouge : MonoBehaviour 
{
    // Handles all the dialouge for the game... enjoy reading it!
	private bool inChat;
	private int lineNum;
	public List<DialougeNode> dNode;

	[SerializeField]
	private GameObject SubtitleHub;
	[SerializeField]
	private Text SubtitleText;

	[SerializeField]
	private Text npcName;
	[SerializeField]
	private GameObject PlayerHud;
//	[SerializeField]
//	private Image NPCImage;
	[SerializeField]
	private Movement M;

	void Start()
	{
		SubtitleHub = GameObject.Find ("ChatUI");
		PlayerHud = GameObject.Find ("MainUI");
	}

	void Update ()
	{
		if (inChat) {
			SubtitleHub.SetActive (true);
			PlayerHud.SetActive (false);

			M.isEnabled = false;
			if (Input.GetButtonUp ("Fire1")) {
				if (lineNum >= dNode.Count - 1) {
					// if the linenumber is not smaller than the current number of nodes then you are not in chat
					inChat = false;
				} else {
					lineNum++;
				}
				updateDialouge ();
			} 
		}else {
			SubtitleHub.SetActive (false);
			PlayerHud.SetActive (true);

			M.isEnabled = true;
		}
	
	}
	void updateDialouge()
	{
		SubtitleText.text = dNode [lineNum].Dialouge;
		//NPCImage.sprite = dNode [lineNum].npcImage;
		npcName.text = dNode [lineNum].npcName;
	}
	public void ChangeDialouge(List<DialougeNode> NewNodes)
	{
        // A function to change the Dialouge outright
		dNode.Clear ();
		dNode.AddRange (NewNodes);

		lineNum = 0;
		inChat = true;
		updateDialouge ();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class TestClass : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.T))
			{ 
				Do();
			}
		}

		private void Do()
		{
			GameObject.FindWithTag("MessagesHome").GetComponent<MessageFactory>().ShowMessage("00000000000000000000000000000000");
		}
	}
}

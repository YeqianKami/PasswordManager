using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class AppManager : MonoBehaviour
	{
		public GameObject FocusOn;

		private bool isReadyQuit = false;
		private float delTime = 1.0f;

		private void Update()
		{
			if (FocusOn == gameObject)
			{
				if (isReadyQuit && Input.GetKeyDown(KeyCode.Escape))
				{
					Application.Quit();
					Debug.Log("Quit");
				}
				else if (!isReadyQuit && Input.GetKeyDown(KeyCode.Escape))
				{
					isReadyQuit = true;
					Invoke("ResetDelTime", delTime);
					GameObject.FindWithTag("MessagesHome").GetComponent<MessageFactory>().ShowMessage("再按一次退出应用", delTime);
				}
			}
		}

		private void ResetDelTime()
		{
			isReadyQuit = false;
		}
	}
}

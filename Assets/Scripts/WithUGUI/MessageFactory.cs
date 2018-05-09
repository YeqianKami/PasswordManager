using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	public class MessageFactory : MonoBehaviour
	{
		[SerializeField]
		private GameObject MessagePrefab;

		private void Start()
		{
			MessagePrefab = Resources.Load<GameObject>("WithUGUI/Message");
		}

		public GameObject ShowMessage(string msg, float delTime = 2.0f)
		{
			GameObject go = Instantiate(MessagePrefab);
			go.transform.SetParent(transform);
			go.GetComponent<Message>().ShowMessage(msg, delTime);
			
			return go;
		}

	}
}
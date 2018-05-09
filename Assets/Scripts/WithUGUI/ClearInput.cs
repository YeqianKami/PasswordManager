using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class ClearInput : MonoBehaviour
	{
		[SerializeField]
		private InputField InputField;

		public void OnValueChange()
		{
			gameObject.SetActive(InputField.text.Length>0);
		}

		public void OnClick()
		{
			InputField.text = "";
			gameObject.SetActive(false);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class AboutBtn:MonoBehaviour
	{
		[SerializeField]
		private AboutBox _AboutBox;
		private void Start()
		{
			if(_AboutBox == null)
			{
				Debug.LogError("_AboutBox 为赋值");
				return;
			}
			gameObject.GetComponent<Button>().onClick.AddListener(CallBox);
		}

		private void CallBox()
		{
			_AboutBox.OpenBox(GameObject.FindWithTag("UIRoot"));
		}

	}
}

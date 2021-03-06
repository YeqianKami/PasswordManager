﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class EditorBtn : MonoBehaviour
	{
		[SerializeField]
		private InputBox InputBox;

		private void Start()
		{
			if (InputBox == null)
			{
				Debug.LogError("InputBox 未赋值");
				return;
			}
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			InputBox.OpenInputBox(Type.Edit, transform.parent.GetComponent<PassInfo>().TargetData, GameObject.FindWithTag("UIRoot"));
		}
	}
}

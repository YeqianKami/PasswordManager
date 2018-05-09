using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class MyMask : MonoBehaviour
	{
		public Action onClick;
		private Button _Button;
		
		private void OnEnable()
		{
			if (_Button == null) _Button = GetComponent<Button>();
			_Button.onClick.AddListener(OnClick);
		}
		private void OnDisable()
		{
			_Button.onClick.RemoveAllListeners();
		}

		/// <summary>
		/// 由 Mask 游戏物体身上的 <c>Button</c> 组件的 OnClick 事件调用
		/// </summary>
		private void OnClick()
		{
			onClick();
		}
	}
}

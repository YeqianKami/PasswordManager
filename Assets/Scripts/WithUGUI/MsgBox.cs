using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class MsgBox : Box
	{
		[SerializeField]
		private Text Title;
		[SerializeField]
		private Text Message;
		[SerializeField]
		private Button GoAhandBtn;
		private Action onGoAhand;
		[SerializeField]
		private Button CancelBtn;
		private Action onCancel;

		private new void OnEnable()
		{
			base.OnEnable();
			if(Title == null)
			{
				Debug.LogError("Title 未赋值");
				return;
			}
			else if(Message == null)
			{
				Debug.LogError("Message 未赋值");
				return;
			}
			else if (GoAhandBtn == null)
			{
				Debug.LogError("GoAhandBtn 未赋值");
				return;
			}
			else if (CancelBtn == null)
			{
				Debug.LogError("CancelBtn 未赋值");
				return;
			}

			GoAhandBtn.onClick.AddListener(OnGoAhand);
			CancelBtn.onClick.AddListener(OnCancel);
		}
		private new void OnDisable()
		{
			base.OnDisable();
			GoAhandBtn.onClick.RemoveAllListeners();
			CancelBtn.onClick.RemoveAllListeners();
		}

		/// <summary>
		/// 打开消息弹窗
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="goAhand"></param>
		/// <param name="cancel"></param>
		public void OpenMessageBox(string title,string message,Action goAhand,Action cancel,GameObject caller)
		{
			this.Title.text = title;
			this.Message.text = message;
			onGoAhand = goAhand;
			onCancel = cancel;

			base.OpenBox(caller);
		}
		private void OnGoAhand()
		{
			onGoAhand();
		}
		private void OnCancel()
		{
			onCancel();
		}

		[Obsolete("使用 WithUGUI.MsgBox.OpenMessageBox()",true)]//标记该方法已弃用
		/// <summary>
		/// 你应该调用本类的 OpenMessageBox 方法
		/// </summary>
		public new void OpenBox()
		{
			Debug.LogError("你应该调用本类的 OpenMessageBox 方法");
		}

		protected override void OnMaskClick()
		{
			OnCancel();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class DeleteBtn : MonoBehaviour
	{
		[SerializeField]
		private MsgBox _MsgBox;
		[SerializeField]
		private DataManager DataManager;

		private void Start()
		{
			if(GetComponent<Button>() == null)
			{
				Debug.LogError("Button 未挂载");
				return;
			}
			else if (_MsgBox == null)
			{
				Debug.LogError("MsgBox 未赋值");
				return;
			}
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		/// <summary>
		/// 由 button 组件的 onClick 事件调用
		/// </summary>
		private void OnClick()
		{
			_MsgBox.OpenMessageBox("FBI Warning", "Are you sure to delete it? U will lost it forever!", GoAhand, Cancel,GameObject.FindWithTag("UIRoot"));
		}

		private void GoAhand()
		{
			DataManager.DeleteItem(transform.parent.GetComponent<PassInfo>().TargetData);
			transform.parent.GetComponent<PassInfo>().Target = null;
			GameObject.FindWithTag("MessagesHome").GetComponent<MessageFactory>().ShowMessage("Delete Successfully");
			_MsgBox.CloseBox();
		}
		private void Cancel()
		{
			_MsgBox.CloseBox();
		}
	}
}

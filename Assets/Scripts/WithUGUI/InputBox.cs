using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	enum Type
	{
		Edit,
		Create
	}
	class InputBox : Box
	{
		private ItemData Target;

		[SerializeField]
		private Text TitleInBar;
		[SerializeField]
		private MsgBox _MsgBox;
		[Space(10)]
		[SerializeField]
		private InputField Title;
		[SerializeField]
		private InputField Account;
		[SerializeField]
		private InputField Password;
		[SerializeField]
		private InputField Scription;
		[Space(10)]
		[SerializeField]
		private Button ResetBtn;
		[SerializeField]
		private Button SaveBtn;
		[SerializeField]
		private Button CancelBtn;
		[SerializeField]
		private DataManager DataManager;

		public Type Type
		{
			get { return _type; }
		}
		private Type _type = Type.Edit;

		private new void OnEnable()
		{
			base.OnEnable();
			if (!CheckReference()) return;
			ResetBtn.onClick.AddListener(OnResetBtnClick);
			SaveBtn.onClick.AddListener(OnSaveBtnClick);
			CancelBtn.onClick.AddListener(OnCancelBtnClick);

			OnResetBtnClick();//重置
		}
		private new void OnDisable()
		{
			base.OnDisable();
			ResetBtn.onClick.RemoveAllListeners();
			SaveBtn.onClick.RemoveAllListeners();
			CancelBtn.onClick.RemoveAllListeners();
		}
		/// <summary>
		/// 检查字段引用是否完整
		/// </summary>
		/// <returns>字段的引用是否完整</returns>
		private bool CheckReference()
		{
			if (TitleInBar == null)
			{
				Debug.LogError("Title 未赋值");
				return false;
			}
			else if (ResetBtn == null)
			{
				Debug.LogError("ResetBtn 未赋值");
				return false;
			}
			else if (SaveBtn == null)
			{
				Debug.LogError("SaveBtn 未赋值");
				return false;
			}
			else if (CancelBtn == null)
			{
				Debug.LogError("CancelBtn 未赋值");
				return false;
			}
			return true;
		}

		public void OpenInputBox(Type type, ItemData target,GameObject caller)
		{
			Target = target;
			this._type = type;
			base.OpenBox(caller);
		}
		[Obsolete("你应该使用 WithUGUI.InputBox.OpenInputBox()", true)]
		public new void OpenBox()
		{
			Debug.LogError("你应该使用 WithUGUI.InputBox.OpenInputBox()");
		}

		private ItemData tempData;
		private void OnResetBtnClick()
		{
			Title.text = Target.Title;
			Account.text = Target.Account;
			Password.text = Target.Password;
			Scription.text = Target.Scription;

		}
		private void OnSaveBtnClick()
		{
			tempData = Target;

			switch (Type)
			{
				case Type.Edit:
					ChangeData();
					break;
				case Type.Create:
					CreateData();
					break;
			}
		}
		private GameObject temp;
		private void CreateData()
		{
			if (CheckData()) Target.ResetItemData(new Guid().ToString("D"), Title.text, Account.text, Password.text, Scription.text, false);
			else { return; }

			temp = DataManager.AddItem(GameObject.FindWithTag("ItemsHome").transform.childCount + 1, Target);
			temp.GetComponent<Item>().OnClick();//往界面当中添加元素并点击选择
			GameObject.FindWithTag("Scroll Bar").GetComponent<ScrollBarTransition>().TurnTo(1);

			base.CloseBox();
		}
		private void ChangeData()
		{
			if (CheckData()) Target.ResetItemData(Target.Guid, Title.text, Account.text, Password.text, Scription.text, true);
			else { return; }

			DataManager.ChangeOldData(tempData, Target);
			//项目上的信息在调用 Target.ResetItemData() 方法的时候已经修改
			GameObject.FindWithTag("ShowPasswordInfomation").GetComponent<PassInfo>().TargetData = Target;//修改面板上的信息

			base.CloseBox();
		}
		private void OnCancelBtnClick()
		{
			CloseBox();
		}

		/// <summary>
		/// 检查输入的账号信息是否合法
		/// </summary>
		/// <returns>是否合法</returns>
		private bool CheckData()
		{
			if (Title.text == null || Title.text == "")
			{
				GameObject.FindWithTag("MessagesHome").GetComponent<MessageFactory>().ShowMessage("Title is prerequisiting!");
				return false;
			}
			else if (Account.text == null || Account.text == "")
			{
				GameObject.FindWithTag("MessagesHome").GetComponent<MessageFactory>().ShowMessage("Account is prerequisiting!");
				return false;
			}
			else if (Password.text == null || Password.text == "")
			{
				GameObject.FindWithTag("MessagesHome").GetComponent<MessageFactory>().ShowMessage("Password is prerequisiting!");
				return false;
			}
			return true;
		}


		public override void CloseBox()
		{
			_MsgBox.OpenMessageBox("FBI Warning","Are u sure to cancel?",
				() => { _MsgBox.CloseBox(); base.CloseBox(); }, 
				() => { _MsgBox.CloseBox(); },
				gameObject);
		}

		protected override void OnMaskClick()
		{
			OnCancelBtnClick();
		}
	}
}

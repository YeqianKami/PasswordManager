using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	/// <summary>
	/// 挂载在信息显示面板上
	/// </summary>
	public class PassInfo : MonoBehaviour
	{
		[SerializeField]
		private Text Title;
		[SerializeField]
		private Text Account;
		[SerializeField]
		private Text Password;
		[SerializeField]
		private Text Scription;

		/// <summary>
		/// 被选中的项目,会赋值自动 TargetData.set
		/// </summary>
		public GameObject Target
		{
			get { return _Target; }
			set
			{
				if (value == null)
				{
					_Target = value;
					TargetData = null;
				}
				else
				{
					_Target = value;
					TargetData = _Target.GetComponent<ItemData>();
				}
			}
		}
		[SerializeField]
		private GameObject _Target;
		/// <summary>
		/// 目标身上的 ItemData 的引用,被赋值自动设置信息面板上的数据
		/// </summary>
		public ItemData TargetData
		{
			get { return _TargetData; }
			set
			{
				_TargetData = value;
				SetItemInfo(_TargetData);
				IsActive = value == null ? false : true;
				EditBtn.interactable = value != null;
				DeleteBtn.interactable = value != null;
			}
		}
		private ItemData _TargetData;


		public bool IsActive
		{
			get { return isActive; }
			set
			{
				isActive = value;
				EditBtn.interactable = IsActive;
				DeleteBtn.interactable = IsActive;
			}
		}
		private bool isActive = false;

		[SerializeField]
		private Button EditBtn;
		[SerializeField]
		private Button DeleteBtn;

		/// <summary>
		/// 统一改变信息栏上的信息
		/// </summary>
		/// <param name="itemData"></param>
		/// <exception cref="System.NullReferenceException">异常的信息里包含异常内容</exception>
		private void SetItemInfo(ItemData itemData)
		{
			if (itemData == null)
			{
				Title.text = "Have no data";
				Account.text = "Have no data";
				Password.text = "Have no data";

				Scription.text = "Have no data";

				return;
			}
			else if (itemData.Title == null)
			{
				throw new System.NullReferenceException("请输入标题");
			}
			else if (itemData.Account == null)
			{
				throw new System.NullReferenceException("请输入账号");
			}
			else if (itemData.Password == null)
			{
				throw new System.NullReferenceException("请输入密码");
			}

			Title.text = itemData.Title;
			Account.text = itemData.Account;
			Password.text = itemData.Password;

			Scription.text = itemData.Scription == null ? "Have no data" : itemData.Scription;
		}
	}
}
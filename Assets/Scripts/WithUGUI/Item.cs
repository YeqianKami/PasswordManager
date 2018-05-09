using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.WithUGUI
{
	/// <summary>
	/// 每一个项目必备，用来综合项目内容
	/// </summary>
	public class Item : MonoBehaviour
	{
		/// <summary>
		/// 身上的 ItemData 组件
		/// </summary>
		private ItemData Data;

		public PassInfo _passInfo;
		[SerializeField]
		private Button Button;
		/// <summary>
		/// 显示标题的 Label 组件的引用
		/// </summary>
		public string TitleText
		{
			get
			{
				return Data.Title;
			}
			set
			{
				TitleInShow.text = value == null ? "空标题" : value;
			}
		}
		[SerializeField]
		private Text TitleInShow;

		private void Start()
		{
			_passInfo = GameObject.FindWithTag("ShowPasswordInfomation").GetComponent<PassInfo>();
			Data = GetComponent<ItemData>();
			Button.onClick.AddListener(OnClick);
		}

		public void OnClick()
		{
			if (_passInfo == null) _passInfo = GameObject.FindWithTag("ShowPasswordInfomation").GetComponent<PassInfo>();
			_passInfo.Target = gameObject;
		}
	}
}
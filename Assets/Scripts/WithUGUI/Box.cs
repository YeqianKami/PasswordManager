using UnityEngine;

namespace Assets.Scripts.WithUGUI
{
	/// <summary>
	/// 所有弹窗窗口的父类
	/// </summary>
	abstract class Box : MonoBehaviour
	{
		[SerializeField]
		private GameObject _Mask;
		private MyMask _MyMask;

		private GameObject Caller;

		protected void Start()
		{
			if (_Mask == null)
			{
				Debug.LogError("Mask 为赋值");
				return;
			}
			else if(_Mask.GetComponent<MyMask>() == null)
			{
				Debug.LogError("Mask 上为挂载 MyMask 组件");
				return;
			}
			_MyMask = _Mask.GetComponent<MyMask>();
		}
		protected void OnEnable()
		{
			Start();
			_MyMask.onClick = OnMaskClick;
			GameObject.FindWithTag("UIRoot").GetComponent<AppManager>().FocusOn = gameObject;
		}
		protected void OnDisable()
		{
			GameObject.FindWithTag("UIRoot").GetComponent<AppManager>().FocusOn = Caller;
		}
		protected void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				CloseBox();
			}
		}

		/// <summary>
		/// 打开盒子
		/// </summary>
		public virtual void OpenBox(GameObject caller)
		{
			Caller = caller;
			_Mask.SetActive(true);
			gameObject.SetActive(true);
		}
		/// <summary>
		/// 关闭盒子
		/// </summary>
		public virtual void CloseBox()
		{
			_Mask.SetActive(false);
			gameObject.SetActive(false);
		}

		protected abstract void OnMaskClick();
	}
}

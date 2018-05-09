using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class AboutBox : Box
	{
		private string Author = "夜黔";
		public string AboutInfo
		{
			get
			{
				return @"作者：" + Author + "\n" +
	@"版本：" + Application.version + "\n" +
	@"使用UI框架：UGUI" + "\n" +
	@"<i><color=#ff0000><size=40>不要问我为什么只有这里有中文</size></color></i>" + "\n" + @"<i><color=#ff0000><size=40>Never ask me why only there have Chinese</size></color></i>" + "\n" +
	@"-----------分割线-----------" + "\n" +
	@"*****2.0.1 2018.3.17*****
  添加返回警告功能
*****2.0.0 2018.3.16*****
  更改了UI框架，修改了布局
*****1.2.0 2018.3.7*****
  添加了更多适用返回键的区域
  添加了退出时的消息提示
  调整了UI深度结构
  修改了项目显示顺序，由先添加在前改为后添加在前
  修改了添加和删除项目时列表的动画
*****1.1.0 2018.3.7*****
  添加了返回键退出功能
  优化了搜索功能，不区分大小写
*****1.0.4 2018.3.7*****
  修复了在弹窗情况下也能搜索的bug
*****1.0.2 2018.3.7*****
  修复了查找输入框没有内容的时候清除键不隐藏的bug
*****1.0.1 2018.3.7*****
  修复了删除确认之后弹窗不退出的BUG";
			}
		}

		[SerializeField]
		private Text _Text;
		[SerializeField]
		private Button _CancelBtn;
		private new void Start()
		{
			base.Start();
			if(_Text == null)
			{
				Debug.LogError("Text 未赋值");
				return;
			}
			_Text.text = AboutInfo;

			if(_CancelBtn == null)
			{
				Debug.LogError("CancelBtn 未赋值");
				return;
			}
			_CancelBtn.onClick.AddListener(CloseBox);
		}

		protected override void OnMaskClick()
		{
			CloseBox();
		}
	}
}

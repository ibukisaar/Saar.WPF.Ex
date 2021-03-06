﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Saar.WPF.Ex;

namespace Sample {
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window {
		static MainWindow() {
			Lisp.RegisterDelegates["to_int"] = (Func<dynamic, dynamic>)(x => x is string s && int.TryParse(s, out int val) ? val : 0);

			WindowStateProperty.Override(new FrameworkPropertyMetadata(WindowState.Maximized));
		}

		
		public static readonly DependencyProperty TestProperty = DP.Register();

		public string Test {
			get => this.GetValue<string>(TestProperty);
			set => SetValue(TestProperty, value);
		}


		public MainWindow() {
			InitializeComponent();

			Loaded += delegate {
				var dp = Test3.Data2AProperty == Test4.Data2AProperty;
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saar.WPF.Ex;
using System.Windows;

namespace Sample {
	public class Test3 : Test2<int> {
		public static readonly DependencyProperty Data3Property = DP.Register();

		static Test3() { }

		public int Data3 {
			get => this.GetValue<int>(Data3Property);
			set => SetValue(Data3Property, value);
		}
	}
}

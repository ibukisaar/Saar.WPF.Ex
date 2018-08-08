using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saar.WPF.Ex;
using System.Windows;

namespace Sample {
	public class Test2<T> : TestControl<T> {
		static readonly DP.Helper DP = GenericType.MakeDPHelper<Test2<T>>();

		public static readonly DependencyProperty Data2Property = DP.Register();
		public static readonly DependencyProperty Data2AProperty = DP.Register();

		public T Data2 {
			get => this.GetValue<T>(Data2Property);
			set => SetValue(Data2Property, value);
		}

		public Dictionary<T, string> Data2A {
			get => this.GetValue<Dictionary<T, string>>(Data2AProperty);
			set => SetValue(Data2AProperty, value);
		}
	}
}

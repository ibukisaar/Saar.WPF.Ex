using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection.Emit;
using Saar.WPF.Ex;

namespace Sample {
	public class TestControl<T> : FrameworkElement {
		static readonly DP.Helper DP = GenericType.MakeDPHelper<TestControl<T>>();

		public static readonly DependencyProperty TestProperty;
		public static readonly DependencyProperty Test2Property = DependencyProperty.Register("Test2", typeof(List<T>), typeof(TestControl<T>));
		public static readonly DependencyProperty Test3Property = DP.Register();

		static TestControl() {
			var propertyType = typeof(List<T>);
			var ownerType = typeof(TestControl<T>);
			TestProperty = DependencyProperty.Register("Test", propertyType, ownerType);
		}

		public List<T> Test {
			get => (List<T>)GetValue(TestProperty);
			set => SetValue(TestProperty, value);
		}

		public List<T> Test3 {
			get => (List<T>)GetValue(Test3Property);
			set => SetValue(Test3Property, value);
		}
	}
}

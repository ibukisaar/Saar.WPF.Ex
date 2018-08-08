using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Reflection;
using System.Linq;

namespace Saar.WPF.Ex {
	public static class DP {
		public class Exception : System.Exception {
			public Exception(string message) : base(message) { }
		}

		private static readonly Regex PropertyRegex = new Regex(@"^(?<Name>[A-Z]\w*)Property$", RegexOptions.Compiled);
		private static readonly Regex PropertyKeyRegex = new Regex(@"^(?<Name>[A-Z]\w*)PropertyKey$", RegexOptions.Compiled);
		
		private static string GetPropertyName(Regex regex, string dpName) {
			var match = regex.Match(dpName);
			if (!match.Success) throw new Exception($"'{dpName}'不是一个规范的Property名称。");
			return match.Groups["Name"].Value;
		}

		private static Type GetPropertyType(Type ownerType, string propertyName) {
			try {
				var propertyInfo = ownerType.GetProperty(propertyName);
				if (propertyInfo == null) throw new Exception($"属性'{ownerType}.{propertyName}'未定义。");
				return propertyInfo.PropertyType;
			} catch (AmbiguousMatchException e) {
				throw new Exception(e.Message);
			}
		}

		private static Type GetPropertyTypeFromMethod(Type ownerType, string propertyName) {
			try {
				var methodInfo = ownerType.GetMethod("Get" + propertyName, BindingFlags.Static | BindingFlags.Public);
				if (methodInfo == null) throw new Exception($"静态方法'{ownerType}.{"Get" + propertyName}'未定义。");
				return methodInfo.ReturnType;
			} catch (AmbiguousMatchException e) {
				throw new Exception(e.Message);
			}
		}

		public static DependencyProperty Register(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.Register(propertyName, GetPropertyType(ownerType, propertyName), ownerType, typeMetadata, validateValueCallback);
		}

		public static DependencyProperty Register<T>(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.Register(propertyName, typeof(T), ownerType, typeMetadata, validateValueCallback);
		}

		public static DependencyPropertyKey RegisterReadOnly(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.RegisterReadOnly(propertyName, GetPropertyType(ownerType, propertyName), ownerType, typeMetadata, validateValueCallback);
		}

		public static DependencyPropertyKey RegisterReadOnly<T>(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.RegisterReadOnly(propertyName, typeof(T), ownerType, typeMetadata, validateValueCallback);
		}

		public static DependencyProperty RegisterAttached(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.RegisterAttached(dpName, GetPropertyTypeFromMethod(ownerType, propertyName), ownerType, defaultMetadata, validateValueCallback);
		}

		public static DependencyProperty RegisterAttached<T>(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.RegisterAttached(dpName, typeof(T), ownerType, defaultMetadata, validateValueCallback);
		}

		public static DependencyPropertyKey RegisterAttachedReadOnly(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.RegisterAttachedReadOnly(dpName, GetPropertyTypeFromMethod(ownerType, propertyName), ownerType, defaultMetadata, validateValueCallback);
		}

		public static DependencyPropertyKey RegisterAttachedReadOnly<T>(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
			var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
			var ownerType = ExTool.GetDeclaringType();
			return DependencyProperty.RegisterAttachedReadOnly(dpName, typeof(T), ownerType, defaultMetadata, validateValueCallback);
		}

		public static void Override(this DependencyProperty dp, PropertyMetadata typeMetadata) {
			dp.OverrideMetadata(ExTool.GetDeclaringType(), typeMetadata);
		}

		public static void Override(this DependencyPropertyKey dpKey, PropertyMetadata typeMetadata) {
			dpKey.DependencyProperty.OverrideMetadata(ExTool.GetDeclaringType(), typeMetadata, dpKey);
		}

		public static T GetValue<T>(this DependencyObject obj, DependencyProperty dp) {
			return (T)obj.GetValue(dp);
		}

		public class Helper {
			private readonly Type ownerType;

			internal Helper(Type ownerType) => this.ownerType = ownerType;


			public DependencyProperty Register(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyRegex, dpName);
				return DependencyProperty.Register(propertyName, GetPropertyType(ownerType, propertyName), ownerType, typeMetadata, validateValueCallback);
			}

			public DependencyProperty Register<T>(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyRegex, dpName);
				return DependencyProperty.Register(propertyName, typeof(T), ownerType, typeMetadata, validateValueCallback);
			}

			public DependencyPropertyKey RegisterReadOnly(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
				return DependencyProperty.RegisterReadOnly(propertyName, GetPropertyType(ownerType, propertyName), ownerType, typeMetadata, validateValueCallback);
			}

			public DependencyPropertyKey RegisterReadOnly<T>(PropertyMetadata typeMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
				return DependencyProperty.RegisterReadOnly(propertyName, typeof(T), ownerType, typeMetadata, validateValueCallback);
			}

			public DependencyProperty RegisterAttached(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyRegex, dpName);
				return DependencyProperty.RegisterAttached(dpName, GetPropertyTypeFromMethod(ownerType, propertyName), ownerType, defaultMetadata, validateValueCallback);
			}

			public DependencyProperty RegisterAttached<T>(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyRegex, dpName);
				return DependencyProperty.RegisterAttached(dpName, typeof(T), ownerType, defaultMetadata, validateValueCallback);
			}

			public DependencyPropertyKey RegisterAttachedReadOnly(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
				return DependencyProperty.RegisterAttachedReadOnly(dpName, GetPropertyTypeFromMethod(ownerType, propertyName), ownerType, defaultMetadata, validateValueCallback);
			}

			public DependencyPropertyKey RegisterAttachedReadOnly<T>(PropertyMetadata defaultMetadata = null, ValidateValueCallback validateValueCallback = null, [CallerMemberName] string dpName = null) {
				var propertyName = GetPropertyName(PropertyKeyRegex, dpName);
				return DependencyProperty.RegisterAttachedReadOnly(dpName, typeof(T), ownerType, defaultMetadata, validateValueCallback);
			}
		}
	}
}

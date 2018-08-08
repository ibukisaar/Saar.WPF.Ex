using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saar.WPF.Ex {
	static class ExTool {
		public static Type GetDeclaringType() {
			var st = new StackTrace();
			var frame = st.GetFrame(2);
			var method = frame.GetMethod();
			var type = method.DeclaringType;

			if (type.IsGenericTypeDefinition) {
				throw new InvalidOperationException($"不完整的泛型 {type} 无法注册DependencyProperty。");
			}
			return type;
		}
	}
}

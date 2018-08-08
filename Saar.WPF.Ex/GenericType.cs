using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saar.WPF.Ex {
	public static class GenericType {
		public static DP.Helper MakeDPHelper(Type type) => new DP.Helper(type);

		public static DP.Helper MakeDPHelper<T>() => new DP.Helper(typeof(T));

		public static EM.Helper MakeEMHelper(Type type) => new EM.Helper(type);

		public static EM.Helper MakeEMHelper<T>() => new EM.Helper(typeof(T));

	}
}

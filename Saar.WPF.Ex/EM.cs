using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Saar.WPF.Ex {
	public static class EM {
		static readonly Regex eventNameRegex = new Regex(@"^(?<Name>[A-Z]\w*)Event$", RegexOptions.Compiled);

		private static Type GetDeclaringType() {
			var st = new StackTrace();
			var frame = st.GetFrame(2);
			return frame.GetMethod().DeclaringType;
		}

		private static string GetEventName(string eventName) {
			var match = eventNameRegex.Match(eventName);
			if (!match.Success) throw new Exception($"'{eventName}'不是一个规范的Event名称。");
			return match.Groups["Name"].Value;
		}

		private static Type GetHandlerType(Type ownerType, string eventName) {
			var eventInfo = ownerType.GetEvent(eventName);
			if (eventInfo == null) throw new Exception($"事件'{ownerType}.{eventName}'未定义。");
			return eventInfo.EventHandlerType;
		}

		public static RoutedEvent RegisterEvent(RoutingStrategy strategy = RoutingStrategy.Direct, [CallerMemberName] string eventName = null) {
			eventName = GetEventName(eventName);
			var ownerType = GetDeclaringType();
			return EventManager.RegisterRoutedEvent(eventName, strategy, GetHandlerType(ownerType, eventName), ownerType);
		}

		public static RoutedEvent RegisterEvent<TDelegate>(RoutingStrategy strategy = RoutingStrategy.Direct, [CallerMemberName] string eventName = null) where TDelegate : Delegate {
			eventName = GetEventName(eventName);
			var ownerType = GetDeclaringType();
			return EventManager.RegisterRoutedEvent(eventName, strategy, typeof(TDelegate), ownerType);
		}

		public class Helper {
			private readonly Type ownerType;

			internal Helper(Type ownerType) => this.ownerType = ownerType;

			public RoutedEvent RegisterEvent(RoutingStrategy strategy = RoutingStrategy.Direct, [CallerMemberName] string eventName = null) {
				eventName = GetEventName(eventName);
				return EventManager.RegisterRoutedEvent(eventName, strategy, GetHandlerType(ownerType, eventName), ownerType);
			}

			public RoutedEvent RegisterEvent<TDelegate>(RoutingStrategy strategy = RoutingStrategy.Direct, [CallerMemberName] string eventName = null) where TDelegate : Delegate {
				eventName = GetEventName(eventName);
				return EventManager.RegisterRoutedEvent(eventName, strategy, typeof(TDelegate), ownerType);
			}
		}
	}
}

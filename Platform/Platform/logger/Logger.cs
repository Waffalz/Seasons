using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.logger {

	class Logger {

		public enum Level {
			OFF,
			FATAL,
			ERROR,
			WARN,
			INFO,
			DEBUG,
			TRACE,
			ALL,
		}

		private static string GetNowTimestamp() {
			string nowString = DateTime.Now.ToLongTimeString();
			return nowString.Substring( 0, nowString.Length - 3 );
		}

		public static void Log( Level level, string className, string classNamespace, object message ) {
			Console.WriteLine( "[" + GetNowTimestamp() + "] [" + className + "/" + level + "] [" + classNamespace + "]: " + message.ToString() );
		}

		public static void Off( string className, string classNamespace, object message ) {
			Log( Level.OFF, className, classNamespace, message );
		}

		public static void Fatal( string className, string classNamespace, object message ) {
			Log( Level.FATAL, className, classNamespace, message );
		}

		public static void Error( string className, string classNamespace, object message ) {
			Log( Level.ERROR, className, classNamespace, message );
		}

		public static void Warn( string className, string classNamespace, object message ) {
			Log( Level.WARN, className, classNamespace, message );
		}

		public static void Info( string className, string classNamespace, object message ) {
			Log( Level.INFO, className, classNamespace, message );
		}

		public static void Debug( string className, string classNamespace, string message ) {
			Log( Level.DEBUG, className, classNamespace, message );
		}

		public static void Trace( string className, string classNamespace, string message ) {
			Log( Level.TRACE, className, classNamespace, message );
		}

		public static void All( string className, string classNamespace, string message ) {
			Log( Level.ALL, className, classNamespace, message );
		}

	}

}
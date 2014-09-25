using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using Mono.Cecil;

namespace FindDerived
{
	class MainClass
	{
		const string XamarinIOS = "/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/Xamarin.iOS.dll";
		const string Monotouch = "/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/2.1/monotouch.dll";

		public static void Main (string[] args)
		{
//			ModuleDefinition mtouchDef = ModuleDefinition.ReadModule (Monotouch);
			ModuleDefinition moduleDef = ModuleDefinition.ReadModule (XamarinIOS);
			ModuleHandler handler = new ModuleHandler (moduleDef);
//			handler.FindDerivedFrom ("MonoTouch.Foundation.DictionaryContainer");
//			handler.FindDerivedFrom ("MonoTouch.CoreVideo.CVPixelBufferAttributes");
//			handler.FindMethodWithParameterName ("options");
//			handler.FindMethodWithParameter ("MonoTouch.Foundation.NSDictionary", "options");
//			handler.FindMethodWithParameterType ("CoreGraphics.CGAffineTransform");

//			handler.FindMethodWithParameterName ("completionHandler");
//			handler.FindMethodWithStringConstant ("MKMetersPerMapPointAtLatitude:");
//			handler.FindPropertiesByType ("MobileCoreServices.UTType", "Foundation.NSString");

			/*
			PropertyDefinition prop = handler.FindProperty ("CloudKit.CKErrorFields", "ErrorRetryAfterKey");
			PropertyUsageFinder pUsageFinder = new PropertyUsageFinder (moduleDef, prop);
			IEnumerable<MethodReference> callers = pUsageFinder.Find ();
			Print (callers);
			*/

			var paramPolicy = new MethodParametersTypeNameMatch (new TypeNameMatch { Type = "Action" });
			var namePolicy = new MethodNameStartWithPolicy ("Set");
			var andPolicy = new AndPolicy<MethodDefinition> (namePolicy, paramPolicy);

			var methodFinder = new MethodFinder (moduleDef, andPolicy);
			Print (methodFinder.Find ());
		}

		static void Print(IEnumerable<MethodDefinition> methods)
		{
			Printer printer = new Printer ();
			foreach (var mRef in methods)
				printer.Print (mRef);
		}
	}
}

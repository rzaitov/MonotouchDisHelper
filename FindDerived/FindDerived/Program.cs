using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using Mono.Cecil;

namespace FindDerived
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*
			Assembly asm = Assembly.LoadFrom ("monotouch.dll");
			IEnumerable<Type> types = asm.GetLoadableTypes ();

			Type dicContainerType = types.Where (t => t.Name == "DictionaryContainer").First ();

			foreach (var t in types) {
				if(t.IsSubclassOf(dicContainerType))
					Console.WriteLine (t.Name);
			}
			*/

			/*
			ModuleDefinition mtouchDef = ModuleDefinition.ReadModule ("monotouch.dll");
			foreach (TypeDefinition typeDef in mtouchDef.Types) {
				Console.WriteLine (typeDef.FullName);
			}
			*/

//			ModuleDefinition mtouchDef = ModuleDefinition.ReadModule ("monotouch.dll");
			ModuleDefinition mtouchDef = ModuleDefinition.ReadModule ("Xamarin.iOS.dll");
			Printer printer = new Printer ();
			ModuleHandler handler = new ModuleHandler (mtouchDef);
//			handler.FindDerivedFrom ("MonoTouch.Foundation.DictionaryContainer");
//			handler.FindDerivedFrom ("MonoTouch.CoreVideo.CVPixelBufferAttributes");
//			handler.FindMethodWithParameterName ("options");
//			handler.FindMethodWithParameter ("MonoTouch.Foundation.NSDictionary", "options");
//			handler.FindMethodWithParameterType ("CoreGraphics.CGAffineTransform");

//			handler.FindMethodWithParameterName ("completionHandler");
//			handler.FindMethodWithStringConstant ("MKMetersPerMapPointAtLatitude:");
//			handler.FindPropertiesByType ("MobileCoreServices.UTType", "Foundation.NSString");


			PropertyDefinition prop = handler.FindProperty ("UIKit.UIImagePickerController", "OriginalImage");
			PropertyUsageFinder pUsageFinder = new PropertyUsageFinder (mtouchDef, prop);
			IEnumerable<MethodReference> callers = pUsageFinder.Find ();

			foreach (var mRef in callers)
				printer.Print (mRef);
		}
	}

	/*
	static class Ext
	{
		public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException("assembly");
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
	}
	*/
}

using System;

using Mono.Cecil;

namespace FindDerived
{
	public class Printer
	{
		public Printer ()
		{
		}

		public void Print(MethodReference mRef)
		{
			Console.WriteLine ("{0} {1}", mRef.DeclaringType.FullName, mRef.ToString());
		}
	}
}


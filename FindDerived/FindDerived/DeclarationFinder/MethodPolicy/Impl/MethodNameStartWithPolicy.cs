using System;

namespace FindDerived
{
	public class MethodNameStartWithPolicy : MethodNamePolicy
	{
		readonly string prefix;

		public MethodNameStartWithPolicy (string prefix)
		{
			this.prefix = prefix;
		}

		protected override bool NameSatisfy (string methodName)
		{
			return methodName.StartsWith (prefix);
		}
	}
}

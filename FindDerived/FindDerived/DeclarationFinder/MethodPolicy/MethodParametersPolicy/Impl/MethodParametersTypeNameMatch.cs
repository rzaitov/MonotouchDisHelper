using System;

namespace FindDerived
{
	public class MethodParametersTypeNameMatch : MethodParametersPolicy
	{
		public MethodParametersTypeNameMatch ()
		{
		}

		protected override void Satisfy (Mono.Collections.Generic.Collection<Mono.Cecil.ParameterDefinition> parameters)
		{
			throw new NotImplementedException ();
		}
	}
}


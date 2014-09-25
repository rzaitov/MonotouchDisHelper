using System;

using Mono.Cecil;
using Mono.Collections.Generic;

namespace FindDerived
{
	public abstract class MethodParametersPolicy : MethodPolicy
	{
		protected abstract void Satisfy(Collection<ParameterDefinition> parameters);

		public override bool Satisfy (MethodDefinition item)
		{
			return Satisfy (item.Parameters);
		}
//		readonly string match;
//
//		public MethodParameterdPolicy (string match)
//		{
//			this.match = match;
//		}
//
//		public override bool Satisfy (MethodDefinition methodDef)
//		{
//			Collection<ParameterDefinition> parameters = methodDef.Parameters;
//			bool satisfy = parameters.Count == 1;
//
//			if (satisfy) {
//				TypeReference paramTypeRef = parameters[0].ParameterType;
//				satisfy &= paramTypeRef.FullName.Contains(match);
//			}
//
//			return satisfy;
//		}
	}
}


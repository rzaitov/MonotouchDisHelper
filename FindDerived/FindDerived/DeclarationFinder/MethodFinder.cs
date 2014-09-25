using System;

using Mono.Cecil;

namespace FindDerived
{
	public class MethodFinder : CecilVisitorBase
	{
		ModuleDefinition moduleDef;

		public MethodFinder (ModuleDefinition moduleDef)
		{
			this.moduleDef = moduleDef;
		}

		public override void VisitModuleDef (ModuleDefinition moduleDef)
		{
			foreach (var t in moduleDef.Types)
				VisitTypeDef (t);
		}

		public override void VisitTypeDef (TypeDefinition typeDef)
		{
			foreach (var m in typeDef.Methods)
				VisitMethodDef (m);
		}

		public override void VisitMethodDef (MethodDefinition methodDef)
		{
			throw new NotImplementedException ();
		}
	}
}


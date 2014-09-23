using System;

using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace FindDerived
{
	public class PropertyUsageFinder : CecilVisitorBase
	{
		PropertyDefinition propertyCallee;
		ModuleDefinition moduleDef;
		System.Collections.Generic.List<MethodReference> callers;

		public PropertyUsageFinder (ModuleDefinition moduleDef, PropertyDefinition propertyDef)
		{
			propertyCallee = propertyDef;
			this.moduleDef = moduleDef;
			callers = new System.Collections.Generic.List<MethodReference> ();
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

			foreach (var p in typeDef.Properties)
				VisitPropertyDef (p);
		}

		public override void VisitMethodDef (MethodDefinition methodDef)
		{
			if (!methodDef.HasBody)
				return;

			Collection<Instruction> instructions = methodDef.Body.Instructions;
			foreach (var il in instructions) {
				if (il.OpCode.Equals (OpCodes.Call)) {
					var mRef = (MethodReference)il.Operand;
					if (mRef.Equals (propertyCallee.GetMethod) || mRef.Equals (propertyCallee.SetMethod))
						callers.Add (methodDef);
				}
			}
		}

		public System.Collections.Generic.IEnumerable<MethodReference> Find()
		{
			VisitModuleDef (moduleDef);
			return callers;
		}
	}
}


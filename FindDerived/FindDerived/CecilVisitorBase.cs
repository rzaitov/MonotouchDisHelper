using System;

using Mono.Cecil;

namespace FindDerived
{
	public class CecilVisitorBase
	{
		public CecilVisitorBase ()
		{
		}

		public virtual void VisitModuleDef(ModuleDefinition moduleDef)
		{

		}

		public virtual void VisitTypeDef(TypeDefinition typeDef)
		{

		}

		public virtual void VisitMethodDef(MethodDefinition methodDef)
		{

		}

		public virtual void VisitPropertyDef(PropertyDefinition propertyDef)
		{

		}

		public virtual void VisitFieldDef(FieldDefinition fieldDef)
		{

		}
	}
}


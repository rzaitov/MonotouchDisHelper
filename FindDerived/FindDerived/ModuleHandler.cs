using System;

using Mono.Cecil;
using System.Linq;
using Mono.Collections.Generic;
using Mono.Cecil.Cil;
using System.Collections.Generic;

namespace FindDerived
{
	public class ModuleHandler
	{
		private ModuleDefinition _module;
		public ModuleHandler (ModuleDefinition module)
		{
			_module = module;
		}

		public void FindMethodWithStringConstant(string literal)
		{
			foreach (var td in _module.Types) {
				foreach (var mDef in td.Methods) {
					if (!mDef.HasBody)
						continue;

					Collection<Instruction> instructions = mDef.Body.Instructions;
					foreach (var il in instructions) {
						if (il.OpCode == OpCodes.Ldstr && ((string)il.Operand) == literal) {
							Console.WriteLine (td.FullName);
							break;
						}
					}
				}
			}
		}


		public PropertyDefinition FindProperty(string propertyHolderFullName, string propertyName)
		{
			foreach (var t in _module.Types) {
				if (t.FullName != propertyHolderFullName)
					continue;

				foreach (PropertyDefinition p in t.Properties) {
					if (p.Name == propertyName)
						return p;
				}
			}

			return null;
		}

		public void FindMethodWithParameterName(string parameterName)
		{
			foreach (var td in _module.Types) {
				foreach (var mDef in td.Methods) {
					if (mDef.IsConstructor)
						continue;

					foreach (var pDef in mDef.Parameters) {
						if(pDef.Name == parameterName)
							Console.WriteLine ("{0} {1}", td.FullName, mDef.ToString());
					} 
				}
			}
		}

		public void FindMethodWithParameterType(string parameterTypeFullName)
		{
			foreach (var td in _module.Types) {
				foreach (var mDef in td.Methods) {
					if (mDef.IsConstructor)
						continue;

					foreach (var pDef in mDef.Parameters) {
						if(pDef.ParameterType.FullName == parameterTypeFullName)
							Console.WriteLine ("{0} {1}", td.FullName, mDef.ToString());
					}
				}
			}
		}

		public void FindMethodWithParameter(string parameterTypeFullName, string parameterName)
		{
			foreach (var td in _module.Types) {
				foreach (var mDef in td.Methods) {
					if (mDef.IsConstructor)
						continue;

					foreach (var pDef in mDef.Parameters) {
						if(pDef.Name.ToLower() == parameterName && pDef.ParameterType.FullName == parameterTypeFullName)
							Console.WriteLine ("{0} {1}", td.FullName, mDef.ToString());
					} 
				}
			}
		}




		public void FindDerivedFrom(string baseTypeFullName)
		{
			TypeDefinition baseTypeRef = _module.Types.First (td => td.FullName == baseTypeFullName);
			FindDerivedFrom (baseTypeRef);
		}

		public void FindDerivedFrom(TypeReference baseTypeRef)
		{
			foreach (var td in _module.Types) {
				if(IsDerivedFrom(td, baseTypeRef))
					Console.WriteLine (td.FullName);
			}
		}

		private bool IsDerivedFrom(TypeDefinition typeDef, TypeReference baseTypeRef)
		{
			bool isDerived = false;

			while (typeDef.BaseType != null) {
				isDerived = typeDef.BaseType.Equals (baseTypeRef);

				if (isDerived 
					|| typeDef.Module != _module)
					break;

				typeDef = typeDef.BaseType.Resolve();
			}

			return isDerived;
		}

		public void FindPropertiesByType(string typeFullName, string propertyTypeFullName)
		{
			TypeDefinition typeDef = _module.GetType (typeFullName);
			Collection<PropertyDefinition> props = typeDef.Properties;

			foreach (PropertyDefinition p in props) {
				if(p.PropertyType.FullName == propertyTypeFullName)
					Console.WriteLine (p.FullName);
			}
		}
	}
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;

namespace Entitas.VisualDebugging.Unity.Editor {

	public class EnumTypeDrawer : ITypeDrawer {

		class EnumInfo {
			public string[] nameList;
			public object[] valueList;
		}

		private readonly Dictionary<Type, EnumInfo> _enumDict = new Dictionary<Type, EnumInfo>();

		public bool HandlesType(Type type) {
			return type.IsEnum;
		}

		public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {
			// handle flag enum by default
			if (memberType.IsDefined(typeof(FlagsAttribute), false))
				return EditorGUILayout.EnumMaskField(memberName, (Enum)value);

			// handle enum without description by default
			var enumInfo = GetEnumInfo(memberType);
			if (enumInfo == null)
				return EditorGUILayout.EnumPopup(memberName, (Enum)value);

			int index = Array.IndexOf(enumInfo.valueList, value);
			index = EditorGUILayout.Popup(memberName, index, enumInfo.nameList);

			if (index < 0 || index >= enumInfo.valueList.Length)
				return value;

			return enumInfo.valueList[index];
		}

		private EnumInfo GetEnumInfo(Type type) {
			EnumInfo enumInfo = null;

			// lookup and return cached enum
			if (_enumDict.TryGetValue(type, out enumInfo))
				return enumInfo;

			// if not found, try fetch enum info
			var array = Enum.GetValues(type);

			enumInfo = new EnumInfo();
			enumInfo.nameList = new string[array.Length];
			enumInfo.valueList = new object[array.Length];

			bool hasDescription = false;

			// find descriptions
			for (int i = 0; i < array.Length; i++) {
				var value = array.GetValue(i);
				var name = Enum.GetName(type, value);

				enumInfo.valueList[i] = value;
				enumInfo.nameList[i] = name;

				var field = type.GetField(name);
				if (field != null) {
					var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
					if (attributes != null && attributes.Length > 0) {
						var desc = (DescriptionAttribute)attributes[0];

						// format name + description
						enumInfo.nameList[i] = $"{name} | {desc.Description}";
						hasDescription = true;
					}
				}
			}

			// if no descriptions found, just keep null in cache
			if (!hasDescription)
				enumInfo = null;

			_enumDict[type] = enumInfo;

			return enumInfo;
		}
	}
}

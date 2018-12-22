using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor {

    public static class EntitasStats {

        [MenuItem("Tools/Entitas/Show Stats", false, 200)]
        public static void ShowStats() {
            var stats = string.Join("\n", GetStats()
                                    .Select(kv => kv.Key + ": " + kv.Value)
                                    .ToArray());

            Debug.Log(stats);
            EditorUtility.DisplayDialog("Entitas Stats", stats, "Close");
        }

        public static Dictionary<string, int> GetStats() {
            var types = AppDomain.CurrentDomain.GetAllTypes();

            var components = types
                .Where(type => type.ImplementsInterface<IComponent>())
                .ToArray();

            var systems = types
                .Where(isSystem)
                .ToArray();

            var contexts = getContexts(components);

            var stats = new Dictionary<string, int> {
                { "Total Components", components.Length },
                { "Systems", systems.Length }
            };

            foreach (var context in contexts) {
                stats.Add("Components in " + context.Key, context.Value);
            }

            return stats;
        }

        static Dictionary<string, int> getContexts(Type[] components) {
			var contexts = new Dictionary<string, int>();
			
			var DefaultAttribs = new object[] { new Default() };
			foreach (var t in components) {
				var attribs = t.GetCustomAttributes(typeof(ContextAttribute), false);
				if (attribs == null || attribs.Length <= 0)
					attribs = DefaultAttribs;

				foreach (var attr in attribs) {
					var contextName = ((ContextAttribute)attr).name;
					if (!contexts.ContainsKey(contextName))
						contexts.Add(contextName, 1);
					else 
						contexts[contextName] += 1;
				}
			}
            return contexts;
        }

        static bool isSystem(Type type) {
            return type.ImplementsInterface<ISystem>()
                && type != typeof(ReactiveSystem)
                && type != typeof(Systems)
                && type != typeof(DebugSystems);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public class SystemManager
	{
		public static int defaultSystemCapacity = 128;

		private readonly Context _context;
		private readonly List<SystemProxy> _systems = new List<SystemProxy>(defaultSystemCapacity);

		private bool _hasSorted = false;

		public SystemManager(Context context)
		{
			_context = context;
		}

		public SystemManager Add(SystemBase system, int priority = 0)
		{
			if (system == null)
				return this;

			var proxy = _systems.Find(p => p.system == system);
			if (proxy != null)
				throw new EntitasException($"Duplicate {system.GetType().FullName} is found in SystemManager");

			if (system.context != null)
				throw new EntitasException($"Context conflict of {system.GetType().FullName} in SystemManager");

			system.context = _context;

			_systems.Add(new SystemProxy(system, priority));
			
			_hasSorted = false;
			return this;
		}

		public void Execute()
		{
			MakeSorted();

			for (int i = 0; i < _systems.Count; i++)
			{
				_context.Poll();
				_systems[i].system.Execute();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void MakeSorted()
		{
			if (_hasSorted)
				return;

			_systems.Sort();
			_hasSorted = true;
		}

		class SystemProxy : IComparable<SystemProxy>
		{
			public readonly SystemBase system;
			public readonly int priority;
			public readonly string name;

			public SystemProxy(SystemBase s, int prior)
			{
				system = s;
				priority = prior;
				name = s.GetType().FullName;
			}

			public int CompareTo(SystemProxy other)
			{
				int priorDiff = priority - other.priority;
				if (priorDiff != 0)
					return priorDiff;

				return string.CompareOrdinal(name, other.name);
			}
		}
	}
}

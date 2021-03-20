using System;
using System.Collections.Generic;

namespace Entitas
{
	public class SystemManager
	{
		private readonly Context _context;
		private readonly List<SystemProxy> _systems = new List<SystemProxy>(128);

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

			if (system.Context != null)
				throw new EntitasException($"Conflict context for {system.GetType().FullName} is found in SystemManager");

			system.Context = _context;

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

		public void GetSystems(IList<SystemBase> output)
		{
			output.Clear();

			MakeSorted();

			for (int i = 0; i < _systems.Count; i++)
				output.Add(_systems[i].system);
		}

		private void MakeSorted()
		{
			if (_hasSorted)
				return;

			_systems.Sort();
			_hasSorted = true;
		}

		private class SystemProxy : IComparable<SystemProxy>
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

using System;

namespace Entitas
{
	public class EntitasException : Exception
	{
		public EntitasException(string message, string hint = null)
			: base(hint != null ? $"{message}\n{hint}" : message)
		{
		}
	}
}

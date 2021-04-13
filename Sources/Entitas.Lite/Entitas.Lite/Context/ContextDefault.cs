using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Context
	{
		public const string DefaultContextName = "Default";
		
		public static Context Default
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get {
				if (_defaultContext == null)
					_defaultContext = new Context(DefaultContextName);
				
				return _defaultContext;
			}
		}

		public static void DestroyDefault()
		{
			if (_defaultContext != null)
			{
				_defaultContext.Clear();
				_defaultContext = null;
			}
		}

		private static Context _defaultContext;
	}
}

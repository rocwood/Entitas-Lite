using System.Runtime.CompilerServices;

namespace Entitas
{
	public struct EntityQueryAllBuilder<T1> where T1:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1> Get()
			=> (EntityQueryAll<T1>)_context.GetQuery(typeof(EntityQueryAll<T1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAll<T1>.Without<X1>)_context.GetQuery(typeof(EntityQueryAll<T1>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAll<T1>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAll<T1>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAll<T1>(EntityQueryAllBuilder<T1> b) => b.Get();

		internal EntityQueryAllBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAllBuilder<T1,T2> where T1:IComponent where T2:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2> Get()
			=> (EntityQueryAll<T1,T2>)_context.GetQuery(typeof(EntityQueryAll<T1,T2>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAll<T1,T2>.Without<X1>)_context.GetQuery(typeof(EntityQueryAll<T1,T2>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAll<T1,T2>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAll<T1,T2>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAll<T1,T2>(EntityQueryAllBuilder<T1,T2> b) => b.Get();

		internal EntityQueryAllBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAllBuilder<T1,T2,T3> where T1:IComponent where T2:IComponent where T3:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3> Get()
			=> (EntityQueryAll<T1,T2,T3>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAll<T1,T2,T3>.Without<X1>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAll<T1,T2,T3>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAll<T1,T2,T3>(EntityQueryAllBuilder<T1,T2,T3> b) => b.Get();

		internal EntityQueryAllBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAllBuilder<T1,T2,T3,T4> where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4> Get()
			=> (EntityQueryAll<T1,T2,T3,T4>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAll<T1,T2,T3,T4>.Without<X1>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAll<T1,T2,T3,T4>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAll<T1,T2,T3,T4>(EntityQueryAllBuilder<T1,T2,T3,T4> b) => b.Get();

		internal EntityQueryAllBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAllBuilder<T1,T2,T3,T4,T5> where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4,T5> Get()
			=> (EntityQueryAll<T1,T2,T3,T4,T5>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4,T5>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4,T5>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAll<T1,T2,T3,T4,T5>.Without<X1>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4,T5>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4,T5>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAll<T1,T2,T3,T4,T5>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4,T5>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAll<T1,T2,T3,T4,T5>(EntityQueryAllBuilder<T1,T2,T3,T4,T5> b) => b.Get();

		internal EntityQueryAllBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAllBuilder<T1,T2,T3,T4,T5,T6> where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent where T6:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4,T5,T6> Get()
			=> (EntityQueryAll<T1,T2,T3,T4,T5,T6>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4,T5,T6>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4,T5,T6>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAll<T1,T2,T3,T4,T5,T6>.Without<X1>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4,T5,T6>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAll<T1,T2,T3,T4,T5,T6>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAll<T1,T2,T3,T4,T5,T6>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAll<T1,T2,T3,T4,T5,T6>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAll<T1,T2,T3,T4,T5,T6>(EntityQueryAllBuilder<T1,T2,T3,T4,T5,T6> b) => b.Get();

		internal EntityQueryAllBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAnyBuilder<T1> where T1:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1> Get()
			=> (EntityQueryAny<T1>)_context.GetQuery(typeof(EntityQueryAny<T1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAny<T1>.Without<X1>)_context.GetQuery(typeof(EntityQueryAny<T1>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAny<T1>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAny<T1>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAny<T1>(EntityQueryAnyBuilder<T1> b) => b.Get();

		internal EntityQueryAnyBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAnyBuilder<T1,T2> where T1:IComponent where T2:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2> Get()
			=> (EntityQueryAny<T1,T2>)_context.GetQuery(typeof(EntityQueryAny<T1,T2>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAny<T1,T2>.Without<X1>)_context.GetQuery(typeof(EntityQueryAny<T1,T2>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAny<T1,T2>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAny<T1,T2>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAny<T1,T2>(EntityQueryAnyBuilder<T1,T2> b) => b.Get();

		internal EntityQueryAnyBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAnyBuilder<T1,T2,T3> where T1:IComponent where T2:IComponent where T3:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3> Get()
			=> (EntityQueryAny<T1,T2,T3>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAny<T1,T2,T3>.Without<X1>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAny<T1,T2,T3>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAny<T1,T2,T3>(EntityQueryAnyBuilder<T1,T2,T3> b) => b.Get();

		internal EntityQueryAnyBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAnyBuilder<T1,T2,T3,T4> where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4> Get()
			=> (EntityQueryAny<T1,T2,T3,T4>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAny<T1,T2,T3,T4>.Without<X1>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAny<T1,T2,T3,T4>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAny<T1,T2,T3,T4>(EntityQueryAnyBuilder<T1,T2,T3,T4> b) => b.Get();

		internal EntityQueryAnyBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAnyBuilder<T1,T2,T3,T4,T5> where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4,T5> Get()
			=> (EntityQueryAny<T1,T2,T3,T4,T5>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4,T5>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4,T5>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAny<T1,T2,T3,T4,T5>.Without<X1>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4,T5>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4,T5>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAny<T1,T2,T3,T4,T5>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4,T5>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAny<T1,T2,T3,T4,T5>(EntityQueryAnyBuilder<T1,T2,T3,T4,T5> b) => b.Get();

		internal EntityQueryAnyBuilder(Context c) => _context = c;
		readonly Context _context;
	}

	public struct EntityQueryAnyBuilder<T1,T2,T3,T4,T5,T6> where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent where T6:IComponent 
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4,T5,T6> Get()
			=> (EntityQueryAny<T1,T2,T3,T4,T5,T6>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4,T5,T6>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4,T5,T6>.Without<X1> Without<X1>() where X1:IComponent 
			=> (EntityQueryAny<T1,T2,T3,T4,T5,T6>.Without<X1>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4,T5,T6>.Without<X1>));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityQueryAny<T1,T2,T3,T4,T5,T6>.Without<X1,X2> Without<X1,X2>() where X1:IComponent where X2:IComponent 
			=> (EntityQueryAny<T1,T2,T3,T4,T5,T6>.Without<X1,X2>)_context.GetQuery(typeof(EntityQueryAny<T1,T2,T3,T4,T5,T6>.Without<X1,X2>));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator EntityQueryAny<T1,T2,T3,T4,T5,T6>(EntityQueryAnyBuilder<T1,T2,T3,T4,T5,T6> b) => b.Get();

		internal EntityQueryAnyBuilder(Context c) => _context = c;
		readonly Context _context;
	}

}

namespace Entitas
{
	public class EntityQueryAll<T1> : EntityQuery where T1:IComponent 
	{
		internal EntityQueryAll(Context c) : base(c,false) => make(inc<T1>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAll<T1,T2> : EntityQuery where T1:IComponent where T2:IComponent 
	{
		internal EntityQueryAll(Context c) : base(c,false) => make(inc<T1>(),inc<T2>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAll<T1,T2,T3> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent 
	{
		internal EntityQueryAll(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAll<T1,T2,T3,T4> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent 
	{
		internal EntityQueryAll(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAll<T1,T2,T3,T4,T5> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent 
	{
		internal EntityQueryAll(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAll<T1,T2,T3,T4,T5,T6> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent where T6:IComponent 
	{
		internal EntityQueryAll(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),inc<T6>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),inc<T6>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,false) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),inc<T6>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAny<T1> : EntityQuery where T1:IComponent 
	{
		internal EntityQueryAny(Context c) : base(c,true) => make(inc<T1>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAny<T1,T2> : EntityQuery where T1:IComponent where T2:IComponent 
	{
		internal EntityQueryAny(Context c) : base(c,true) => make(inc<T1>(),inc<T2>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAny<T1,T2,T3> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent 
	{
		internal EntityQueryAny(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAny<T1,T2,T3,T4> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent 
	{
		internal EntityQueryAny(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAny<T1,T2,T3,T4,T5> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent 
	{
		internal EntityQueryAny(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),outc<X1>(),outc<X2>());
		}
	}

	public class EntityQueryAny<T1,T2,T3,T4,T5,T6> : EntityQuery where T1:IComponent where T2:IComponent where T3:IComponent where T4:IComponent where T5:IComponent where T6:IComponent 
	{
		internal EntityQueryAny(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),inc<T6>());

		public class Without<X1> : EntityQuery where X1:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),inc<T6>(),outc<X1>());
		}
		public class Without<X1,X2> : EntityQuery where X1:IComponent where X2:IComponent {
			internal Without(Context c) : base(c,true) => make(inc<T1>(),inc<T2>(),inc<T3>(),inc<T4>(),inc<T5>(),inc<T6>(),outc<X1>(),outc<X2>());
		}
	}

}

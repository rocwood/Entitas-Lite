# Entitas-Lite

Entitas-Lite is a **No-CodeGenerator** branch of Entitas.<br/>
It's suitable for large projects and teams prefer ECS without code-generation.<br/> 
Some core of Entitas was rewritten to provide easy interface for hand-coding.

## Getting Start
Download and extract "Build/deploy/Entitas-Lite" folder into your Unity Project/Assets/.<br/>
Just write your own Components and Systems, then a GameController class for game entry.<br/>
**No CodeGenerator** required! Have fun!

## Example 1

```csharp
[Default]
public class PositionComponent : IComponent
{
	public int x;
	public int y;
}

// if no context declaration, it comes into Default context
public class VelocityComponent : IComponent
{
	public int x;
	public int y;

	// don't be afraid of writing helper accessor
	public void SetValue(int nx, int ny)
	{
		x = nx;
		y = ny;
	}
}

// if no feature-set declaration, it comes into UnnamedFeature
public class MovementSystem : IExecuteSystem
{
	public void Execute()
	{
		// new API for getting group with all matched entities from context
		var entities = Context<Default>.AllOf<PositionComponent, VelocityComponent>().GetEntities();

		foreach (var e in entities)
		{
			var vel = e.Get<VelocityComponent>();
			var pos = e.Modify<PositionComponent>(); // new API for trigger Monitor/ReactiveSystem

			pos.x += vel.x;
			pos.y += vel.y;
		}
	}
}

// Sample view just display Entity's Position if changed
public class ViewSystem : ReactiveSystem
{
	public ViewSystem()
	{
		// new API, add monitor that watch Position changed and call Process 
		monitors += Context<Default>.AllOf<PositionComponent>().OnAdded(Process);
	}

	protected void Process(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			var pos = e.Get<PositionComponent>();
			Debug.Log("Entity" + e.creationIndex + ": x=" + pos.x + " y=" + pos.y);
		}
	}
}

public class GameController : MonoBehaviour
{
	private Systems _feature;

	public void Start()
	{
		var contexts = Contexts.sharedInstance;

#if UNITY_EDITOR
		ContextObserverHelper.ObserveAll(contexts);
#endif

		// create random entity
		var rand = new System.Random();
		var context = Contexts.Default;
		var e = context.CreateEntity();
		    e.Add<PositionComponent>();
		    e.Add<VelocityComponent>().SetValue(rand.Next()%10, rand.Next()%10);

#if UNITY_EDITOR
		_feature = FeatureObserverHelper.CreateFeature(null);
#else
		// init systems, auto collect matched systems, no manual Systems.Add(ISystem) required
		_feature = new Feature(null);
#endif
		_feature.Initialize();
	}

	public void Update()
	{
		_feature.Execute();
		_feature.Cleanup();
	}
}
```


## Improvement & NewAPI beyond Entitas

* Entity, Context, Contexts, Matcher, Feature are now just one class. <br/>
No more generated GameEntity, GameContext, GameMatcher, InputEntity ...<br/>
However, another generic helpers were added for easy hand-coding.

* Feature: Auto add matched Systems, no manual Systems.Add(ISystem) required

* Scoping: Subclassing from ContextAttribute for Components, and FeatureAttribute for Systems.

```csharp
public class Game : ContextAttribute {}
public class MyFeature : FeatureAttribute { public MyFeature(int prior = 0) :base(prior) {} }

[Game] public class MyComponent : IComponent {}
[MyFeature] public class MySystem : IExecuteSystem {}
```

* Entity: Generic API for Add/Replace/Get/RemoveComponents. Forget component-index!

```csharp
e.Add<PositionComponent>();	// equals to e.AddComponent<PositionComponent>();
e.Remove<PositionComponent>();	// equals to e.RemoveComponent<PositionComponent>();
var vel = e.Get<VelocityComponent>();
var pos = e.Modify<PositionComponent>();  // get component for modification, will trigger Monitor/ReactiveSystem
```

* Context: Auto register all components with the same ContextAttribute. 

```csharp
var e1 = context.GetEntity(100);	// get entity with creationIndex==100

public class UserComponent : IUniqueComponent {}	// mark this component unique in context
var e2 = context.GetSingleEntity<UserComponent>();	// whill raise exception if not unique
var user = context.GetUnique<UserComponent>();		// directly fetch unique component
var user2 = context.ModifyUnique<UserComponent>();
var user3 = context.AddUnique<UserComponent>();
```

* Contexts: Auto register all context. Add name-Context lookup and a defaultContext for notitled Components.

```csharp
Context c = Contexts.Default;  // = Contexts.sharedInstance.defaultContext;
Context game = Contexts.Get("Game");  // = Contexts.sharedInstance.GetContext("Game");
Context input = Contexts.Get<Input>(); // = Contexts.sharedInstance.GetContext<Input>();
```

* Matcher: Generic templates for easy Matcher creation

```csharp
var matcher = Match<Game>.AllOf<PositionComponent, VelocityComponent>();
var group = Context<Game>.AllOf<PositionComponent, VelocityComponent>(); // easy combin context.GetGroup(matcher)
```

* Monitor/Collector: Monitor combins collector/filter/processor for Reactive-programming

```csharp
var monitor = Context<Default>.AllOf<PositionComponent>() // group => monitor
		.OnAdded(entities => { foreach (var e in entities) { /* do something */ }})
		.where(e => e.Has<ViewComponent>); // filter

monitor.Execute(); // in each update
```

* ReactiveSystem: brand-new usage by Monitor, allow multi monitors 

```csharp
public class ViewSystem : ReactiveSystem {
	public ViewSystem() {
		// use += to add more monitors to Execute
		monitors += Context<Default>.AllOf<PositionComponent>().OnAdded(this.Process);  
	}
}
```

* new ExecuteSystem: simplify IExecuteSystem for subclass

```csharp
constructor ExecuteSystem(Context context, IMatcher<Entity> matcher) // requre both context and matcher
abstract void Execute(Entity entity) // override this for executing on each matched entity
```


## TODO

* Bug fixes
* Performance tweak
* Clean document
* More examples
 

# Entitas-Lite

Entitas-Lite is a **No-CodeGenerator** edition of Entitas.
We rewrote some core of Entitas-CSharp, and provide easy interface for hand-coding.


## Getting Start
Download and extract "Build/deploy/Entitas-Lite" folder into your Unity Project/Assets/.<br>
Just write your own Components and Systems, then a GameController class for game entry.<br/>
**No CodeGenerator** required! Have fun!

Get Entitas-Lite  : https://github.com/rocwood/Entitas-Lite  <br>


## Example 1

```csharp
using System;
using Entitas;

[Default]  
public class PositionComponent : IComponent {
  public int x;
  public int y;
}

// if no context declaration, it comes into Default context
public class VelocityComponent : IComponent {
  public int x;
  public int y;

  // don't be afraid of writing helper accessor
  public void SetValue(int nx, int ny) { x = nx; y = ny; }
}

// if no feature-set declaration, it comes into UnnamedFeature
public class MovementSystem : IExecuteSystem {
  public void Execute() {

    // new API for getting all matched entities from context
    var entities = Context<Default>.AllOf<PositionComponent, VelocityComponent>();
	
	foreach (var e in entities) {
	  var vel = e.Get<VelocityComponent>();
	  var pos = e.Modify<PositionComponent>();  // new API for trigger Monitor/ReactiveSystem
	  
	  pos.x += vel.x;
	  pos.y += vel.y;
	}
  }
}

// Sample view just display Entity's Position if changed
public class ViewSystem : ReactiveSystem {
  public ViewSystem() {
    // new API, add monitor that watch Position changed and call Process 
    monitors += Context<Default>.AllOf<PositionComponent>().OnAdded(this.Process);
  }

  void Process(List<Entity> entities) {
    foreach (var e in entities) {
      var pos = e.GetComponent<PositionComponent>();
      Console.WriteLine("Entity" + e.creationIndex + ": x=" + pos.x + " y=" + pos.y);
    }
  }
}

// Game Entry
public class GameController {
  private Systems _feature;

  public void Start() {
    var contexts = Contexts.sharedInstance;

#if UNITY_EDITOR
    ContextObserverHelper.ObserveAll(contexts); // Unity-only API
#endif

    // create random entity
    var rand = new Random();
    var context = Contexts.Default;
    var e = context.CreateEntity();
        e.Add<PositionComponent>();  // NewAPI
        e.Add<VelocityComponent>().SetValue(rand.Next()%10, rand.Next()%10);

    // init systems, auto collect matched systems, no manual Systems.Add(ISystem) required
#if UNITY_EDITOR
    _feature = new FeatureObserverHelper.Create();  // Unity-only API, shorter for Create("DefaultFeature")
#else
    _feature = new Feature(null);
#endif
    _feature.Initialize();
  }

  public void Update() {
    _feature.Execute();
    _feature.Cleanup();
  }
}
```



## Improvement & NewAPI beyond Entitas

* Entity, Context, Contexts, Matcher, Feature are now just one class.<br/>
No more generated GameEntity, GameContext, GameMatcher, InputEntity ...<br/>
Use new attributes like ContextScope, FeatureScope for scoping.
```
public class Game : ContextScope {
}
public class GameFeature : FeatureScope { 
	public GameFeature(int prior = 0) : base("GameFeature", prior) { }
}

[Game]
public class MyComponent : IComponent {}

[GameFeature]
public class MySystem : IExecuteSystem {}
```


* Entity: Generic API for Add/Replace/Get/RemoveComponents. Forget component-index!
```
e.AddComponent<PositionComponent>();
e.RemoveComponent<PositionComponent>();
var vel = e.GetComponent<VelocityComponent>();
var pos = e.Get<PositionComponent>();  // shorter API
```


* Context: Auto register all components. Add API for getting entity with creationIndex.
```
Entity e = context.GetEntity(100);	
```


* Contexts: Auto register all context. Add name-Context lookup and a defaultContext for notitled Components.
```
Context c = Contexts.Default;  // = Contexts.sharedInstance.defaultContext;
Context game = Contexts.Get("Game");  // = Contexts.sharedInstance.GetContext("Game");
Context input = Contexts.Get<Input>(); // = Contexts.sharedInstance.GetContext<Input>();
```


* Matcher: Generic templates for easy Matcher creation
```
var matcher = Match<Game>.AllOf<PositionComponent, VelocityComponent>();
var matcher2 = MatchDefault.AllOf<PositionComponent, VelocityComponent>();  // shorter for Match<DefaultContext>
```


* Feature: Auto add matched Systems, no manual Systems.Add(ISystem) required

* new ReactiveSystem: simplify ReactiveSystem<Entity> for subclass
```
constructor ReactiveSystem(ICollector<Entity> collector) // only accept Collector, no need to override GetTrigger()
bool Filter(Entity entity) // return true by default, not override required
```

* new ExecuteSystem: simplify IExecuteSystem for subclass
```
constructor ExecuteSystem(Context context, IMatcher<Entity> matcher) // requre both context and matcher
abstract void Execute(Entity entity) // override this for executing on each matched entity
```


## TODO

* Performance tweak
* Clean document
* More examples
 

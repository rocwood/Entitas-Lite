# Entitas-Lite

Entitas-Lite is a helper extension of Entitas.<br>
We focusses on easy development **WITHOUT** CodeGenerator.

AFAIK, CodeGenerator is less efficiency for large projects and teams. <br>So why not keep it simple and easy?



## Getting Start
Download and extract "Entitas-Lite" and "Entitas" folder into your Unity Project/Assets/.<br>
Just write your own Components and Systems, then a GameController class for game entry.<br/>
**No** CodeGenerator required! Have fun!

Get Entitas-Lite  : https://github.com/rocwood/Entitas-Lite  <br>
Get Entitas : https://github.com/sschmid/Entitas-CSharp



## Example 1

```csharp
using System;
using Entitas;

public class PositionComponent : IComponent {
  public int x;
  public int y;
}

// default with [DefaultContext]
public class VelocityComponent : IComponent {
  public int x;
  public int y;

  // don't be afraid of writing helper accessor
  public void SetValue(int nx, int ny) { x = nx; y = ny; }  
}

// Move each Entity's Position with Velocity
public class MovementSystem : IExecuteSystem {
  public void Execute() {
    var context = Contexts.Default; // NewAPI
    
    var entities = context.GetEntities(
        MatchDefault.AllOf<PositionComponent, VelocityComponent>()); // NewAPI
    
    foreach (var e in entities) {
      var pos = e.Get<PositionComponent>(); // NewAPI
      var vel = e.Get<VelocityComponent>();
      pos.x += vel.x;
      pos.y += vel.y;

      e.MarkUpdated<PositionComponent>(); // NewAPI, trigger event and ReactiveSystem
    }
  }
}

// Sample view just display Entity's Position if changed
public class ViewSystem : ReactiveSystem
{
  // constructor now only accepts collector
  public ViewSystem() 
    : base(Contexts.Default.CreateCollector(MatchDefault.AllOf<PositionComponent>()))
  {}

  protected override void Execute(List<Entity> entities)
  {
    foreach (var e in entities)
    {
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

    // create random entity
    var rand = new Random();
    var context = Contexts.Default;
    var e = context.CreateEntity();
        e.AddComponent<PositionComponent>();  // NewAPI
        e.AddComponent<VelocityComponent>().SetValue(rand.Next()%10, rand.Next()%10);

    // init systems
    _feature = new Feature(); // NewAPI, no manual Systems.Add(ISystem) required
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
Use new attributes like ContextScope, FeatureScope for scorping.


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




## TODO

* Performance tweak
* Clean document
* More examples
 

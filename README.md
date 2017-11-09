# Entitas-Lite

Entitas-Lite is a helper extension of Entitas (ECS framework for c# and Unity).<br>
Entitas-Lite focusses on easy development **WITHOUT** CodeGenerator of original Entitas.

CodeGenerator is less efficiency for large projects and teams. <br>Why not keep it simple and easy?

---

### Getting Start
Download and extract "Entitas-Lite" and "Entitas" folder into your Unity Project/Assets/.<br>
Just write your own Components and Systems, then a GameController class for game entry.<br/>
**No** CodeGenerator required! Have fun!

Get Entitas-Lite  : https://github.com/rocwood/Entitas-Lite  <br>
Get Entitas : https://github.com/sschmid/Entitas-CSharp

---

### Example 1

```csharp
using System;
using Entitas;

public class PositionComponent : IComponent {
  public int x;
  public int y;
}

public class VelocityComponent : IComponent {
  public int x;
  public int y;

  // don't be afraid of writing helper accessor
  public void SetValue(int nx, int ny) { x = nx; y = ny; }  
}

// Move each Entity's Position with Velocity
public class MovementSystem : IExecuteSystem {
  public void Execute() {
    var context = Contexts.sharedInstance.defaultContext; // NewAPI
    
    var entities = context.GetEntities(
        Matcher.AllOf<PositionComponent, VelocityComponent>()); // NewAPI
    foreach (var e in entities) {
      var pos = e.GetComponent<PositionComponent>(); // NewAPI
      var vel = e.GetComponent<VelocityComponent>();
      pos.x += vel.x;
      pos.y += vel.y;
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
    var context = Contexts.sharedInstance.defaultContext;
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

---

### Improvement & NewAPI beyond Entitas

* Entity, Context, Contexts, Matcher, Feature are now just one class.<br/>
No more generated GameEntity, GameContext, GameMatcher, InputEntity ... 


* Entity: Generic API for Add/Replace/Get/RemoveComponents. Forget component-index!
```
e.AddComponent<PositionComponent>();
e.RemoveComponent<PositionComponent>();
var vel = e.GetComponent<VelocityComponent>();
```  


* Context: Auto register all components. Add API for getting entity with creationIndex.
```
Entity e = context.GetEntityByCreationIndex(100);
```


* Contexts: Auto register all context. Add name-Context lookup and a defaultContext for notitled Components.
```
Context c = Contexts.sharedInstance.defaultContext;
Context game = Contexts.sharedInstance["Game"];
```


* Matcher: Generic templates for easy Matcher creation
```
var matcher = Matcher.AllOf<PositionComponent, VelocityComponent>();
```  


* Feature: Auto add matched Systems, no manual Systems.Add(ISystem) required


* ComponentInfo, ComponentInfoManager: new classes for automatical handle for Component->Index mapping.
```
int pos_index = ComponentInfo<PositionComponent>.index; // cached and fast
Context pos_context = ComponentInfo<PositionComponent>.context;
var vel_info = ComponentInfoManager.GetComponentInfo<VelocityComponent>();
```


---

### TODO

* Performance tweak
* Clean document
* More examples
 

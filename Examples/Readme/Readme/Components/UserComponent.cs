using Entitas;

[Game]
public sealed class UserComponent : IComponent, IUnique {

    public string name;
    public int age;
}

namespace Entitas {

    public interface IAllOfMatcher : IAnyOfMatcher {

        IAnyOfMatcher AnyOf(params int[] indices);
        IAnyOfMatcher AnyOf(params IMatcher[] matchers);
    }
}

namespace Entitas {

    public interface IAnyOfMatcher : INoneOfMatcher {

        INoneOfMatcher NoneOf(params int[] indices);
        INoneOfMatcher NoneOf(params IMatcher[] matchers);
    }
}

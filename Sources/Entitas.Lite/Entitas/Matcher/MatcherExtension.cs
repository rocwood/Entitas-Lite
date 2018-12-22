namespace Entitas {

    public static class MatcherExtension {

		public static U SetComponentNames<U>(this U matcher, string[] componentNames) where U : IMatcher {
			var m = matcher as Matcher;
			if (m != null)
				m.componentNames = componentNames;

			return matcher;
		}
	}
}

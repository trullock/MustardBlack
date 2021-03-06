

namespace MustardBlack.Tests.UrlSpecs
{
    public class RelativePathString : Specification
    {
        private Url uri;

        protected override void When()
        {
			this.uri = new Url("/some/relative/path");
        }

        [Then]
        public void ShoudlToStringProperly()
        {
	        this.uri.ToString().ShouldEqual("/some/relative/path");
        }
    }
}
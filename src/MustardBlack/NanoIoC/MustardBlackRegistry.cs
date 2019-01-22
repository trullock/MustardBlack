using MustardBlack.TempData;
using NanoIoC;

namespace MustardBlack.NanoIoC
{
	public class MustardBlackRegistry : IContainerRegistry
	{
		public void Register(IContainer container)
		{
			container.Register<ITempDataMechanism, CookieTempDataMechanism>();
		}
	}
}

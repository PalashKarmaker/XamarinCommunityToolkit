using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views.Options
{
	public class SnackBarOptions : ToastOptions
	{
		/// <summary>
		/// Action options
		/// </summary>
		public IEnumerable<SnackBarActionOptions> Actions { get; set; } = DefaultActions;

		public static IEnumerable<SnackBarActionOptions> DefaultActions { get; set; } = Enumerable.Empty<SnackBarActionOptions>();
	}
}
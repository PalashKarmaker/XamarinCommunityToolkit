using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.Sample.Pages.Views
{
	public partial class SnackBarPage : BasePage
	{
		public SnackBarPage() => InitializeComponent();

		async void DisplaySnackBarClicked(object? sender, EventArgs args)
		{
			var result = await this.DisplaySnackBarAsync(GenerateLongText(5), "Run action", () =>
			{
				Debug.WriteLine("SnackBar action button clicked");
				return Task.CompletedTask;
			});
			StatusText.Text = result ? "SnackBar is closed by user" : "SnackBar is closed by timeout";
		}

		async void DisplaySnackBarWithPadding(object? sender, EventArgs args)
		{
			var options = new SnackBarOptions()
			{
				BackgroundColor = Color.FromHex("#CC0000"),
				MessageOptions = new MessageOptions
				{
					Message = "msg",
					Foreground = Color.White,
					Font = Font.SystemFontOfSize(16),
					Padding = new Thickness(10, 20, 30, 40)
				}
			};

			await this.DisplaySnackBarAsync(options);
		}

		async void DisplayToastClicked(object? sender, EventArgs args)
		{
			await this.DisplayToastAsync(GenerateLongText(5));
			StatusText.Text = "Toast is closed by timeout";
		}

		async void DisplaySnackBarAdvancedClicked(object? sender, EventArgs args)
		{
			const string SmileIcon = "\uf118";
			var options = new SnackBarOptions
			{
				MessageOptions = new MessageOptions
				{
					Foreground = Color.DeepSkyBlue,
					Font = Font.OfSize("FARegular", 40),
					Padding = new Thickness(10, 20, 30, 40),
					Message = SmileIcon
				},
				Duration = TimeSpan.FromMilliseconds(5000),
				BackgroundColor = Color.Coral,
				IsRtl = CultureInfo.CurrentCulture.TextInfo.IsRightToLeft,
				Actions = new List<SnackBarActionOptions>
				{
					new SnackBarActionOptions
					{
						ForegroundColor = Color.Red,
						BackgroundColor = Color.Green,
						Font = Font.OfSize("Times New Roman", 15),
						Padding = new Thickness(10, 20, 30, 40),
						Text = "Action1",
						Action = () =>
						{
							Debug.WriteLine("1");
							return Task.CompletedTask;
						}
					},
					new SnackBarActionOptions
					{
						ForegroundColor = Color.Green,
						BackgroundColor = Color.Red,
						Font = Font.OfSize("Times New Roman", 20),
						Padding = new Thickness(40, 30, 20, 10),
						Text = "Action2",
						Action = () =>
						{
							Debug.WriteLine("2");
							return Task.CompletedTask;
						}
					}
				}
			};
			var result = await this.DisplaySnackBarAsync(options);
			StatusText.Text = result ? "SnackBar is closed by user" : "SnackBar is closed by timeout";
		}

		string GenerateLongText(int stringDuplicationTimes)
		{
			const string message = "It is a very long message to test multiple strings. A B C D E F G H I I J K L M N O P Q R S T U V W X Y Z";
			var result = new StringBuilder();
			for (var i = 0; i < stringDuplicationTimes; i++)
			{
				result.AppendLine(message);
			}

			return result.ToString();
		}
	}
}
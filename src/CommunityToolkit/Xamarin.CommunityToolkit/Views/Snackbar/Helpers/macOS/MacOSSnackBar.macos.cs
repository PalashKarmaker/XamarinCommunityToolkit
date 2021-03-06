using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Xamarin.CommunityToolkit.UI.Views.Helpers.macOS.SnackBarViews;
using Xamarin.CommunityToolkit.Views.Snackbar.Helpers;

namespace Xamarin.CommunityToolkit.UI.Views.Helpers.macOS
{
	class NativeSnackBar
	{
		NSTimer? timer;

		public Func<Task>? TimeoutAction { get; protected set; }

		public List<NativeSnackButton> Actions { get; protected set; } = new List<NativeSnackButton>();

		public NativeSnackBarAppearance Appearance { get; protected set; } = new NativeSnackBarAppearance();

		public double Duration { get; protected set; }

		public SnackBarLayout Layout { get; } = new SnackBarLayout();

		public string Message { get; protected set; } = string.Empty;

		protected BaseSnackBarView? SnackBarView { get; set; }

		public void Dismiss()
		{
			if (timer != null)
			{
				timer.Invalidate();
				timer.Dispose();
				timer = null;
			}

			SnackBarView?.Dismiss();
		}

		public static NativeSnackBar MakeSnackBar(string message)
		{
			var snackbar = new NativeSnackBar { Message = message };
			return snackbar;
		}

		public NativeSnackBar SetTimeoutAction(Func<Task> action)
		{
			TimeoutAction = action;
			return this;
		}

		public NativeSnackBar SetDuration(double duration)
		{
			Duration = duration;
			return this;
		}

		public NativeSnackBar Show()
		{
			SnackBarView = GetSnackBarView();

			SnackBarView.ParentView.AddSubview(SnackBarView);

			SnackBarView.Setup();

			timer = NSTimer.CreateScheduledTimer(TimeSpan.FromMilliseconds(Duration), async t =>
			{
				if (TimeoutAction != null)
					await TimeoutAction();
				Dismiss();
			});

			return this;
		}

		BaseSnackBarView GetSnackBarView()
		{
			if (Actions.Count() > 0)
			{
				return new ActionMessageSnackBarView(this);
			}

			return new MessageSnackBarView(this);
		}
	}
}
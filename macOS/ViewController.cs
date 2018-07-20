using System;

using AppKit;
using Foundation;
using QuickLookUI;

namespace DocumentInteraction.macOS
{
	public partial class ViewController : NSViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Do any additional setup after loading the view.

			PreviewBtn.Activated += (sender, e) =>
			{
				/*
				 * Applications have a shared QLPreviewPanel instance: 
				 * https://developer.apple.com/reference/quartz/qlpreviewpanel
				 */

				QLPreviewPanel.SharedPreviewPanel().MakeKeyAndOrderFront(null);

			};
		}

		#region QuickLook implementation

		/*
		 * Implementing these methods will make this ViewController
		 * class responsible for managing the preview panel since 
		 * the QLPreviewPanel will look for the first responder 
		 * that accepts control.
		 * 
		 * 
		 * The methods must be manually exported due to a possible issue
		 * with the current bindings: https://bugzilla.xamarin.com/show_bug.cgi?id=34114
		 */

		[Export("acceptsPreviewPanelControl:")]
		public bool AcceptsPreviewPanelControl(QLPreviewPanel panel)
		{
			Console.WriteLine("AcceptsPreviewPanelControl");
			return true;
		}

		[Export("beginPreviewPanelControl:")]
		public void BeginPreviewPanelControl(QLPreviewPanel panel)
		{
			Console.WriteLine("BeginPreviewPanelControl");
			panel.DataSource = new QuickLookSource();
		}

		[Export("endPreviewPanelControl:")]
		public void EndPreviewPanelControl(QLPreviewPanel panel)
		{
			Console.WriteLine("EndPreviewPanelControl");
			panel.Delegate = null;
			panel.DataSource = null;
		}

		#endregion
	}
}

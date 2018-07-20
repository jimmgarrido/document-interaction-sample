using System;

using Foundation;
using AppKit;
using QuickLookUI;

namespace DocumentInteraction.macOS
{
	public partial class QuickLookSource : QLPreviewPanelDataSource
	{
		public static string[] Documents = {
			"sampledocs/gettingstarted.pdf",
			"sampledocs/Xamagon.png"
		};

		public override nint NumberOfPreviewItemsInPreviewPanel(QLPreviewPanel panel)
		{
			return Documents.Length;
		}

		public override IQLPreviewItem PreviewItemAtIndex(QLPreviewPanel panel, nint index)
		{
			return new PreviewItem(index);
		}	

	}

	public class PreviewItem : QLPreviewItem
	{
		public override NSUrl PreviewItemURL
		{
			get
			{
				var filePath = QuickLookSource.Documents[itemIndex];
				return NSUrl.FromFilename(filePath);
			}
		}

		nint itemIndex;

		public PreviewItem(nint index)
		{
			itemIndex = index;
		}
	}
}

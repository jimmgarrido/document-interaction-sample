using System;
using System.Collections.Generic;
using Foundation;
using QuickLook;

namespace DocumentInteraction.iOS
{
    public class QuickLookSource : QLPreviewControllerDataSource
    {
        List<string> documents;

        public QuickLookSource(List<string> docs)
        {
            documents = docs;
        }

		public override nint PreviewItemCount(QLPreviewController controller)
        {
            return documents.Count;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return new PreviewItem(documents[(int)index]);
        }
    }

    public class PreviewItem : QLPreviewItem
    {
		NSUrl fileUrl;

		public override string ItemTitle => fileUrl.LastPathComponent;

		public override NSUrl ItemUrl => fileUrl;

		public PreviewItem(string url)
        {
			fileUrl = NSUrl.FromFilename(url);
        }
    }
}
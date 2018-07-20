using System;
using System.Collections.Generic;
using Foundation;
using QuickLook;

namespace DocumentInteraction.iOS
{
    public class QuickLook : QLPreviewControllerDelegate, IQLPreviewControllerDataSource
    {
        List<string> documents;

        public QuickLook(List<string> docs)
        {
            documents = docs;
        }

		public nint PreviewItemCount(QLPreviewController controller)
        {
            return documents.Count;
        }

        public IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return new PreviewItem(index, documents[(int)index]);
        }
    }

    public class PreviewItem : QLPreviewItem
    {
		nint itemIndex;
		NSUrl fileUrl;

		public override string ItemTitle
        {
            get
            {
				return fileUrl.LastPathComponent;
            }
        }

        public override NSUrl ItemUrl
        {
            get
            {
				return fileUrl;
            }
        }

        public PreviewItem(nint index, string url)
        {
            itemIndex = index;
			fileUrl = NSUrl.FromFilename(url);
        }
    }
}
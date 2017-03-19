using System;
using Foundation;
using QuickLook;

namespace DocumentInteraction.iOS
{
    public class QuickLook : QLPreviewControllerDelegate, IQLPreviewControllerDataSource
    {

		#region IQLPreviewControllerDataSource implementations

		public nint PreviewItemCount(QLPreviewController controller)
        {
            return TableSource.Documents.Length;
        }

        public IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return new PreviewItem(index);
        }

		#endregion
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

        public PreviewItem(nint index)
        {
            itemIndex = index;
			fileUrl = NSUrl.FromFilename(TableSource.Documents[itemIndex]);
        }
    }
}


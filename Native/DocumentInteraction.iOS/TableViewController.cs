using Foundation;
using System;
using UIKit;
using QuickLook;

namespace DocumentInteraction.iOS
{
    public partial class TableViewController : UITableViewController, IUITableViewDelegate
    {
        public TableViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			TableView.Source = new TableSource();
			TableView.Delegate = this;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				var sourceDelegate = new QuickLook();

				var previewController = new QLPreviewController();
				previewController.Delegate = sourceDelegate;
				previewController.DataSource = sourceDelegate;

				previewController.CurrentPreviewItemIndex = indexPath.Row;
				NavigationController.PushViewController(previewController, true);
			}
			else
			{
				var previewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(TableSource.Documents[indexPath.Row]));
				previewController.Delegate = new InteractionDelegate(this);
				previewController.PresentPreview(true);
			}
		}
    }
}
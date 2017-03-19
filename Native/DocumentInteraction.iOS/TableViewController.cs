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
				var previewController = new QLPreviewController();
				var sourceDelegate = new QuickLook();

				previewController.Delegate = sourceDelegate;
				previewController.DataSource = sourceDelegate;

				previewController.CurrentPreviewItemIndex = indexPath.Row;
				NavigationController.PushViewController(previewController, true);

				// You can present modally instead
				//
				// PresentViewController(previewController, true, null); 
			}
			else
			{
				var previewController = UIDocumentInteractionController.FromUrl(
					NSUrl.FromFilename(TableSource.Documents[indexPath.Row]));
				previewController.Delegate = new InteractionDelegate(this);
				previewController.PresentPreview(true);


				// You can present other options for the file instead of a preview
				//
				// previewController.PresentOptionsMenu(TableView.Frame, TableView, true);
				// previewController.PresentOpenInMenu(TableView.Frame, TableView, true);
			}
		}
    }
}
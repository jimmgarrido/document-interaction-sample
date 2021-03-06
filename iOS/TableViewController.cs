using Foundation;
using System;
using UIKit;
using QuickLook;
using System.Collections.Generic;

namespace DocumentInteraction.iOS
{
    public partial class TableViewController : UITableViewController, IUITableViewDelegate
    {
        TableSource source;

        public TableViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NavigationController.NavigationBar.PrefersLargeTitles = true;

            source = new TableSource();
            TableView.Source = source;

			TableView.Delegate = this;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				var previewController = UIDocumentInteractionController.FromUrl(
					NSUrl.FromFilename(source.Documents[indexPath.Row]));

				previewController.Delegate = new MyInteractionDelegate(this);
				previewController.PresentPreview(true);


				// You can present other options for the file instead of a preview
				//
				 //previewController.PresentOptionsMenu(TableView.Frame, TableView, true);
				 //previewController.PresentOpenInMenu(TableView.Frame, TableView, true);
			}
			else
			{
				var previewController = new QLPreviewController();
				previewController.DataSource = new QuickLookSource(source.Documents);

				previewController.CurrentPreviewItemIndex = indexPath.Row;
				NavigationController.PushViewController(previewController, true);

				// You can present modally instead
				//
				// PresentViewController(previewController, true, null); 
			}
		}
    }

    public class MyInteractionDelegate : UIDocumentInteractionControllerDelegate
    {
        UIViewController parent;

        public MyInteractionDelegate(UIViewController controller)
        {
            parent = controller;
        }

        public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
        {
            return parent;
        }
    }
}
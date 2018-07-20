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

            source = new TableSource();
            source.Documents = new List<string> {
                "sampledocs/gettingstarted.pdf",
                "sampledocs/Xamagon.png"
            };

            TableView.Source = source;
			TableView.Delegate = this;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				var previewController = new QLPreviewController();
				var sourceDelegate = new QuickLook(source.Documents);

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
					NSUrl.FromFilename(source.Documents[indexPath.Row]));
                previewController.Delegate = new MyInteractionDelegate(this);
                previewController.PresentPreview(true);


				// You can present other options for the file instead of a preview
				//
				// previewController.PresentOptionsMenu(TableView.Frame, TableView, true);
				// previewController.PresentOpenInMenu(TableView.Frame, TableView, true);
			}
		}
    }

    public class MyInteractionDelegate : UIDocumentInteractionControllerDelegate
    {
        UIViewController parentController;

        public MyInteractionDelegate(UIViewController controller)
        {
            parentController = controller;
        }

        public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
        {
            return parentController;
        }
    }
}
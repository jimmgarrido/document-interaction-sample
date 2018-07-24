using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DocumentInteraction.iOS
{
    public class TableSource : UITableViewSource
    {
		readonly string cellID = "1";

        public List<string> Documents { get; set; }

		public TableSource()
		{
			Documents = new List<string> {
				"sampledocs/gettingstarted.pdf",
				"sampledocs/Xamagon.png"
			};
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellID);

            if (cell == null)
            {
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellID)
				{
					Accessory = UITableViewCellAccessory.DisclosureIndicator
				};
			}

            var fileUrl = NSUrl.FromFilename(Documents[indexPath.Row]);
			cell.TextLabel.Text = fileUrl.LastPathComponent;

			return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
			return Documents.Count;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 2;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
			return section == 0 ? "Document Interaction Controller" : "Quick Look controller";
		}
    }
}
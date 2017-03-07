using System;
using UIKit;

namespace DocumentInteraction.iOS
{
	public partial class InteractionDelegate : UIDocumentInteractionControllerDelegate
	{
		UIViewController currentController;

		public InteractionDelegate(UIViewController controller)
		{
			currentController = controller;
		}

		public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
		{
			return currentController;
		}
	}
}


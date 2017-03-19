using System;
using UIKit;

namespace DocumentInteraction.iOS
{
	public class InteractionDelegate : UIDocumentInteractionControllerDelegate
	{
		UIViewController parentController;

		public InteractionDelegate(UIViewController controller)
		{
			parentController = controller;
		}

		public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
		{
			return parentController;
		}
	}
}


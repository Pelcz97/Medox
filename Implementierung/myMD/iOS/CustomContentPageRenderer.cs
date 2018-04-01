using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ProjectName.iOS.Renderers.CustomContentPageRenderer))]
namespace ProjectName.iOS.Renderers
{
    class CustomContentPageRenderer : PageRenderer
    {
        public new ContentPage Element
        {
            get { return (ContentPage)base.Element; }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ConfigureToolbarItems();
            ConfigureNavBar();
        }

        private void ConfigureToolbarItems()
        {
            if (NavigationController != null)
            {
                UINavigationItem navigationItem = NavigationController.TopViewController.NavigationItem;
                var orderedItems = Element.ToolbarItems.OrderBy(x => x.Priority);

                // add right side items
                var rightItems = orderedItems.Where(x => x.Priority >= 0).Select(x => x.ToUIBarButtonItem()).ToArray();
                navigationItem.SetRightBarButtonItems(rightItems, false);

                // add left side items, keep any already there
                var leftItems = orderedItems.Where(x => x.Priority < 0).Select(x => x.ToUIBarButtonItem()).ToArray();
                if (navigationItem.LeftBarButtonItems != null)
                    leftItems = navigationItem.LeftBarButtonItems.Union(leftItems).ToArray();
                navigationItem.SetLeftBarButtonItems(leftItems, false);
            }
        }

        private void ConfigureNavBar(){
            var toolbarPage = this.Element as ToolbarPage;
            var toolbarView = toolbarPage.ToolbarViewElement as VisualElement;

            var navigationItem = this.NavigationController?.TopViewController?.NavigationItem;
            if (navigationItem == null)
            {
                return;
            }

            if (toolbarView != null)
            {
                if (Platform.GetRenderer(toolbarView) == null)
                    Platform.SetRenderer(toolbarView, Platform.CreateRenderer(toolbarView));
                var vRenderer = Platform.GetRenderer(toolbarView);

                var size = new CGRect(0, 0, 200, 30);

                var nativeView = vRenderer.NativeView;
                var segmentedControl = nativeView as UISegmentedControl;
                nativeView.Frame = size;

                nativeView.AutoresizingMask = UIViewAutoresizing.All;
                nativeView.ContentMode = UIViewContentMode.ScaleToFill;

                vRenderer.Element.Layout(size.ToRectangle());

                navigationItem.TitleView = nativeView;

                nativeView.SetNeedsLayout();
            }

            if (toolbarPage.ToolbarHome != null)
            {
                navigationItem.LeftBarButtonItems = new UIBarButtonItem[] { toolbarPage.ToolbarHome.ToUIBarButtonItem() };
            }
        }
    }
}

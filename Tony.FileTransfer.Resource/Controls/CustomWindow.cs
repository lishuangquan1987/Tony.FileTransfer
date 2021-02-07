using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tony.FileTransfer.Resource.Controls
{
    public class CustomWindow:Window
    {
        Grid titleGrid;
        public CustomWindow()
        {
            string resourcePath = "pack://application:,,,/Tony.FileTransfer.Resource;component/Dark-Orange-Style.xaml";
            if (!Application.Current.Resources.MergedDictionaries.Any(x => x.Source.ToString() == resourcePath))
            {
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() {Source=new Uri(resourcePath, UriKind.RelativeOrAbsolute)});
            }
            this.Loaded += CustomWindow_Loaded;
        }

        private void CustomWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var style = this.FindResource("customWindowStyle") as Style;
            if (style != null)
            {
                this.Style = style;
            }

            this.OnApplyTemplate();

            if (this.WindowStyle == System.Windows.WindowStyle.None&&titleGrid!=null)
            {
                titleGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            titleGrid = this.Template.FindName("PART_TitleBar",this) as Grid;
            
        }
    }
}

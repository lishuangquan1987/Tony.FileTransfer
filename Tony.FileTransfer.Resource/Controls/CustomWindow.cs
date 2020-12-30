using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tony.FileTransfer.Resource.Controls
{
    public class CustomWindow:Window
    {
        Grid titleGrid;
        public CustomWindow()
        {
            this.Loaded += CustomWindow_Loaded;
        }

        private void CustomWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.OnApplyTemplate();

            if (this.WindowStyle == System.Windows.WindowStyle.None&&titleGrid!=null)
            {
                titleGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            titleGrid = GetTemplateChild("PART_TitleBar") as Grid;
            
        }
    }
}

namespace RefactoringExample.UiTests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

    public partial class UIMap
    {
        public bool ListContains(string value)
        {
            return this.UIMainWindowWindow.UIListBoxList.Items.Any(x => ((WpfListItem)x).DisplayText == value);
        }
    }
}

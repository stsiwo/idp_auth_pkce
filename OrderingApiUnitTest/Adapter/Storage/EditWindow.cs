using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Adapter.Storage
{
    class EditWindow
    {
        private IEnumerable<ToolBarButton> _toolBarButton; 

        public EditWindow(IEnumerable<ToolBarButton> toolBarButtons)
        {
            _toolBarButton = toolBarButtons;
        }

        public IList<string> Test()
        {
            IList<string> result = new List<string>(); 
            foreach (var button in _toolBarButton)
            {
                result.Add(button.Push());
            }
            return result;
        } 

    }
}

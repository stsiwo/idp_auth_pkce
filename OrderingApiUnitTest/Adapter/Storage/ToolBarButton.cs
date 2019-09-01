using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Adapter.Storage
{
    class ToolBarButton
    {
        private ICommand _command;

        public ToolBarButton(ICommand command)
        {
            _command = command;
        }

        public string Push()
        {
            return _command.Handle();
        }

    }
}

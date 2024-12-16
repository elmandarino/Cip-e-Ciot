using System.Data;
using System.Windows.Input;
using System;

namespace NetCoreClient.Sensors
{
    interface ICommand
    {
        void Execute();
    }
}
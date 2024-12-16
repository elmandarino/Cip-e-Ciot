using System.Data;
using System.Windows.Input;
using System;

namespace NetCoreClient.Sensors
{
    public class MyCommand : ICommand
    {

        
        public void Execute()
        {
            Console.WriteLine("Comando Eseguito, Luce Accesa");
        }
    }
}
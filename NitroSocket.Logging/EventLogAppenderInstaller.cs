using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Configuration.Install;

namespace Library.Logging
{
    [RunInstaller(true)]
    public partial class EventLogAppenderInstaller : Installer
    {
        private EventLogInstaller myEventLogInstaller;

        public EventLogAppenderInstaller()
		{
			//Create Instance of EventLogInstaller
			myEventLogInstaller = new EventLogInstaller();

			// Set the Source of Event Log, to be created.
            myEventLogInstaller.Source = "CORE";

			// Set the Log that source is created in
			myEventLogInstaller.Log = "Application";
			
			// Add myEventLogInstaller to the Installers Collection.
			Installers.Add(myEventLogInstaller);
		}
    }
}
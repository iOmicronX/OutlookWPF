using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using OutlookWPF.Core.RegionsAdapters;
using OutlookWPF.Modules.Contacts;
using OutlookWPF.Modules.Mail;
using OutlookWPF.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace OutlookWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MailModule>(); //let the prism executable that mailmodule is a
                                                   //module that we are gonna execute
            moduleCatalog.AddModule<ContactsModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            //crea una instanza di Xam[...]Bar e la inietta dove trova XamOutlookBar (credo?) --> mappa l'adapter
            regionAdapterMappings.RegisterMapping(typeof(XamOutlookBar), Container.Resolve<XamOutlookBarRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(XamRibbon), Container.Resolve<XamRibbonRegionAdapter>());
        }
    }
}

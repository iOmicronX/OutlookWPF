using Infragistics.Windows.OutlookBar;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookWPF.Core.RegionsAdapters
{
    public class XamOutlookBarRegionAdapter : RegionAdapterBase<XamOutlookBar>
    {
        public XamOutlookBarRegionAdapter(IRegionBehaviorFactory factory)
           : base(factory)
        {

        }

        protected override void Adapt(IRegion region, XamOutlookBar regionTarget) //dobbiamo adattare il controllo,
                                                                                  //devo prendere il controllo e aggiungerlo
                                                                                  //al componente
         //regionTarget = hosting element
         //Mi devo registrare all'evento CollectionChanged
        {
            region.Views.CollectionChanged += ((x, y) =>
            {
                switch (y.Action)
                {
                    //gestisco l'aggiunta di una view
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            //faccio il loop degli elementi che sto aggiungendo 
                            foreach (OutlookBarGroup group in y.NewItems)
                            {
                                //e li aggiungo
                                regionTarget.Groups.Add(group);

                                //The WPF XamOutlookBar does not automatically select the first group in it's collection.
                                //So we must manually select the group if it is the first one in the collection, but we don't
                                //want to excute this code every time a new group is added, only if the first group is the current group being added.
                                if (regionTarget.Groups[0] == group)
                                {
                                    regionTarget.SelectedGroup = group;
                                }
                            }
                            break;
                        }
                        //gestisco la rimozione di una view
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            foreach (OutlookBarGroup group in y.NewItems)
                            {
                                regionTarget.Groups.Remove(group);
                            }
                            break;
                        }
                }
            });
        }

        protected override IRegion CreateRegion() //mi permette di specificare con che tipo di region ho a che fare
        {
            return new SingleActiveRegion(); //tre tipi: Region(generico),SingleActiveRegion(tiene traccia
                                             //di che view è attiva in quella regione,
                                             //solo una tab alla volta --> LA PIU' COMUNE,
                                             //AllActiveRegion(tutte le regioni attive possono essere attive
        }
    }
}

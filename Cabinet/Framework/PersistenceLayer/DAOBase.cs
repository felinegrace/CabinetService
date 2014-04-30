using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.PersistenceLayer
{
    public class DAOBase
    {
        private CabinetTreeDataContext context { get; set; }
        protected DAOBase()
        {
            context = new CabinetTreeDataContext();
            context.Log = new Logger4LINQ();
        }

        protected void submit()
        {
            context.SubmitChanges();
        }

        protected Table<CabTree_Region> regions
        { 
            get
            {
                return context.CabTree_Regions;
            }
        }
        protected Table<CabTree_VolClass> volClasses
        {
            get
            {
                return context.CabTree_VolClasses;
            }
        }

        protected Table<CabTree_Eqptroom> eqptRooms
        {
            get
            {
                return context.CabTree_Eqptrooms;
            }
        }
    }
}

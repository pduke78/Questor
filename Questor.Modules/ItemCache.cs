﻿// ------------------------------------------------------------------------------
//   <copyright from='2010' to='2015' company='THEHACKERWITHIN.COM'>
//     Copyright (c) TheHackerWithin.COM. All Rights Reserved.
// 
//     Please look in the accompanying license.htm file for the license that 
//     applies to this source code. (a copy can also be found at: 
//     http://www.thehackerwithin.com/license.htm)
//   </copyright>
// -------------------------------------------------------------------------------
namespace Questor.Modules
{
    using DirectEve;

    public class ItemCache
    {
        private DirectItem _directItem;

        public ItemCache(DirectItem item)
        {
            _directItem = item;
        }

        public long Id
        {
            get { return _directItem.ItemId; }
        }

        public int TypeId
        {
            get { return _directItem.TypeId ?? 0; }
        }

        public int GroupID
        {
            get { return _directItem.GroupId ?? 0; }
        }

        public int Quantity
        {
            get { return _directItem.Quantity ?? 0; }
        }

        public bool IsContraband
        {
            get
            {
                if (GroupID == 313) return true; // Drugs
                if (GroupID == 282) return true; // Toxic Waste
                if (GroupID == 283) return true; // Slaves
                if (GroupID == 280) return true; // Small Arms
                return false;
            }
        }

        public string Name
        {
            get { return _directItem.Name; }
        }

        public double Volume
        {
            get { return _directItem.Volume ?? 0; }
        }

        public double TotalVolume
        {
            get { return InvType.Volume*Quantity; }
        }

        public InvType InvType
        {
            get
            {
                // Create a new InvType if its unknown
                if (!Cache.Instance.InvTypesById.ContainsKey(TypeId))
                {
                    Logging.Log("ItemCache: Unknown TypeID for [" + Name + "][" + TypeId + "]");
                    Cache.Instance.InvTypesById[TypeId] = new InvType(this);
                }

                return Cache.Instance.InvTypesById[TypeId];
            }
        }

        public double? IskPerM3
        {
            get
            {
                if (InvType.MedianBuy == null)
                    return null;

                return InvType.MedianBuy/InvType.Volume;
            }
        }
    }
}
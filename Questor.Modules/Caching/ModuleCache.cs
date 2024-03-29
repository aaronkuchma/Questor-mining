﻿// ------------------------------------------------------------------------------
//   <copyright from='2010' to='2015' company='THEHACKERWITHIN.COM'>
//     Copyright (c) TheHackerWithin.COM. All Rights Reserved.
//
//     Please look in the accompanying license.htm file for the license that
//     applies to this source code. (a copy can also be found at:
//     http://www.thehackerwithin.com/license.htm)
//   </copyright>
// -------------------------------------------------------------------------------

namespace Questor.Modules.Caching
{
    using System;
    using System.Collections.Generic;
    using DirectEve;
    using Questor.Modules.Lookup;

    public class ModuleCache
    {
        private readonly DirectModule _module;
        private double _reloadTimeThisMission;
        private DateTime _activatedTimeStamp;

        public ModuleCache(DirectModule module, double reloadTimeThisMission = 0, DateTime activatedTimeStamp = default(DateTime))
        {
            _module = module;
            _reloadTimeThisMission = reloadTimeThisMission;
            _activatedTimeStamp = activatedTimeStamp;
        }

        public double ReloadTimeThisMission
        {
            get { return _reloadTimeThisMission; }
            set
            {
                _reloadTimeThisMission = value;
                return;
            }
        }

        public DateTime ActivatedTimeStamp
        {
            get { return _activatedTimeStamp; }
            set
            {
                _activatedTimeStamp = value;
                return;
            }
        }

        public int TypeId
        {
            get { return _module.TypeId; }
        }

        public int GroupId
        {
            get { return _module.GroupId; }
        }

        public bool IsActivatable
        {
            get { return _module.IsActivatable; }
        }

        public long ItemId
        {
            get { return _module.ItemId; }
        }

        public bool IsActive
        {
            get { return _module.IsActive; }
        }

        public bool IsOnline
        {
            get { return _module.IsOnline; }
        }

        public bool IsGoingOnline
        {
            get { return _module.IsGoingOnline; }
        }

        public bool IsReloadingAmmo
        {
            get { return _module.IsReloadingAmmo; }
        }

        public bool IsDeactivating
        {
            get { return _module.IsDeactivating; }
        }

        public bool IsChangingAmmo
        {
            get { return _module.IsChangingAmmo; }
        }

        public bool IsTurret
        {
            get
            {
                if (GroupId == (int)Group.EnergyWeapon) return true;
                if (GroupId == (int)Group.ProjectileWeapon) return true;
                if (GroupId == (int)Group.HybridWeapon) return true;
                return false;
            }
        }

        public bool IsMissileLauncher
        {
            get
            {
                if (GroupId == (int)Group.AssaultMissilelaunchers) return true;
                if (GroupId == (int)Group.CruiseMissileLaunchers) return true;
                if (GroupId == (int)Group.TorpedoLaunchers) return true;
                if (GroupId == (int)Group.StandardMissileLaunchers) return true;
                if (GroupId == (int)Group.AssaultMissilelaunchers) return true;
                if (GroupId == (int)Group.HeavyMissilelaunchers) return true;
                if (GroupId == (int)Group.DefenderMissilelaunchers) return true;
                return false;
            }
        }

        public bool IsEnergyWeapon
        {
            get { return GroupId == (int)Group.EnergyWeapon; }
        }

        public long TargetId
        {
            get { return _module.TargetId ?? -1; }
        }

        public long LastTargetId
        {
            get
            {
                if (Cache.Instance.LastModuleTargetIDs.ContainsKey(ItemId))
                    return Cache.Instance.LastModuleTargetIDs[ItemId];

                return -1;
            }
        }

        public IEnumerable<DirectItem> MatchingAmmo
        {
            get { return _module.MatchingAmmo; }
        }

        public DirectItem Charge
        {
            get { return _module.Charge; }
        }

        public int CurrentCharges
        {
            get
            {
                if (_module.Charge != null)
                    return _module.Charge.Quantity;

                return -1;
            }
        }

        public int MaxCharges
        {
            get { return _module.MaxCharges; }
        }

        public double OptimalRange
        {
            get { return _module.OptimalRange ?? 0; }
        }

        public void ReloadAmmo(DirectItem charge)
        {
            _module.ReloadAmmo(charge);
        }

        public void ChangeAmmo(DirectItem charge)
        {
            _module.ChangeAmmo(charge);
        }

        public bool InLimboState
        {
            get
            {
                bool result = false;
                result |= !IsActivatable;
                result |= !IsOnline;
                result |= IsDeactivating;
                result |= IsGoingOnline;
                result |= IsReloadingAmmo;
                result |= IsChangingAmmo;
                return result;
            }
        }

        public void Click()
        {
            if (InLimboState)
                return;

            _module.Click();
        }

        public void Activate()
        {
            if (InLimboState || IsActive)
                return;

            _module.Activate();
        }

        public void Activate(long entityId)
        {
            if (InLimboState || IsActive)
                return;

            _module.Activate(entityId);

            Cache.Instance.LastModuleTargetIDs[ItemId] = entityId;
        }

        public void Deactivate()
        {
            if (InLimboState || !IsActive)
                return;

            _module.Deactivate();
        }
    }
}
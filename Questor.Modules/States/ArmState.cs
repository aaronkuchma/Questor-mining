﻿// ------------------------------------------------------------------------------
//   <copyright from='2010' to='2015' company='THEHACKERWITHIN.COM'>
//     Copyright (c) TheHackerWithin.COM. All Rights Reserved.
//
//     Please look in the accompanying license.htm file for the license that
//     applies to this source code. (a copy can also be found at:
//     http://www.thehackerwithin.com/license.htm)
//   </copyright>
// -------------------------------------------------------------------------------
namespace Questor.Modules.States
{
    public enum ArmState
    {
        Idle,
        Begin,
        OpenShipHangar,
        ActivateCombatShip,
        OpenAmmoHangar,
        OpenCargo,
        OpenDroneBay,
        MoveDrones,
        MoveItems,
        WaitForItems,
        Done,
        SwitchToSalvageShip,
        ActivateSalvageShip,
        SwitchToTransportShip,
        ActivateTransportShip,
        NotEnoughAmmo,
        NotEnoughDrones,
        WaitForFitting,
        OpenFittingWindow,
        WaitForFittingWindow,
        ChoseFitting,
        Cleanup
    }
}
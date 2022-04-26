using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.IncrimantalIdleSystem;
public class HammerUpgrader : StatObjectBase
{
    Rigidbody _rigidbody;

    Rigidbody Rigidbody { get { return (_rigidbody == null) ? _rigidbody = GetComponent<Rigidbody>() : _rigidbody; } }
   

    public override void UpdateStat(string id)
    {
        if (!string.Equals(StatData.IdleStatData.StatID, id))
            return;

        Rigidbody.mass = (float)StatData.CurrentValue;
    }


}

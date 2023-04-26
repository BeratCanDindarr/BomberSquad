using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamage
{
    public void Damage(float damage);
    public void CheckDeath(float health);
}

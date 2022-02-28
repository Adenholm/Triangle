using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElementAttack
{
    public void Attack(float power, LayerMask enemyLayers);
}

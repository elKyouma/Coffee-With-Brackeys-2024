using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public int keyId;
    public override void UseItem()
    {
        Destroy(gameObject);
    }
}

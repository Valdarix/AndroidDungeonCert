using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] private GameObject acidProjectile;

    public override void Update()
   {
     //stand still
   }

    public override void Attack()
    {
        var instantionPoint = sprite.transform;
        var acidShot = Instantiate(acidProjectile, instantionPoint.position, Quaternion.identity);
        acidShot.gameObject.transform.parent = instantionPoint;
    }
}

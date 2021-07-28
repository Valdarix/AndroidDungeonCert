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
        var instantiationPoint = sprite.transform;
        var acidShot = Instantiate(acidProjectile, instantiationPoint.position, Quaternion.identity);
        acidShot.gameObject.transform.parent = instantiationPoint;
    }
}

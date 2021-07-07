using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected int health;
    [SerializeField] protected int gems;

    public virtual void Attack()
    {
        // Do nothing for now. 
    }
    public abstract void Update();
}

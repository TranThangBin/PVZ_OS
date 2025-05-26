using UnityEngine;

public class PeashooterAttackState : StateMachine.State
{
    [SerializeField] private GameObject _projectile;

    public override string GetStateName()
    {
        return typeof(PeashooterAttackState).ToString();
    }

    public override void StateEnter()
    {
        GameObject gameObject = Instantiate(_projectile, transform.position + Vector3.right, Quaternion.identity);
        IProjectile projectile = gameObject.GetComponent<IProjectile>();
        projectile.Fire(Vector2.right);
        InvokeTransitionListener(this, typeof(PeashooterCooldownState).ToString());
    }
}

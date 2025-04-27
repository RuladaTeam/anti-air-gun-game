using UnityEngine;

public class BombsTrajectory : MovingObjectTraectory
{
    [Space(30), Header("Bombs Setting")]
    [SerializeField] private ParticleSystem _detonation;

    public void SpawnBombs()
    {
        MovingObjectOnParabola(movingObject.transform);
        Invoke(nameof(PlayParticles), parabolaDuration);
    }

    private void PlayParticles()
    {
        //_detonation.Play();
        Debug.Log("Detonation");
    }

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

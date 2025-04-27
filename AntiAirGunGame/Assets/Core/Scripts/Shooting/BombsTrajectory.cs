using UnityEngine;

public class BombsTrajectory : MovingObjectTraectory
{

    public void SpawnBombs()
    {
        MovingObjectOnParabola(movingObject.transform);
    }

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

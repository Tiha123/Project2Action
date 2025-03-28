using DungeonArchitect;
using UnityEngine;

public class DungeonTransformRuleRandom : TransformationRule
{
    public override void GetTransform(PropSocket socket, DungeonModel model, Matrix4x4 propTransform, System.Random random, out Vector3 outPosition, out Quaternion outRotation, out Vector3 outScale)
    {
        base.GetTransform(socket, model, propTransform, random, out outPosition, out outRotation, out outScale);
        outRotation = Quaternion.Euler(Random.rotation.eulerAngles);
    }
}

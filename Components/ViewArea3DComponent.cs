using Godot;
using System;

public partial class ViewArea3DComponent : Area3D
{
	PlayerVisuals ParentVisuals;
	bool mustPointFollowTarget;
	MeshInstance3D TargetPoint;
	Area3D Target;

	float HorizontalTargetAngle = 0.0f;
	float VerticalTargetAngle = 0.0f;

	public override void _Ready()
	{
		ParentVisuals = GetParent<CharacterBody3D>().GetNode<PlayerVisuals>("Visuals");
		TargetPoint = GetNode<MeshInstance3D>("TargetPointMeshInstance3D");
		AreaEntered += (area) => SetTargetPointToFollow(area);
		AreaExited += (node) => SetTargetPointToDefaultPosition();
	}

	public override void _Process(double delta)
	{
		if ( mustPointFollowTarget )
		{
			HorizontalTargetAngle = Mathf.Atan2( TargetPoint.GlobalPosition.Z - GlobalPosition.Z, GlobalPosition.X - TargetPoint.GlobalPosition.X ) + Mathf.Pi/2 - Rotation.Y;
			VerticalTargetAngle = -Mathf.Atan2( TargetPoint.GlobalPosition.Z - GlobalPosition.Z, GlobalPosition.Y - TargetPoint.GlobalPosition.Y ) - Mathf.Pi/2;
			ParentVisuals.Skeleton.SetBonePoseRotation(5, new Quaternion( -VerticalTargetAngle, HorizontalTargetAngle, 0, 1));
		}
	}

    public override void _PhysicsProcess(double delta)
    {
        if(mustPointFollowTarget)
		{
			TargetPoint.GlobalPosition = Target.GlobalPosition;
		}
		Rotation = ParentVisuals.Rotation;
    }

	private void SetTargetPointToFollow( Area3D area )
	{
		mustPointFollowTarget = true;
		Target = area;
	}

	private void SetTargetPointToDefaultPosition()
	{
		TargetPoint.Position = new Vector3(0,0,-4);
		mustPointFollowTarget = false;
	}
}

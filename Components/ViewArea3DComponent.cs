using Godot;
using System;

public partial class ViewArea3DComponent : Area3D
{
	Node3D ParentVisuals;
	bool mustPointFollowTarget;
	MeshInstance3D TargetPoint;
	Area3D Target;

	public override void _Ready()
	{
		ParentVisuals = GetParent<CharacterBody3D>().GetNode<Node3D>("Visuals");
		TargetPoint = GetNode<MeshInstance3D>("TargetPointMeshInstance3D");
		AreaEntered += (area) => SetTargetPointToFollow(area);
		AreaExited += (node) => SetTargetPointToDefaultPosition();
	}

	public override void _Process(double delta)
	{
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

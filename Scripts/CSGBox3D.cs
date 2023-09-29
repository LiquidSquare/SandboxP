using Godot;
using System;

public partial class CSGBox3D : CsgBox3D
{
	float HorizontalTargetAngleInDegress = 0.0f;
	float VerticalTargetAngleInDegress = 0.0f;
	float HorizontalTargetAngle = 0.0f;
	float VerticalTargetAngle = 0.0f;
	float RotationSpeed = 10f;
	Node3D TargetNode;

	public override void _Ready()
	{
		TargetNode = GetNode<Node3D>("%Enemy");
	}

	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        HorizontalTargetAngle = Mathf.Atan2( TargetNode.Position.Z - GlobalPosition.Z, GlobalPosition.X - TargetNode.Position.X );
		HorizontalTargetAngleInDegress = (Mathf.RadToDeg(HorizontalTargetAngle) + 90);
		
		VerticalTargetAngle = -Mathf.Atan2( TargetNode.Position.Z - Position.Z, Position.Y - TargetNode.Position.Y );
		VerticalTargetAngleInDegress = (Mathf.RadToDeg(VerticalTargetAngle) - 90);

		RotationDegrees = new Vector3(0, HorizontalTargetAngleInDegress, 0);
	}
}

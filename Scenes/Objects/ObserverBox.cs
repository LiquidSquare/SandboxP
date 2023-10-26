using Godot;
using System;

public partial class ObserverBox : CsgBox3D
{
	float HorizontalTargetAngleInDegress = 0.0f;
	float VerticalTargetAngleInDegress = 0.0f;
	float HorizontalTargetAngle = 0.0f;
	float VerticalTargetAngle = 0.0f;
	float RotationSpeed = 10f;
	Node3D TargetNode;

	public override void _Ready()
	{
		TargetNode = GetNode<Node3D>("../%TargetBoxD");
	}

	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        HorizontalTargetAngle = Mathf.Atan2( TargetNode.GlobalPosition.Z - GlobalPosition.Z, GlobalPosition.X - TargetNode.GlobalPosition.X ) + Mathf.Pi/2;
		VerticalTargetAngle = -Mathf.Atan2( TargetNode.GlobalPosition.Z - GlobalPosition.Z, GlobalPosition.Y - TargetNode.GlobalPosition.Y ) - Mathf.Pi/2;

		// Horizontal Only
		// Rotation = new Vector3(0, HorizontalTargetAngle, 0);

		// Vertical Only
		//Rotation = new Vector3(VerticalTargetAngle, 0, 0);

		// Horizontal and Vertical
		Rotation = new Vector3(VerticalTargetAngle, HorizontalTargetAngle, 0);

	}
}

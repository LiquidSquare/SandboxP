using Godot;
using System;

public partial class Label3D : Godot.Label3D
{
	Node3D CameraSpringArm;
	ThirdPersonCameraComponent Camera;

	public override void _Ready()
	{
		Camera = GetNode<ThirdPersonCameraComponent>("%ThirdPersonCameraComponent");
		CameraSpringArm = Camera.GetNode<Node3D>("SpringArmPivot");
	}

	public override void _Process(double delta)
	{
		Rotation = CameraSpringArm.Rotation;
	}
}

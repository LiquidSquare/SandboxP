using Godot;
using System;

public partial class PlayerVisuals : Node3D
{
	//Inner Variables
	Vector3 newVisualsRotation;

	//Nodes References
	CharacterBody3D Player;
	AnimationTree AnimationTree;
	Skeleton3D Skeleton;

	float HorizontalTargetAngle = 0.0f;
	float VerticalTargetAngle = 0.0f;
	float RotationSpeed = 10f;
	Node3D TargetNode;

	public override void _Ready()
	{
		Player = GetNode<CharacterBody3D>("../");
		Skeleton = GetNode<Skeleton3D>("%Skeleton3D");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		TargetNode = GetParent().GetParent().GetNode<Node3D>("%TargetBoxB");
	}

	public override void _Process(double delta)
	{
		if ( (Vector3)Player.Get("Direction") != Vector3.Zero )
		{
			newVisualsRotation = new Vector3()
			{
				X = Rotation.X,
				Y = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(Player.Velocity.X, Player.Velocity.Z) + Mathf.Pi, 0.25f),
				Z = Rotation.Z
			};
		}
		
		Rotation = newVisualsRotation;
    	
		AnimationTree.Set("parameters/BlendSpace1D/blend_position", Player.Velocity.Length() / (float)Player.Get("Speed") );

		HorizontalTargetAngle = Mathf.Atan2( TargetNode.GlobalPosition.Z - Player.GlobalPosition.Z, Player.GlobalPosition.X - TargetNode.GlobalPosition.X ) + Mathf.Pi/2 - Rotation.Y;
		VerticalTargetAngle = -Mathf.Atan2( TargetNode.GlobalPosition.Z - GlobalPosition.Z, GlobalPosition.Y - TargetNode.GlobalPosition.Y ) - Mathf.Pi/2;

		// Horizontal Only
		// Skeleton.SetBonePoseRotation(5, new Quaternion(Vector3.Up, HorizontalTargetAngle ));

		// Vertical Only
		// Skeleton.SetBonePoseRotation(5, new Quaternion(Vector3.Left, VerticalTargetAngle));

		// Vertical and Horizontal
		Skeleton.SetBonePoseRotation(5, new Quaternion( -VerticalTargetAngle, HorizontalTargetAngle, 0, 1));

	}
}

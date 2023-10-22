using Godot;
using System;

public partial class PlayerVisuals : Node3D
{
	//Inner Variables
	Vector3 newVisualsRotation;

	//Nodes References
	public AnimationTree AnimationTree;
	public Skeleton3D Skeleton;
	public Area3D ViewArea;
	public MeshInstance3D TargetNode;
	public CharacterBody3D Parent;
	public ThirdPersonMovementComponent ThirdPersonMovementComponent;

	float HorizontalTargetAngle = 0.0f;
	float VerticalTargetAngle = 0.0f;
	float RotationSpeed = 10f;

	public override void _Ready()
	{
		Parent = GetParent<CharacterBody3D>();
		Skeleton = GetNode<Skeleton3D>("%Skeleton3D");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		ViewArea = GetParent().GetNode<Area3D>("ViewArea3DComponent");
		TargetNode = GetParent().GetNode<MeshInstance3D>("ViewArea3DComponent/TargetPointMeshInstance3D");
		ThirdPersonMovementComponent = Parent.GetNode<ThirdPersonMovementComponent>("ThirdPersonMovementComponent");
	}

	public override void _Process(double delta)
	{
		if ( ThirdPersonMovementComponent.Direction != Vector3.Zero )
		{
			newVisualsRotation = new Vector3()
			{
				X = Rotation.X,
				Y = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(ThirdPersonMovementComponent.Velocity.X, ThirdPersonMovementComponent.Velocity.Z) + Mathf.Pi, 0.25f),
				Z = Rotation.Z
			};
		}
		Rotation = newVisualsRotation;

		AnimationTree.Set("parameters/BlendSpace1D/blend_position", ThirdPersonMovementComponent.Velocity.Length() / ThirdPersonMovementComponent.Speed );

		if ( (bool)ViewArea.Get("mustPointFollowTarget") )
		{
			HorizontalTargetAngle = Mathf.Atan2( TargetNode.GlobalPosition.Z - Parent.GlobalPosition.Z, Parent.GlobalPosition.X - TargetNode.GlobalPosition.X ) + Mathf.Pi/2 - Rotation.Y;
			VerticalTargetAngle = -Mathf.Atan2( TargetNode.GlobalPosition.Z - GlobalPosition.Z, GlobalPosition.Y - TargetNode.GlobalPosition.Y ) - Mathf.Pi/2;
			Skeleton.SetBonePoseRotation(5, new Quaternion( -VerticalTargetAngle, HorizontalTargetAngle, 0, 1));
		}

	}
}

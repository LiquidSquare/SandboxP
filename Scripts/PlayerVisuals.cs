using Godot;
using System;

public partial class PlayerVisuals : Node3D
{
	//Inner Variables
	Vector3 newVisualsRotation;
	float RotationSpeed = 10f;

	//Nodes References
	public AnimationTree AnimationTree;
	public Skeleton3D Skeleton;
	public CharacterBody3D Parent;
	public ViewArea3DComponent ViewAreaComponent;
	public ThirdPersonMovementComponent ThirdPersonMovementComponent;

	public override void _Ready()
	{
		Parent = GetParent<CharacterBody3D>();
		Skeleton = GetNode<Skeleton3D>("%Skeleton3D");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		ViewAreaComponent = Parent.GetNode<ViewArea3DComponent>("ViewArea3DComponent");
		ThirdPersonMovementComponent = Parent.GetNode<ThirdPersonMovementComponent>("ThirdPersonMovementComponent");
	}

	public override void _Process(double delta)
	{
		/* HEAD ROTATION TO FACE A TARGET */
		if( ViewAreaComponent.mustPointFollowTarget )
		{
			Skeleton.SetBonePoseRotation(5, new Quaternion( -ViewAreaComponent.VerticalTargetAngle, ViewAreaComponent.HorizontalTargetAngle, 0, 1)); //TODO: Bone id is hardset;
		}

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

	}
}

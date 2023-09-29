using Godot;
using System;

public partial class PathFollow : PathFollow3D
{

	[Export]
	public float Speed = 5; 
    public override void _PhysicsProcess(double delta)
    {
        Progress += Speed * (float)delta;
    }
}

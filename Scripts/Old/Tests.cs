using Godot;
using System;

public partial class Tests : Node3D 
{
	Timer Timer;
	MeshInstance3D Coisa;
	Label Label;

	float horizontalInput;
	float verticalInput;
	Vector3 direction;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Timer = GetNode<Timer>("Timer");
		this.Label = GetNode<Label>("Label");
		this.Coisa = GetNode<MeshInstance3D>("Coisa");

		this.Timer.Timeout += () => TestFunction();
	}

	public override void _Input(InputEvent @event)
	{
		horizontalInput = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		verticalInput = Input.GetActionStrength("down") - Input.GetActionStrength("up");
		direction = new Vector3( horizontalInput, 0, verticalInput ).Normalized();
		Coisa.LookAt(-direction);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Label.Text = $" H_INPUT: {horizontalInput} \n V_INPUT: {verticalInput} \n";
		Label.Text += $"ROTATION: {this.Coisa.Rotation} \n";
		Label.Text += $"TRANSFORM: {this.Coisa.Transform} \n";
		Label.Text += $"POSITION: {this.Coisa.Position} \n";
		Label.Text += $"Transform Basis: {Transform.Basis} \n";

		Label.Text += $"DIRECTION: {direction} \n";
	}

	private void TestFunction()
	{
		GD.Print("Timeout!");
		// this.Coisa.RotateY( Mathf.DegToRad(45) );
	}
}

using Godot;
using System;

public partial class NPCController : CharacterBody2D
{
	public Node2D moveLocation = null;
	private float speed;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(moveLocation != null )
		{
			
		}
	}
}

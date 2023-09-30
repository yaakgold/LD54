using Godot;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System;
using Godot.Collections;

public partial class CustomerController : Node2D
{
	public List<Node> tables;

	[Export] PackedScene NPC;

	CharacterBody2D npc;
	AStarGrid2D grid;
	Node2D chosenTable;
	Array<Vector2I> path = new Array<Vector2I>();

	int index = 0;

	double timer = 0;

	float speed = 1000;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tables = GetNode("%Tables").GetChildren().ToList();

		npc = (CharacterBody2D)NPC.Instantiate();
		AddChild(npc);

		grid = new AStarGrid2D();
		grid.Region = new Rect2I(new Vector2I(0, 0), new Vector2I(72, 41));
		grid.CellSize = new Vector2(16, 16);
		grid.Update();

		chosenTable = (Node2D)tables[0];

		path = grid.GetIdPath(new Vector2I((int)(npc.Position.X / 16), (int)(npc.Position.Y / 16)),
								new Vector2I((int)(chosenTable.Position.X / 16), (int)(chosenTable.Position.Y / 16)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(index + 1 >= path.Count)
			return;
		
		timer += delta;
		//Debug.WriteLine(timer);
		if(timer >= 1)
		{
			timer = 0;

			var v = path[index + 1] - path[index];
			npc.Velocity = new Vector2(v.X, v.Y).Normalized() * speed;
			npc.MoveAndSlide();

			Debug.WriteLine(npc.Velocity);

			index++;
		}
	}

	private Vector2I GetNPCGridPos(Vector2 npcPos)
	{
		return new Vector2I((int)(npc.Position.X / 16), (int)(npc.Position.Y / 16));
	}
}

using System;

public class Game
{
	public static void Main(String[] args)
	{
		//Initialize
		GameManager gameManager = new GameManager();

		Console.WriteLine("Wilkommen zum Schere Stein Papier Spiel!");
		gameManager.RegisterPlayer(true, "Chris");
		int HowManyNPCs = 10;
		for (int i = 0; i < HowManyNPCs; i++)
		{
			gameManager.RegisterPlayer(false, "NPC" + i);
		}

		Console.WriteLine("Wir haben " + Player.totalPlayers + " mitspieper!");
		gameManager.Run();
	}
}
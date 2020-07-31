import java.util.LinkedList;
import java.util.Vector;
import java.util.Stack;

import java.util.List;
//import java.awt.List;
import java.util.ArrayList;
import java.util.Scanner; 
//import java.lang.*;

public class SchereSteinPapier
{
	public static void main(String[] args)
	{
		//Initialize
		GameManager gameManager = new GameManager();
		
		System.out.println("Wilkommen zum Schere Stein Papier Spiel!");
		gameManager.RegisterPlayer(true, "Chris");
		
		int HowManyNPCs = 10;
		for (int i = 0; i < HowManyNPCs; i++)
		{
			gameManager.RegisterPlayer(false, "NPC" + i);
		}
		
		System.out.println("Wir haben " + Player.totalPlayers + " mitspieper!");
		gameManager.Run();
	}
}
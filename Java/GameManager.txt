import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class GameManager
{
	//Player input string to number converter
	public static int tryParse(String value, int defaultVal)
	{
	    try
	    {
	        return Integer.parseInt(value);
	    }
	    catch (NumberFormatException e)
	    {
	        return defaultVal;
	    }
	}
	
	//Registriert den player (packt ihn in die players list)
	public void RegisterPlayer(boolean isHuman, String name)
	{
		Player player = new Player(isHuman, name);
		players.add(player);
	}
	
	//muss die runde wiederholt werden?
 	public int GetZeichenBenutzt()
 	{
		boolean[] zeichenMakierer = new boolean[Zeichen.values().length];
		List<Zeichen> zeichenBenutzt = new ArrayList<Zeichen>();
		int VerschiedeneZeichenBenutzt = 0;
		
		//makiert im bool array welche enum Zeichen benutzt worden sind.
		for (int j = 0; j < players.size(); j++)
		{
			if (players.get(j).m_stillPlaying)
			{
				zeichenMakierer[players.get(j).getZeichen().ordinal()] = true;
			}
		}
		
		//loop durch das array um zu gucken ob irgend ein zeichen nicht gewählt worden ist
		for (int j = 0; j < zeichenMakierer.length; j++)
		{
			
			if (zeichenMakierer[j] == true)
				{VerschiedeneZeichenBenutzt++;}
		}
		return VerschiedeneZeichenBenutzt;
 	}
 	
 	//Wer hat das stärkere zeichen gewählt? Wer ist diese runde ausgeschieden?
 	public void Evaluirung()
 	{
 		List<Zeichen> zeichenBenutzt = new ArrayList<Zeichen>();
 		boolean[] zeichenBenutztBool = new boolean[Zeichen.values().length];
		
		//makiert im array welche enum Zeichen benutzt worden sind.
		for (int j = 0; j < players.size(); j++)
		{
			if (players.get(j).m_stillPlaying)
			{
				zeichenBenutztBool[players.get(j).getZeichen().ordinal()] = true;
			}
		}
		
		//put alle benutzten zeichen ins zeichenBenutzt (zeichen) list
		for (int i = 0; i < Zeichen.values().length; i++)
		{
			if (zeichenBenutztBool[i])
			{
				zeichenBenutzt.add(Zeichen.values()[i]);
			}
		}
		
		Zeichen unbenutztesZeichen = Zeichen.Stein; //dummy da Java rumheult. Mann kann es nicht leer lassen... wird in nähsten loop überschrieben.
		
		for (int i = 0; i < Zeichen.values().length; i++)
		{
			if (!zeichenBenutztBool[i])
			{
				unbenutztesZeichen = Zeichen.values()[i];
			}
		}
		
		Zeichen winningZeichen;
		
		switch (unbenutztesZeichen)
		{
		case Schere:
			winningZeichen = Zeichen.Papier;
			break;
		case Stein:
			winningZeichen = Zeichen.Schere;
			break;
		case Papier:
			winningZeichen = Zeichen.Stein;
			break;
		default:
			winningZeichen = Zeichen.Stein; //Java heult rum das variable may not be initialized
			break;
		}
		
		//kick leute die verloren haben aus dieser runde raus.
		for (int i = 0; i < players.size(); i++)
		{
			if(players.get(i).getZeichen() != winningZeichen)
			{
				players.get(i).m_stillPlaying = false;
				System.out.println("Player " + players.get(i).getName() + " hat leider verloren in dieser runde." );	
			}
		}
 	}
 	
 	public void ResetRound()
 	{
 		for (int i = 0; i < players.size(); i++)
 		{
			players.get(i).m_stillPlaying = true;
		}
 		System.out.println("Nähste Runde!");
 	}
 	
 	public void Reset()
 	{
 		for (int i = 0; i < players.size(); i++)
 		{
			players.get(i).m_stillPlaying = true;
			players.get(i).SetPoints(0);
		}
 	}
 	
 	public int GetNumberOfStillPlayingPlayers()
 	{
 		int stillPlayingPlayers = 0;
 		for (int j = 0; j < players.size(); j++)
		{
			if (players.get(j).m_stillPlaying)
			{
				stillPlayingPlayers++;
			}
		}
 		return stillPlayingPlayers;
 	}
 	
 	
	public void PrintMitspielerZeichen()
	{
		for (int j = 0; j < players.size(); j++)
		{
			if (players.get(j).m_stillPlaying)
			{
				System.out.println(players.get(j).getName() + " hat " + players.get(j).getZeichen() + " gewaehlt!");
			}
		}
	}
	public void PrintMitspielerPunkte()
	{
		for (int j = 0; j < players.size(); j++)
		{
			System.out.println(players.get(j).getName() + " hat " + players.get(j).GetPoints() + " Punkte!" );
		}
	}
	 	

 	public void Run()
 	{
 		for (int i = 1; i < maxRunden + 1; i++)
		{
 			while (GetNumberOfStillPlayingPlayers() > 1) //while there is more then 1 player keep looping
 			{
	 			System.out.println("Runde " + i + "!");
				//Step 1, wähle zeichen
				for (int j = 0; j < players.size(); j++)
				{
					//Player (Human)
					if (players.get(j).m_stillPlaying)
					{
						if (players.get(j).m_isHuman == true)
						{
							System.out.println(players.get(j).getName() + " waehle ein zeichen!");
							int PlayerInputInt;
							boolean ZeichenExistiert = false;
							for (int k = 0; k < ZeichenLaenge; k++)
							{
								System.out.println(k + " = " + Zeichen.values()[k].toString());
							}
							
							do
							{
								playerInput = playerInputScanner.nextLine();
								PlayerInputInt = tryParse(playerInput,-1);
								ZeichenExistiert = (PlayerInputInt > -1 && PlayerInputInt < (ZeichenLaenge));
							}while(!ZeichenExistiert);
							players.get(j).setZeichen(Zeichen.values()[PlayerInputInt]);
						}
						//Player (NPC)
						else
						{
							int rngZeichen = ((int)(Math.random() * 100) % ZeichenLaenge);
							players.get(j).setZeichen(Zeichen.values()[rngZeichen]);
						}	
						
					}
				}
				PrintMitspielerZeichen();
	
				int verschiedeneZeichenBenutzt = GetZeichenBenutzt();
				
				//falls alle zeichen benutzt worden sind wird die runde wiederholt
				if (verschiedeneZeichenBenutzt == Zeichen.values().length)
				{
					System.out.println("Alle zeichen wurden benutzt, runde wird wiederholt!");
					continue;
				}
				
				//falls nur 1 zeichen benutzt worden sind wird die runde wiederholt.
				if (verschiedeneZeichenBenutzt == 1)
				{
					System.out.println("Das Gleiche zeichen wurde benutzt, runde wird wiederholt!");
					continue;
				}
				
				Evaluirung();
			}
 			//Give the winning player a point.
 			for (int j = 0; j < players.size(); j++)
 			{
 				if (players.get(j).m_stillPlaying)
 				{
 					players.get(j).AddPoint();
 				}
 			}
 			PrintMitspielerPunkte();
 			ResetRound();
		}
 	}
 	
	//Settings
	private int maxRunden = 10;
	int ZeichenLaenge = Zeichen.values().length;
	Scanner playerInputScanner = new Scanner(System.in);
	String playerInput;
	List<Player> players = new ArrayList<Player>();
}
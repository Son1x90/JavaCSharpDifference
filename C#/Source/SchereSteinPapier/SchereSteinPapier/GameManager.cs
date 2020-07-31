using System;
using System.Collections.Generic;
using System.Text;

public class GameManager
{

	//Registriert den player (packt ihn in die players list)
	public void RegisterPlayer(bool isHuman, String name)
	{
		Player player = new Player(isHuman, name);
		players.Add(player);
	}

	//muss die runde wiederholt werden?
	public int GetZeichenBenutzt()
	{
		bool[] zeichenMakierer = new bool[Enum.GetNames(typeof(Zeichen)).Length];
		List<Zeichen> zeichenBenutzt = new List<Zeichen>();
		int VerschiedeneZeichenBenutzt = 0;

		//makiert im bool array welche enum Zeichen benutzt worden sind.
		for (int j = 0; j < players.Count; j++)
		{
			if (players[j].m_stillPlaying)
			{
				zeichenMakierer[(byte)players[j].getZeichen()] = true;
			}
		}

		//loop durch das array um zu gucken ob irgend ein zeichen nicht gewählt worden ist
		for (int j = 0; j < zeichenMakierer.Length; j++)
		{

			if (zeichenMakierer[j] == true)
			{ VerschiedeneZeichenBenutzt++; }
		}
		return VerschiedeneZeichenBenutzt;
	}

	//Wer hat das stärkere zeichen gewählt? Wer ist diese runde ausgeschieden?
	public void Evaluirung()
	{
		List<Zeichen> zeichenBenutzt = new List<Zeichen>();
		bool[] zeichenBenutztBool = new bool[Enum.GetNames(typeof(Zeichen)).Length];

		//makiert im array welche enum Zeichen benutzt worden sind.
		for (int j = 0; j < players.Count; j++)
		{
			if (players[j].m_stillPlaying)
			{
				zeichenBenutztBool[(byte)players[j].getZeichen()] = true;
			}
		}

		//put alle benutzten zeichen ins zeichenBenutzt (zeichen) list
		for (int i = 0; i < Enum.GetNames(typeof(Zeichen)).Length; i++)
		{
			if (zeichenBenutztBool[i])
			{
				zeichenBenutzt.Add((Zeichen)i);
			}
		}

		Zeichen unbenutztesZeichen = Zeichen.Stein; //dummy da Java rumheult. Mann kann es nicht leer lassen... wird in nähsten loop überschrieben.

		for (int i = 0; i < Enum.GetNames(typeof(Zeichen)).Length; i++)
		{
			if (!zeichenBenutztBool[i])
			{
				unbenutztesZeichen = (Zeichen)i;
			}
		}

		Zeichen winningZeichen;

		switch (unbenutztesZeichen)
		{
			case Zeichen.Schere:
				winningZeichen = Zeichen.Papier;
				break;
			case Zeichen.Stein:
				winningZeichen = Zeichen.Schere;
				break;
			case Zeichen.Papier:
				winningZeichen = Zeichen.Stein;
				break;
			default:
				winningZeichen = Zeichen.Stein; //Java heult rum das variable may not be initialized
				break;
		}

		//kick leute die verloren haben aus dieser runde raus.
		for (int i = 0; i < players.Count; i++)
		{
			if (players[i].getZeichen() != winningZeichen)
			{
				players[i].m_stillPlaying = false;
				Console.WriteLine("Player " + players[i].getName() + " hat leider verloren in dieser runde.");
			}
		}
	}

	public void ResetRound()
	{
		for (int i = 0; i < players.Count; i++)
		{
			players[i].m_stillPlaying = true;
		}
		Console.WriteLine("Nähste Runde!");
	}

	public void Reset()
	{
		for (int i = 0; i < players.Count; i++)
		{
			players[i].m_stillPlaying = true;
			players[i].SetPoints(0);
		}
	}

	public int GetNumberOfStillPlayingPlayers()
	{
		int stillPlayingPlayers = 0;
		for (int j = 0; j < players.Count; j++)
		{
			if (players[j].m_stillPlaying)
			{
				stillPlayingPlayers++;
			}
		}
		return stillPlayingPlayers;
	}


	public void PrintMitspielerZeichen()
	{
		for (int j = 0; j < players.Count; j++)
		{
			if (players[j].m_stillPlaying)
			{
				Console.WriteLine(players[j].getName() + " hat " + players[j].getZeichen() + " gewaehlt!");
			}
		}
	}
	public void PrintMitspielerPunkte()
	{
		for (int j = 0; j < players.Count; j++)
		{
			Console.WriteLine(players[j].getName() + " hat " + players[j].GetPoints() + " Punkte!");
		}
	}


	public void Run()
	{
		for (int i = 1; i < maxRunden + 1; i++)
		{
			while (GetNumberOfStillPlayingPlayers() > 1) //while there is more then 1 player keep looping
			{
				Console.WriteLine("Runde " + i + "!");
				//Step 1, wähle zeichen
				for (int j = 0; j < players.Count; j++)
				{
					//Player (Human)
					if (players[j].m_stillPlaying)
					{
						if (players[j].m_isHuman == true)
						{
							Console.WriteLine(players[j].getName() + " waehle ein zeichen!");
							int PlayerInputInt;
							bool ZeichenExistiert = false;
							for (int k = 0; k < ZeichenLaenge; k++)
							{
								Console.WriteLine(k + " = " + (Zeichen)k);
							}

							do
							{
								playerInput = Console.ReadLine();
								Int32.TryParse(playerInput, out PlayerInputInt);
								ZeichenExistiert = (PlayerInputInt > -1 && PlayerInputInt < (ZeichenLaenge));
							} while (!ZeichenExistiert);
							players[j].setZeichen((Zeichen)PlayerInputInt);
						}
						//Player (NPC)
						else
						{
							var rand = new Random();
							int rngZeichen = rand.Next(0, Enum.GetNames(typeof(Zeichen)).Length);
							players[j].setZeichen((Zeichen)rngZeichen);
						}

					}
				}
				PrintMitspielerZeichen();

				int verschiedeneZeichenBenutzt = GetZeichenBenutzt();

				//falls alle zeichen benutzt worden sind wird die runde wiederholt
				if (verschiedeneZeichenBenutzt == Enum.GetNames(typeof(Zeichen)).Length)
				{
					Console.WriteLine("Alle zeichen wurden benutzt, runde wird wiederholt!");
					continue;
				}

				//falls nur 1 zeichen benutzt worden sind wird die runde wiederholt.
				if (verschiedeneZeichenBenutzt == 1)
				{
					Console.WriteLine("Das Gleiche zeichen wurde benutzt, runde wird wiederholt!");
					continue;
				}

				Evaluirung();
			}
			//Give the winning player a point.
			for (int j = 0; j < players.Count; j++)
			{
				if (players[j].m_stillPlaying)
				{
					players[j].AddPoint();
				}
			}
			PrintMitspielerPunkte();
			ResetRound();
		}
	}

	//Settings
	private int maxRunden = 10;
	int ZeichenLaenge = Enum.GetNames(typeof(Zeichen)).Length;
	//Scanner playerInputScanner = new Scanner(System.in);
	String playerInput;
	List<Player> players = new List<Player>();
}
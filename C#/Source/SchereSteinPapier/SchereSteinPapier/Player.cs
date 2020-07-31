using System;
using System.Collections.Generic;
using System.Text;

public class Player
{
	//Constructor
	public Player(bool isHuman, String name)
	{
		m_Punkte = 0;
		m_isHuman = isHuman;
		m_playerName = name;
		totalPlayers++;
		//m_playerId = totalPlayers -1;
		m_stillPlaying = true;
	}

	public String getName()
	{
		return m_playerName;
	}

	public Zeichen getZeichen()
	{
		return m_Zeichen;
	}

	public void setZeichen(Zeichen zeichen)
	{
		m_Zeichen = zeichen;
	}

	public void AddPoint()
	{
		m_Punkte++;
	}

	public int GetPoints()
	{
		return m_Punkte;
	}

	public void SetPoints(int points)
	{
		m_Punkte = points;
	}

	//public int GetPlayerId()
	//{
	//	return m_playerId;
	//}


	private int m_Punkte; // wie viele punkte der spieler hat.
	public bool m_isHuman; // falls true dann muss die eingabe selbst gemacht werden, falls false wird das zeichen zufällig gewählt.
	private Zeichen m_Zeichen; // Das gewählte zeichen, zb Stein
	//private int m_playerId; // zur eindeutigen identifikation, in diesem falle hatten wir es noch nicht benötigt, (not used)
	private String m_playerName; // Name des spielers
	public bool m_stillPlaying; // ob er in dieser runde noch mitspielt oder bereits weggefallen ist.
	public static int totalPlayers; // Globale variable (da static) auf die wir zugreifen können und sehen können wie viele spieler wir haben.
}
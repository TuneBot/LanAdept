﻿using System.Collections.Generic;
using LanAdeptCore.Service;
using LanAdeptData.Model;

namespace LanAdept.Models
{
    public class TeamModel
	{
		public int TeamID { get; set; }

		public bool IsMyTeam { get; set; }

		public bool IsMyTeamForTeamLeader { get; set; }

		public bool IsTeamDemande { get; set; }

		public string Name { get; set; }

		public string Tag { get; set; }

		public GamerTag TeamLeaderTag { get; set; }

		public IEnumerable<GamerTag> Gamertags { get; set; }

		public TeamModel(Team team)
		{
			TeamID = team.TeamID;
			Name = team.Name;
			Tag = team.Tag;
			TeamLeaderTag = team.TeamLeaderTag;
			Gamertags = team.GamerTags;
		}

		public string GetSeeTeamOnMapHash()
		{
			string hash = "?";

			foreach (GamerTag gamertag in Gamertags)
			{
				if (ReservationService.HasUserPlace(gamertag.User))
					hash += "seats=" + gamertag.User.LastReservation.Place.SeatsId + "&";
			}

			return hash;
		}
	}
}
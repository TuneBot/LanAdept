﻿using LanAdeptData.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using LanAdeptData.Model.Tournaments;
using LanAdeptCore.Service.Challonge;

namespace LanAdeptAdmin.Views.Tournaments.ModelController
{
	public class TournamentModel
	{
		[DisplayName("Heure de début")]
		[Required(ErrorMessage="L'heure de début du tournoi est requise")]
		[DisplayFormat(DataFormatString = @"{0:HH\hmm}")]
		public DateTime? StartTime { get; set; }

		public bool IsPublished { get; set; }

		public bool IsStarted { get; set; }

		public bool IsOver { get; set; }

		[DataType(DataType.MultilineText)]
		[DisplayName("Informations et règlements")]
		public string Info { get; set; }

		[DisplayName("Nb. de joueurs par équipe")]
		public int MaxPlayerPerTeam { get; set; }

		public int Id { get; set; }

        public bool IsChallonge { get; set; }

		[Display(Name = "Jeu")]
		public string Game { get; set; }

		public MvcHtmlString GetStatus()
		{
			string classeEtat = "";
			string messageEtat = "";

			if(!IsPublished)
			{
				classeEtat = "label-default";
				messageEtat = "Caché";
			}
			else if (!IsStarted && !IsOver)
			{
				classeEtat = "label-warning";
				messageEtat = "Préparation";
			}
			else if (!IsOver)
			{
				classeEtat = "label-danger";
				messageEtat = "En cours";
			}
			else
			{
				classeEtat = "label-primary";
				messageEtat = "Terminé";
			}

			return new MvcHtmlString("<div class=\"label " + classeEtat + "\">" + messageEtat + "</div>");
		}


		#region Navigation properties
		public virtual ICollection<Team> Teams { get; set; }
		#endregion

		#region Constructors

		public TournamentModel() {

		}

		public TournamentModel(Tournament tournament) {
			StartTime = tournament.StartTime;
			MaxPlayerPerTeam = tournament.MaxPlayerPerTeam;
			Game = tournament.Game;
			Teams = tournament.Teams;
			Id = tournament.TournamentID;
			IsOver = tournament.IsOver;
			IsStarted = tournament.IsStarted;
			IsPublished = tournament.IsPublished;
			Info = tournament.Info;
            IsChallonge = tournament.ChallongeUrl != null;
		}

		#endregion
	}
}
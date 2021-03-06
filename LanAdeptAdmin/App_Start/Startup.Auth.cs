﻿using LanAdeptCore.Manager;
using LanAdeptData.DAL;
using LanAdeptData.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanAdeptAdmin
{
	public partial class Startup
	{
		public void ConfigureAuth(IAppBuilder app)
		{
			app.CreatePerOwinContext(LanAdeptDataContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			// Autoriser l’application à utiliser un cookie pour stocker des informations pour l’utilisateur connecté
			// et pour utiliser un cookie à des fins de stockage temporaire des informations sur la connexion utilisateur avec un fournisseur de connexion tiers
			// Configurer le cookie de connexion
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Auth/Login"),
				Provider = new CookieAuthenticationProvider
				{
					// Cette fonction de sécurité est utilisée quand vous changez un mot de passe ou ajoutez une connexion externe à votre compte.  
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
						validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
				}
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			// Permet à l'application de stocker temporairement les informations utilisateur lors de la vérification du second facteur dans le processus d'authentification à 2 facteurs.
			app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

			// Permet à l'application de mémoriser le second facteur de vérification de la connexion, un numéro de téléphone ou un e-mail par exemple.
			// Lorsque vous activez cette option, votre seconde étape de vérification pendant le processus de connexion est mémorisée sur le poste à partir duquel vous vous êtes connecté.
			// Ceci est similaire à l'option RememberMe quand vous vous connectez.
			app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

#if DEBUG

			var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
			{
				AppId = "108482432869073",
				AppSecret = "9c53519dfb270275528207a135701c60"
			};

			facebookAuthenticationOptions.Scope.Add("email");
			app.UseFacebookAuthentication(facebookAuthenticationOptions);

			app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			{
				ClientId = "416838099642-ekrjgmhv1o088j9fe6j9co82foql8rpb.apps.googleusercontent.com",
				ClientSecret = "WBWKz27EkCgfFpdrUtT_GtFM"
			});

#else

			var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
			{
			   AppId = "108479299536053",
			   AppSecret = "0c0bd86e39ed4cdfc5eca6397e37311f"
			};

			facebookAuthenticationOptions.Scope.Add("email");
			app.UseFacebookAuthentication(facebookAuthenticationOptions);


			app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			{
				ClientId = "416838099642-759uqgsrh4aboil99n7ufrpru2lk28hm.apps.googleusercontent.com",
				ClientSecret = "LtHbk-EwRp5lblApjzBAm_h3"
			});

#endif


		}
	}
}
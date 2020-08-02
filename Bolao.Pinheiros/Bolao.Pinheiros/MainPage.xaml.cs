using Bolao.Pinheiros.BusinessLogic.Models;
using Bolao.Pinheiros.BusinessLogic.Utils;
using Bolao.Pinheiros.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Xamarin.Forms;

namespace Bolao.Pinheiros
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private Root _todaysGames;
        
        public MainPage()
        {
            InitializeComponent();

            _todaysGames = GetDataFromGames(DateTime.Now, false);
            _todaysGames.FillCountryNames();

            BindingContext = new CompetitionsViewModel(_todaysGames.competitions);
        }

        #region " PRIVATE METHODS "

        private List<Standing> GetCompetitionsData(List<Game> games)
        {
            var competitions = games.Select(x => x.competitionId).Distinct();
            var url = string.Concat(Constants.URL_BASE_COMPETITIONS, string.Join(",", competitions));
            var standings = GetDataFromApi<Root>(url);
            return standings.standings;
        }

        private T GetDataFromApi<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<T>(json);
                    if (data != null)
                    {
                        return data;
                    }
                }
            }

            return default;
        }

        private Root GetDataFromGames(DateTime date, bool onlyLiveGames)
        {
            var startDate = date.ToString(Constants.DATE_FORMAT);
            var endDate = date.ToString(Constants.DATE_FORMAT);
            var url = string.Format(Constants.URL_BASE, startDate, endDate);
            if (onlyLiveGames)
            {
                url = string.Concat(url, "&onlyLiveGames=true");
            }

            var model = GetDataFromApi<Root>(url);
            if (model.games != null && model.games.Any())
            {
                model.games = model.games.Where(x => !Constants.EXCLUDE_COMPETITIONS.Contains(x.competitionId)).ToList();
                model.games = model.games.OrderBy(x => x.competitionCountry).ThenBy(x => x.startTime).ToList();
                model.competitions = model.competitions.Where(x => !Constants.EXCLUDE_COMPETITIONS.Contains(x.id)).OrderBy(x => x.countryName).ToList();
            }

            return model;
        }

        private GameData GetGameData(int gameId)
        {
            var url = string.Format(Constants.URL_BASE_GAME, gameId);
            return GetDataFromApi<GameData>(url);
        }

        private Root GetGamesData(List<int> gamesIds)
        {
            gamesIds = gamesIds.OrderByDescending(x => x).ToList();
            var gamesData = new Root { games = new List<Game>() };
            var tarefas = new List<Thread>();

            foreach (var gameId in gamesIds)
            {
                var thread = new Thread(() => InsertGameData(gamesData, gameId));
                thread.Start();
                tarefas.Add(thread);
            }

            while (tarefas.Any(x => x.IsAlive))
            {
                Thread.Sleep(500);
            }

            return gamesData;
        }

        private void InsertGameData(Root data, double gameId)
        {
            var url = string.Format(Constants.URL_BASE_GAME, gameId);
            var gameData = GetDataFromApi<GameData>(url);
            data.games.Add(gameData.game);
        }

        #endregion " PRIVATE METHODS "
    }
}

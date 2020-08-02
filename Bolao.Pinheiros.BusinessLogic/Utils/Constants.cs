using System.Collections.Generic;

namespace Bolao.Pinheiros.BusinessLogic.Utils
{
    public static class Constants
    {
        public static readonly string DATE_FORMAT = "dd/MM/yyyy";

        public static readonly List<int> EXCLUDE_COMPETITIONS = new List<int> 
        { 
            146, 165, 173, 397, 554, 564, 5103, 5431, 5456, 5462, 5547, 5565, 5556, 5605, 5606, 
            5639, 5667, 5807, 5818, 5820, 5825, 6108, 6148, 6219, 6221, 6252, 6345, 6347, 6362, 
            6896, 7110, 7298, 7299, 7336, 7345, 7466, 7499, 7522, 7523, 7528, 7529, 7530, 7536, 
            7542, 7543, 7538, 7540, 7556, 7568, 7569, 7570, 7571, 7572, 7573, 7574 
        };
        
        public static readonly string GAMES_DATA = "GamesData";
        
        public static readonly int MAXIMUM_GAMES = 5;
        
        public static readonly string URL_BASE = "https://webws.365scores.com/web/games/?langId=31&timezoneName=America/Sao_Paulo&userCountryId=21&appTypeId=5&sports=1&startDate={0}&endDate={1}&showOdds=true";
        
        public static readonly string URL_BASE_COMPETITIONS = "https://webws.365scores.com/web/standings/?langId=31&timezoneName=America/Sao_Paulo&userCountryId=21&appTypeId=5&competitions=";
        
        public static readonly string URL_BASE_GAME = "https://webws.365scores.com/web/game/?langId=31&timezoneName=America/Sao_Paulo&userCountryId=21&appTypeId=5&gameId={0}";
        
        public static readonly string URL_BASE_GAMES = "https://webws.365scores.com/web/games/?langId=31&timezoneName=America/Sao_Paulo&userCountryId=21&appTypeId=5&games={0}&startDate=01/01/2000&endDate={1}";

    }
}

using Bolao.Pinheiros.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bolao.Pinheiros.ViewModels
{
    public class CompetitionsViewModel
    {
        public CompetitionsViewModel(IList<Competition> competitions)
        {
            Items = competitions;
            AgruppedData = Items.GroupBy(p => p.countryName)
                                .OrderBy(x => x.Key)
                                .Select(p => new ObservableGroupCollection<string, Competition>(p)).ToList();
            ItemsCount = competitions.Count;
        }

        public IList<Competition> Items { get; set; }

        public List<ObservableGroupCollection<string, Competition>> AgruppedData { get; set; }

        public int ItemsCount { get; set; }
    }
}

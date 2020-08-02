using System.Collections.Generic;

namespace Bolao.Pinheiros.BusinessLogic.Models
{
    public class Lineups
    {
        public string status { get; set; }
        public string formation { get; set; }
        public bool hasFieldPositions { get; set; }
        public List<Member> members { get; set; }
    }
}
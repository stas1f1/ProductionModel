using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cars
{
    public class Rule
    {
        public List<string> preconditions;
        public string consequence;

        public Rule(string r)
        {
            preconditions = new List<string>();

            var temp = r.Split('-');
            consequence = temp[1].Trim(' ');

            var pcs = temp[0].Split(',');
            foreach (var i in pcs)
                preconditions.Add(i.Trim(' '));
        }

        /// <summary>
        /// Имеются ли все условия в списке фактов
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public bool preconditionsValid(List<string> f)
        {
            foreach (var i in preconditions)
                if (!f.Contains(i)) return false;
            return true;
        }
    }
}

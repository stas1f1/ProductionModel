using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cars
{
    class Knowledge
    {
        public string conc = "";

        public Dictionary<string, string> facts = new Dictionary<string, string>();
        public List<string> basic_facts = new List<string>();
        public Dictionary<string, Rule> rules = new Dictionary<string, Rule>();

        public List<string> res_objects = new List<string>();

        public Knowledge(string factfname = "..//..//facts.txt", string rulefname = "..//..//rules.txt") { parse_facts(factfname); parse_rules(rulefname); get_basic_facts(); }

        /// <summary>
        /// Составление фактов
        /// </summary>
        /// <param name="fname"></param>
        private void parse_facts(string fname)
        {
            using (StreamReader fs = new StreamReader(fname))
            {
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    var temp = line.Split(':');
                    facts[temp[0].Trim(' ').ToString()] = temp[1].Trim(' ');
                }
            }
        }

        /// <summary>
        /// Поиск элементарных фактов
        /// </summary>
        /// <param name="fname"></param>
        private void get_basic_facts()
        {
            Dictionary<string, bool> compound_facts = new Dictionary<string, bool>();
            foreach (var i in facts)
                compound_facts[i.Key] = false;
            foreach (var i in rules.Values)
                compound_facts[i.consequence] = true;
            foreach (var i in compound_facts)
                if (i.Value == false) basic_facts.Add(i.Key);
        }

        /// <summary>
        /// Составление правил, идентификатор - правило
        /// </summary>
        /// <param name="fname"></param>
        private void parse_rules(string fname)
        {
            using (StreamReader fs = new StreamReader(fname))
            {
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    var temp = line.Split(':');
                    rules[temp[0].Trim(' ').ToString()] = new Rule(temp[1].Trim(' '));
                }
            }
        }


        /// <summary>
        /// Повестка (получить все продукции для текущего этапа)
        /// </summary>
        /// <param name="in_facts"></param>
        /// <param name="out_facts"></param>
        /// <returns></returns>
        private List<string> agenda(ref List<string> in_facts, ref List<string> out_facts)
        {
            List<string> res = new List<string>();
            foreach (var r in rules)
                if (r.Value.preconditionsValid(in_facts) && !out_facts.Contains(r.Key))
                    res.Add(r.Key);
            return res;
        }


        /// <summary>
        /// Запустить процесс продукции
        /// </summary>
        /// <param name="in_facts"></param>
        /// <param name="out_facts"></param>
        /// <returns></returns>
        public bool production_forward(ref List<string> in_facts, ref List<string> out_facts)
        {
            List<string> next = new List<string>();
            bool flag = false;

            do
            {
                next = agenda(ref in_facts, ref out_facts).ToList();
                if (next.Count > 0)
                {
                    string current_rule = next.First();
                    flag = true;
                    out_facts.Add(current_rule);

                    in_facts.Add(rules[current_rule].consequence);

                    foreach (var r in rules[current_rule].preconditions)
                    {
                        conc += facts[r];
                        if (r != rules[current_rule].preconditions.Last())
                            conc += ", ";
                    }
                    conc += " -> " + facts[rules[current_rule].consequence] + Environment.NewLine;

                    if (rules[current_rule].consequence[0] == 'F')
                        res_objects.Add(rules[current_rule].consequence);
                }

            } while (next.Count > 0);

            res_objects = res_objects.Distinct().Take(5).ToList();

            return flag;
        }

        /// <summary>
        /// Обратная повестка (получить все продукции для текущего этапа)
        /// </summary>
        /// <param name="in_facts"></param>
        /// <param name="out_facts"></param>
        /// <returns></returns>
        private List<string> agenda_reverse(ref List<string> in_facts, ref List<string> out_facts)
        {
            List<string> res = new List<string>();
            foreach (var r in rules)
                if (in_facts.Contains(r.Value.consequence))
                {
                    bool flag = false;
                    foreach (var pc in r.Value.preconditions)
                        if (!out_facts.Contains(pc))
                            flag = true;
                    if (flag) res.Add(r.Key);
                }
            return res;
        }

        /// <summary>
        /// Запустить обратный процесс продукции
        /// </summary>
        /// <param name="in_facts"></param>
        /// <param name="out_facts"></param>
        /// <returns></returns>
        public bool production_reverse(ref List<string> in_facts, ref List<string> out_facts)
        {
            bool flag = false;
            List<string> achieved = in_facts;
            List<string> next = new List<string>();
            while (in_facts.Count != 0)
            {
                next = agenda_reverse(ref in_facts, ref out_facts).ToList();
                if (next.Count > 0)
                {
                    string current_rule = next.First();
                    flag = true;
                    foreach (var pc in rules[current_rule].preconditions)
                        if (!achieved.Contains(pc))
                        {
                            achieved.Add(pc);
                            if (!basic_facts.Contains(pc))
                                in_facts.Add(pc);
                            else
                                out_facts.Add(pc);
                        }
                    

                    in_facts.Remove(rules[current_rule].consequence);

                    string line = "";
                    flag = true;

                    foreach (var r in rules[current_rule].preconditions)
                    {
                        line += facts[r];
                        if (r != rules[current_rule].preconditions.Last())
                            line += ", ";
                    }
                    conc = line + " -> " + facts[rules[current_rule].consequence] + Environment.NewLine + conc;
                    
                }
                else
                {
                    foreach (var f in in_facts)
                        out_facts.Add(f);
                }
            }

            res_objects = res_objects.Distinct().ToList();

            return flag;
        }
    }
}

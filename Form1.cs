using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace cars
{
    public partial class Form1 : Form
    {
        Knowledge knowledge;

        public Form1()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            knowledge = new Knowledge();
            foreach (var item in knowledge.facts.Keys)
            {
                if (item.First() == 'T')
                    checkedListBoxT.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'S')
                    checkedListBoxS.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'P')
                    checkedListBoxP.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'Z')
                    checkedListBoxZ.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'C')
                    checkedListBoxС.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'W')
                    checkedListBoxW.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'F')
                    checkedListBoxF.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'O')
                    checkedListBoxO.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'G')
                    checkedListBoxG.Items.Add("" + item + ": " + knowledge.facts[item]);
                if (item.First() == 'M')
                    checkedListBoxM.Items.Add("" + item + ": " + knowledge.facts[item]);
            }
        }
        private void start_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            listBox1.Items.Clear();
            List<string> in_fact = new List<string>();
            foreach (var i in summary.Items)
            {
                in_fact.Add(i.ToString().Split(':')[0].Trim(' '));
            }
            List<string> repeat = new List<string>();
            knowledge.production_forward(ref in_fact, ref repeat);
            textBox2.Text = knowledge.conc;

            foreach (var i in knowledge.res_objects)
                listBox1.Items.Add(knowledge.facts[i]);
        }


        private void reverseButton_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            listBox1.Items.Clear();
            List<string> in_fact = new List<string>();
            foreach (var i in summary.Items)
            {
                in_fact.Add(i.ToString().Split(':')[0].Trim(' '));
            }
            List<string> repeat = new List<string>();
            knowledge.production_reverse(ref in_fact, ref repeat);
            textBox2.Text = knowledge.conc;

            foreach (var i in knowledge.res_objects)
                listBox1.Items.Add(knowledge.facts[i]);
        }

        private void checkedListBoxT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxT.SelectedItem);
            checkedListBoxT.Items.Remove(checkedListBoxT.SelectedItem);
        }
        private void checkedListBoxS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxS.SelectedItem);
            checkedListBoxS.Items.Remove(checkedListBoxS.SelectedItem);
        }
        private void checkedListBoxP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxP.SelectedItem);
            checkedListBoxP.Items.Remove(checkedListBoxP.SelectedItem);
        }
        private void checkedListBoxZ_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxZ.SelectedItem);
            checkedListBoxZ.Items.Remove(checkedListBoxZ.SelectedItem);
        }
        private void checkedListBoxC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxС.SelectedItem);
            checkedListBoxС.Items.Remove(checkedListBoxС.SelectedItem);
        }
        private void checkedListBoxF_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxF.SelectedItem);
            checkedListBoxF.Items.Remove(checkedListBoxF.SelectedItem);
        }
        private void checkedListBoxW_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxW.SelectedItem);
            checkedListBoxW.Items.Remove(checkedListBoxW.SelectedItem);
        }
        private void checkedListBoxO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxO.SelectedItem);
            checkedListBoxO.Items.Remove(checkedListBoxO.SelectedItem);
        }
        private void checkedListBoxG_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxG.SelectedItem);
            checkedListBoxG.Items.Remove(checkedListBoxG.SelectedItem);
        }
        private void checkedListBoxM_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            summary.Items.Add(checkedListBoxM.SelectedItem);
            checkedListBoxM.Items.Remove(checkedListBoxM.SelectedItem);
        }

        private void return_facts(char rem)
        {
            switch (rem)
            {
                case 'T':
                    checkedListBoxT.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'S':
                    checkedListBoxS.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'P':
                    checkedListBoxP.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'Z':
                    checkedListBoxZ.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'C':
                    checkedListBoxС.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'F':
                    checkedListBoxF.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'W':
                    checkedListBoxW.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'O':
                    checkedListBoxO.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'G':
                    checkedListBoxG.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                case 'M':
                    checkedListBoxM.Items.Add(summary.SelectedItem);
                    summary.Items.Remove(summary.SelectedItem);
                    break;
                default:
                    break;
            }
        }

        private void summary_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var rem = summary.SelectedItem.ToString()[0];
            return_facts(rem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBoxT.Items.Clear();
            checkedListBoxS.Items.Clear();
            checkedListBoxP.Items.Clear();
            checkedListBoxZ.Items.Clear();
            checkedListBoxС.Items.Clear();
            checkedListBoxW.Items.Clear();
            checkedListBoxF.Items.Clear();
            checkedListBoxO.Items.Clear();
            checkedListBoxG.Items.Clear();
            checkedListBoxM.Items.Clear();
            summary.Items.Clear();
            listBox1.Items.Clear();
            textBox2.Text = "";
            load();
        }

        

    }
}

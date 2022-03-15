using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotFuzzy;
namespace AliacFuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection detergent,dirt,throttle;
        LinguisticVariable clothes_detergent, clothes_dirt, mythrottle;
        FuzzyRuleCollection myrules;
        

        public Form1()
        {
            InitializeComponent();
        }

    
        public void setMembers()
        {

            detergent = new MembershipFunctionCollection();
            detergent.Add(new MembershipFunction("LOW",0.0,0.0,45.0,50.0));
            detergent.Add(new MembershipFunction("OK", 45.0, 50.0, 50.0, 55.0));
            detergent.Add(new MembershipFunction("HIGH", 50.0, 55.0, 100.0, 100.0));
            clothes_detergent = new LinguisticVariable("DETERGENT", detergent);


            dirt = new MembershipFunctionCollection();
            dirt.Add(new MembershipFunction("DOWN", 0.0, 0.0, 0.0, 0.0));
            dirt.Add(new MembershipFunction("LEVEL", 0.0, 1.0, 1.0, 5.0));
            dirt.Add(new MembershipFunction("UP", 0.0, 5.0, 10.0, 10.0));
            clothes_dirt = new LinguisticVariable("DIRT", dirt);

            throttle = new MembershipFunctionCollection();
            throttle.Add(new MembershipFunction("LOW",0.0,0.0,2.0,4.0));
            throttle.Add(new MembershipFunction("LM", 2.0, 4.0, 4.0, 6.0));
            throttle.Add(new MembershipFunction("MED", 4.0, 6.0, 6.0, 8.0));
            throttle.Add(new MembershipFunction("HM", 6.0, 8.0, 8.0, 10.0));
            throttle.Add(new MembershipFunction("HIGH", 8.0, 10.0, 10.0, 10.0));
            mythrottle = new LinguisticVariable("THROTTLE", throttle);

            
        
        }

        public void setRules()
        {
          myrules = new FuzzyRuleCollection();
          myrules.Add(new FuzzyRule("IF (DETERGENT IS HIGH) AND (DIRT IS UP) THEN THROTTLE IS HIGH"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS HIGH) AND (DIRT IS LEVEL) THEN THROTTLE IS HIGH"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS HIGH) AND (DIRT IS DOWN) THEN THROTTLE IS HIGH"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS OK) AND (DIRT IS UP) THEN THROTTLE IS MED"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS OK) AND (DIRT IS LEVEL) THEN THROTTLE IS HM"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS OK) AND (DIRT IS DOWN) THEN THROTTLE IS HIGH"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS LOW) AND (DIRT IS UP) THEN THROTTLE IS MED"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS LOW) AND (DIRT IS LEVEL) THEN THROTTLE IS LM"));
          myrules.Add(new FuzzyRule("IF (DETERGENT IS LOW) AND (DIRT IS DOWN) THEN THROTTLE IS LOW"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(clothes_detergent);
            fe.LinguisticVariableCollection.Add(clothes_dirt);
            fe.LinguisticVariableCollection.Add(mythrottle);
            fe.FuzzyRuleCollection = myrules;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void defuziffyToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMembers();
            setRules();
            //setFuzzyEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clothes_detergent.InputValue=(Convert.ToDouble(textBox1.Text));
            clothes_detergent.Fuzzify("OK");
            
            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clothes_dirt.InputValue = (Convert.ToDouble(textBox2.Text));
            clothes_dirt.Fuzzify("LEVEL");
            
        }

        public void fuziffyvalues()
        {
            clothes_detergent.InputValue = (Convert.ToDouble(textBox1.Text));
            clothes_detergent.Fuzzify("LOW");
            clothes_dirt.InputValue = (Convert.ToDouble(textBox2.Text));
            clothes_dirt.Fuzzify("DOWN");
        
        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "THROTTLE";
            textBox3.Text = "" + fe.Defuzzify().ToString("0.00");
        }

        public void washclothes()
        {

            double detergent = Convert.ToDouble(textBox1.Text);
            double throttle = Convert.ToDouble(textBox3.Text);
            double dirt = Convert.ToDouble(textBox2.Text);
            double washed;

            double newDetergent = ((1 - 0.5) * (detergent)) + (throttle - (0.5 * dirt));

            if (dirt == 0)
                washed = 0;
            else
                washed = ((1 - 0.1) * (dirt)) + (detergent - (0.1 * throttle));

            textBox1.Text = "" + newDetergent.ToString("0.00");
            textBox4.Text = "" + washed.ToString("0.00");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            fe.Consequent = "THROTTLE";
            textBox3.Text = "" + fe.Defuzzify().ToString("0.00");
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            washclothes();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fuziffyvalues();
            defuzzy();
            washclothes();
        }

       
    }
}

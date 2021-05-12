using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dohvati();
        }
        private void dohvati()
        {
            listBox1.Items.Clear();

            AutoContext db = new AutoContext();

            foreach (var item in db.Automobili)
            {
                listBox1.Items.Add("ID : " + item.Id + " Proizvodac : " + item.Proizvodac + " Model - " + item.Model + " GOD - " + item.GodinaProizvodnje + " Boja - " + item.Boja + " Kotaci - " + item.BrojKotaca + " Stakla jesu potamljena - " + item.PotamljenaStakla);
            }
            listBox1.Items.Add("NEW...");
        }

        private void button1_Click(object sender, EventArgs e) //kreiraj
        {
            AutoContext db = new AutoContext();

            if (listBox1.SelectedItem == "NEW..." || listBox1.SelectedItem == null || listBox1.SelectedItem == "")
            {
                if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "" || textBox5.Text == null || textBox5.Text == "")
                {
                    MessageBox.Show("cant be empty");
                    return;
                }
                else
                {
                    int dasdasd = System.Convert.ToInt32(numericUpDown1.Value);

                    db.Automobili.Add(new Automobil()
                    {
                        Proizvodac = textBox1.Text,
                        Model = textBox2.Text,
                        Boja = textBox3.Text,
                        BrojKotaca = dasdasd,
                        GodinaProizvodnje = textBox5.Text,
                        PotamljenaStakla = checkBox1.Checked

                    });
                }

                db.SaveChanges();
                dohvati();
            }
            else
            {
                string[] words = listBox1.SelectedItem.ToString().Split(' ');

                int id = System.Convert.ToInt32(words[2]);

                var auto = db.Automobili.Find(id);

                auto.Proizvodac = textBox1.Text;
                auto.Model = textBox2.Text;
                auto.Boja = textBox3.Text;
                auto.BrojKotaca = System.Convert.ToInt32(numericUpDown1.Value);
                auto.GodinaProizvodnje = textBox5.Text;
                auto.PotamljenaStakla = checkBox1.Checked;


                db.SaveChanges();
                dohvati();
            }
            }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoContext db = new AutoContext();
            if (listBox1.SelectedItem != "NEW...")
            {
                string[] words = listBox1.SelectedItem.ToString().Split(' ');

                int id = System.Convert.ToInt32(words[2]);

                var auto = db.Automobili.Find(id);

                textBox1.Text = auto.Proizvodac;
                textBox2.Text = auto.Model;
                textBox3.Text = auto.Boja;
                numericUpDown1.Value = auto.BrojKotaca;
                textBox5.Text = auto.GodinaProizvodnje;
                checkBox1.Checked = auto.PotamljenaStakla;
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                numericUpDown1.Value = 4;
                textBox5.Text = "";
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)//filter button
        {
            AutoContext db = new AutoContext();

            listBox2.Items.Clear();

            foreach (var item in db.Automobili)
            {
                if (item.Proizvodac.ToLower() == textBox8.Text.ToLower() || item.Model.ToLower() == textBox7.Text.ToLower() || item.Boja.ToLower() == textBox6.Text.ToLower() || item.GodinaProizvodnje == textBox4.Text)
                {
                    listBox2.Items.Add("Proizvodac : " + item.Proizvodac + " Model - " + item.Model + " GOD - " + item.GodinaProizvodnje + " Boja - " + item.Boja + " Kotaci - " + item.BrojKotaca + " Stakla jesu potamljena - " + item.PotamljenaStakla);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AutoContext db = new AutoContext();
            foreach (var item in db.Automobili)
            {
                db.Automobili.Remove(item);
            }
            db.SaveChanges();
            dohvati();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                AutoContext db = new AutoContext();

                string[] words = listBox1.SelectedItem.ToString().Split(' ');

                int id = System.Convert.ToInt32(words[2]);


                var auto = db.Automobili.Find(id);
                db.Automobili.Remove(auto);
                db.SaveChanges();
                dohvati();
            }
            else
            {
                MessageBox.Show("lol");
            }
        }
    }
    }

    public class AutoContext : DbContext
    {
        public AutoContext()
        {

        }

        // Entities        
        public DbSet<Automobil> Automobili { get; set; }
    }
    public class Automobil
    {
    public int Id { get; set; }
    public string Model { get; set; }
        public string Proizvodac { get; set; }
        public string GodinaProizvodnje { get; set; }
        public string Boja { get; set; }
        public bool PotamljenaStakla { get; set; }
        public int BrojKotaca { get; set; }
    }






//string[] bojeArray = { "Crvena", "Plava", "Zelena", "Narancasta", "Bjela", "Crna", "Ljubicasta", "Zuta", "Tamno crvena", "Tamno plava"};
//string[] godProizArray = { "2021", "2020", "2019", "2018", "2017", "2016", "2015", "2014", "2013", "2012" };
//string[] modelArray = { "S", "i8", "R8", "a45", "Vitara", "Wrx", "xc90", "Arteon", "911", "Aventador" };
//string[] proizvodacArray = { "Tesla", "BMW", "Audi", "Mercedes", "Suzuki", "Subaru", "Volvo", "Volkswagen", "Ferrari", "Lamborghini" };
//bool stakloRes = true;


//for (int i = 0; i < 9; i++)
//{
//    Automobil a = new Automobil();
//    Random r = new Random();




//    int randInt = r.Next(1, 2);
//    if (randInt == 1)
//    {
//        stakloRes = true;
//    }
//    if (randInt == 2)
//    {
//        stakloRes = false;
//    }

//    db.Automobili.Add(new Automobil() { Boja = bojeArray[i], BrojKotaca = 4, GodinaProizvodnje = godProizArray[i], Model = modelArray[i], Proizvodac = proizvodacArray[i], PotamljenaStakla = stakloRes });


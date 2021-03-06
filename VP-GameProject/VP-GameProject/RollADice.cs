﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VP_GameProject.Properties;

namespace VP_GameProject
{
    public partial class RollADice : Form
    {
        public static Random random = new Random();
        public RollGame Game { get; set; }
        public SoundPlayer SoundPlayer;
        public RollADice()
        {
            InitializeComponent();
            lblYourName.Text = Form1.CurrPlayer.Name;
            changeMoney();
            SoundPlayer = new SoundPlayer();

        }
        public void changeMoney() {
            lblMoney.Text = Form1.CurrPlayer.Money + "$";
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            int money = 0;
            int.TryParse(tbBet.Text, out money);
            if (Form1.CurrPlayer.Money < money) return;
            int rollings = 11;
            int.TryParse(tbNumberRollings.Text, out rollings);
            Game = new RollGame(money, rollings);
            ((Button)sender).Enabled = false;
            Form1.CurrPlayer.Money -= money;
            changeMoney();
            rollIt.Start();
        }


        private void rollIt_Tick(object sender, EventArgs e)
        {
            if (!Game.Rolling()) {
                rollIt.Stop();
                btnGo.Enabled = true;
                Form1.CurrPlayer.Money += Game.GetMoney();
                lbl_earned.Text = "You earned "+ Game.GetMoney()+"$";
                if (Game.GetMoney() > 0)
                {
                    SoundPlayer.Stream = Resources.money;
                    SoundPlayer.Play();
                }
                changeMoney();

            }
            for (int i = 1; i <= 4; i++)
            {
                 PictureBox pb = (PictureBox)Controls.Find("pictureBox" + i, false)[0];

                pb.Image = (Image)VP_GameProject.Properties.Resources.ResourceManager.GetObject("Alea_" + Game.Results[i]);
            }
            pbMe.Image = (Image)VP_GameProject.Properties.Resources.ResourceManager.GetObject("Alea_" + Game.Results[0]);
        }
        
    }
}

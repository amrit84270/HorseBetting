using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HorseBetting

{
    public partial class frmBetting : Form
    {
        public frmBetting()
        {
            InitializeComponent();
        }

        private Punter []PuntersList = null;
        private GreyHound []HorsesList = null;
        private int flag = 0;
        private bool enableRaceBtn = false;
        
        public void FillAll()
        {
            Random myRandom = new Random();


        // list the name of persons. 
            PuntersList = new Punter[3];
            
            PuntersList[0] = new Mark();
            PuntersList[0].MyLabel = Punter1desc;
            PuntersList[0].MyRadioButton = Punter1;

            PuntersList[1] = new Stanley();
            PuntersList[1].MyLabel = Punter2desc;
            PuntersList[1].MyRadioButton = Punter2;

            PuntersList[2] = new Cristina();
            PuntersList[2].MyLabel = Punter3desc;
            PuntersList[2].MyRadioButton = Punter3;

            // The list of horses and information about racing track.
            HorsesList = new GreyHound[4]
            {
                new GreyHound() 
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Horse1
                },

                new GreyHound()
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Horse2
                },

                new GreyHound() 
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Horse3
                },

                new GreyHound() 
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Horse4
                }
            };

            for (int i = 0; i < PuntersList.Length; i++)
            {
                PuntersList[i].MyBet.Bettor = PuntersList[i];
                PuntersList[i].UpdateLabel();
            }
            PlaceHorsePicturesAtStart();            
        }

        private void frmBetting_Load(object sender, EventArgs e)
        {
            try
            {
                // Maximum limit and value of bucks.
                if (numBucks.Value == 1)
                   

                FillAll();
                
                if (!this.enableRaceBtn)
                    btnRace.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            maxlbl.Text = "Maximum Limit : 50";
        }

        private void Punter1_CheckedChanged(object sender, EventArgs e)
        {
            if (Punter1.Checked)
            {
                this.flag = 1;
                PunterName.Text = this.PuntersList[0].Name;
            }
        }

        private void Punter2_CheckedChanged(object sender, EventArgs e)
        {
            if (Punter2.Checked)
            {
                this.flag = 2;
                PunterName.Text = this.PuntersList[1].Name;
            }
        }

        private void Punter3_CheckedChanged(object sender, EventArgs e)
        {
            if (Punter3.Checked)
            {
                this.flag = 3;
                PunterName.Text = this.PuntersList[2].Name;
            }
        }

        public void BetBtnWorking()
        {            
            int bucksNumber = 0;
            int HorseNumber = 0;

            if (!Punter1.Checked && !Punter2.Checked && !Punter3.Checked)
            {
                MessageBox.Show("You must choose atleast one guy to place bet.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bucksNumber = Convert.ToInt32(numBucks.Value);
            HorseNumber = Convert.ToInt32(HorseNo.Value);

            if (IsExceedBetLimit(bucksNumber))
            {
                MessageBox.Show("You can't put bucks greater than 50 on Horse.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            enableRaceBtn = true; // if at least one bet is placed enable race button then

            if (this.flag == 1)
            {
                this.PuntersList[0].PlaceBet(bucksNumber, HorseNumber);
            }
            else if (this.flag == 2)
            {
                this.PuntersList[1].PlaceBet(bucksNumber, HorseNumber);
            }
            else if (this.flag == 3)
            {
                this.PuntersList[2].PlaceBet(bucksNumber, HorseNumber);
            }            
        }

        private void betbtn_Click(object sender, EventArgs e)
        {
            try
            {
                BetBtnWorking();

                if (this.enableRaceBtn)
                    btnRace.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
          // The total limit of cash in race.
        public bool IsExceedBetLimit(int amount)
        {
            if (amount >= 50 && amount > 1)
                return true;

            return false;
        }
        public void RaceButtonWorking()
        {
            betbtn.Enabled = false;
            btnRace.Enabled = false;

            bool winnerHorseFlag = false;
            int winningHorseNo = 0;            

            while (!winnerHorseFlag)
            {
                for (int i = 0; i < HorsesList.Length; i++)
                {
                    if (this.HorsesList[i].Run())
                    {
                        winnerHorseFlag = true;
                        winningHorseNo = i;
                    }
                  
                    Track.Refresh();                 
                }                
            }
            // At the end of race and show who win the bet. 

            MessageBox.Show("We have a winner - Horse # " + (winningHorseNo + 1) + "!", "Race Over");

            for (int j = 0; j < PuntersList.Length; j++)
            {
                this.PuntersList[j].Collect(winningHorseNo + 1);
                this.PuntersList[j].ClearBet(); // clearing all bets
            }

            PlaceHorsePicturesAtStart();

            betbtn.Enabled = true;       
        }

        public void PlaceHorsePicturesAtStart()
        {
            for (int k = 0; k < HorsesList.Length; k++)
                HorsesList[k].TakeStartingPosition();  
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            try
            {
                RaceButtonWorking();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Horse3_Click(object sender, EventArgs e)
        {

        }

        private void maxlbl_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

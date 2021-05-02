using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HorseBetting
{
    public abstract class Punter
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _cash;

        public int Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        private Bet _myBet;

        public Bet MyBet
        {
            get { return _myBet; }
            set { _myBet = value; }
        }

        private RadioButton RadioButton;

        public RadioButton MyRadioButton
        {
            get { return RadioButton; }
            set { RadioButton = value; }
        }

        private Label myLabel;

        public Label MyLabel
        {
            get { return myLabel; }
            set { myLabel = value; }
        }

        public void UpdateLabel()
        {
            this.RadioButton.Text = this._name + " has " + this._cash.ToString() + " bucks";
            this.myLabel.Text = this._myBet.GetDescription();
        }

        public void Collect(int winningHorseNo)
        {
            if (this._cash > 0)
                this._cash += this._myBet.Payout(winningHorseNo);
        }

        public void ClearBet()
        {            
            this._myBet.Amount = 0;
            this.RadioButton.Text = this._name + " has " + this._cash + " bucks";
            this.myLabel.Text = this._name + " hasn't placed any bet"; 
        }

        public bool PlaceBet(int amount, int HorseNumber)
        {
            if (amount < this._cash)
            {
                this._myBet = new Bet() { Amount = amount, Horse = HorseNumber, Bettor = this };
                UpdateLabel();
                return true;
            }   

            return false;
        }
    }
}

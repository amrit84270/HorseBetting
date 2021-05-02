using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HorseBetting
{
    public class GreyHound
    {        
        private int start;

        public int StartingPosition
        {
            get { return start; }
            set { start = value; }
        }

        private int TrackLength;

        public int RaceTrackLength
        {
            get { return TrackLength; }
            set { TrackLength = value; }
        }

        private int loc;

        public int Location
        {
            get { return loc; }
            set { loc = value; }
        }    

        private PictureBox PictureBox;       

        public PictureBox MyPictureBox
        {
            get { return PictureBox; }
            set { PictureBox = value; }
        }

        private Random Random;

        public Random MyRandom
        {
            get { return Random; }
            set { Random = value; }
        }
       
        public bool Run()
        {
            int randomDistance = this.Random.Next(1, 4);
            this.loc += randomDistance;

            Point p = this.PictureBox.Location;

            if (p.X > this.TrackLength)
            {
                return true;
            }
            else
            {
                p.X += randomDistance;
                this.PictureBox.Location = p;

                return false;
            }
        }
  
        public void TakeStartingPosition()
        {                  
            this.loc = this.start;

            Point p = this.PictureBox.Location;
            p.X = Location;
            this.PictureBox.Location = p;
        }
    }
}

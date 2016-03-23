using System;
using System.Drawing;
using System.Windows.Forms;

namespace zadanie3 {

    public partial class Form1 : Form {
        private float circlePositionX = 150;
        private float circlePositionY = 150;
        private float enemySpeedX = 10;
        private float enemySpeedY = 10;
        private float firstEnemyPositionX = 400;
        private float firstEnemyPositionY = 60;
        private float positionChangeX;
        private double positionChangeY;
        private float speedX = 10;
        private float speedY = 10;

        public Form1() {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = ConstantsHelper.TimerInterval;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawRectangle(Pens.Black, 50, 50, 700, 450);

            e.Graphics.FillEllipse(Brushes.SpringGreen, circlePositionX, circlePositionY, ConstantsHelper.BallSize,
                ConstantsHelper.BallSize);
            e.Graphics.FillEllipse(Brushes.Red, firstEnemyPositionX, firstEnemyPositionY, ConstantsHelper.BallSize,
                ConstantsHelper.BallSize);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            circlePositionX += speedX;
            positionChangeX += speedX;
            circlePositionY += speedY;
            positionChangeY += speedY;

            firstEnemyPositionX += enemySpeedX;
            firstEnemyPositionY += enemySpeedY;

            //bounce - wall
            if (circlePositionX <= ConstantsHelper.BoxLeftBorder) {
                speedX = (float) (speedX*(Math.Cos(ConstantsHelper.Angle)));
            }
            else if (circlePositionY <= ConstantsHelper.BoxTopBorder) {
                speedY = (float) (speedY*(Math.Cos(ConstantsHelper.Angle)));
            }
            else if (circlePositionX >= ConstantsHelper.BoxRightBorder) {
                speedX = (float) (speedX*(Math.Cos(ConstantsHelper.Angle)));
            }
            else if (circlePositionY >= ConstantsHelper.BoxBottomBorder) {
                speedY = (float) (speedY*(Math.Cos(ConstantsHelper.Angle)));
            }

            //enemy bounce - wall
            if (firstEnemyPositionX <= ConstantsHelper.BoxLeftBorder) {
                enemySpeedX = (float) (enemySpeedX*(Math.Cos(ConstantsHelper.Angle)));
            }
            else if (firstEnemyPositionY <= ConstantsHelper.BoxTopBorder) {
                enemySpeedY = (float) (enemySpeedY*(Math.Cos(ConstantsHelper.Angle)));
            }
            else if (firstEnemyPositionX >= ConstantsHelper.BoxRightBorder) {
                enemySpeedX = (float) (enemySpeedX*(Math.Cos(ConstantsHelper.Angle)));
            }
            else if (firstEnemyPositionY >= ConstantsHelper.BoxBottomBorder) {
                enemySpeedY = (float) (enemySpeedY*(Math.Cos(ConstantsHelper.Angle)));
            }

            //hit enemy
            //if (Math.Abs(circlePositionX - firstEnemyPositionX) < 100 && Math.Abs(circlePositionY - firstEnemyPositionY) < 100) {
            //    //speedX = speedX*-1;
            //    speedY = speedY*-1;
            //    enemySpeedX = enemySpeedX*-1;
            //    enemySpeedY = enemySpeedY*-1;
            //}

            Invalidate();

            #region debug info

            debugInfo.Text = string.Format(
                "debug info:" + Environment.NewLine +
                "circle pos x = {0}," + Environment.NewLine +
                "circle pos y = {1}," + Environment.NewLine +
                "enemy circle pos x = {10}," + Environment.NewLine +
                "enemy circle pos y = {11}," + Environment.NewLine +
                "number of steps = {2}" + Environment.NewLine +
                "positionChangeX = {3}," + Environment.NewLine +
                "positionChangeY = {4}," + Environment.NewLine +
                "speed X = {5}" + Environment.NewLine +
                "speed Y = {6}" + Environment.NewLine +
                "enemy speed X = {12}" + Environment.NewLine +
                "enemy speed Y = {13}" + Environment.NewLine +
                "bounce angle = {7}" + Environment.NewLine +
                "window height = {8}," + Environment.NewLine +
                "window width = {9}",
                circlePositionX + positionChangeX, circlePositionY + positionChangeY, 0, positionChangeX,
                positionChangeY, speedX, speedY, ConstantsHelper.Angle, Height, Width, firstEnemyPositionX, firstEnemyPositionY, enemySpeedX, enemySpeedY);

            #endregion
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == ' ') {
                if (ConstantsHelper.IsStopped) {
                    timer1.Start();
                    ConstantsHelper.IsStopped = false;
                }
                else {
                    timer1.Stop();
                    ConstantsHelper.IsStopped = true;
                }
            }
        }
    }

}
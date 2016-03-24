using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace zadanie3 {
    public partial class Form1 : Form {
        private readonly List<EnemyBall> balls;
        private float circlePositionX = 150;
        private float circlePositionY = 150;
        private float positionChangeX;
        private float positionChangeY;
        private float speedX = 20;
        private float speedY = 20;

        public Form1() {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = ConstantsHelper.TimerInterval;
            timer1.Start();
            balls = EnemyBall.CreateEnemies();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawRectangle(Pens.Black, ConstantsHelper.BoxLeftBorder,
                ConstantsHelper.BoxTopBorder,
                ConstantsHelper.BoxRightBorder + ConstantsHelper.BoxLeftBorder,
                ConstantsHelper.BoxBottomBorder + ConstantsHelper.BoxTopBorder);

            e.Graphics.FillEllipse(Brushes.SpringGreen, circlePositionX, circlePositionY, ConstantsHelper.BallSize,
                ConstantsHelper.BallSize);

            foreach (var enemyBall in balls) {
                e.Graphics.FillEllipse(Brushes.Red, enemyBall.PositionX, enemyBall.PositionY,
                    enemyBall.Size == 0 ? ConstantsHelper.BallSize : enemyBall.Size,
                    enemyBall.Size == 0 ? ConstantsHelper.BallSize : enemyBall.Size);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            circlePositionX += speedX;
            positionChangeX += speedX;
            circlePositionY += speedY;
            positionChangeY += speedY;

            foreach (var enemyBall in balls) {
                enemyBall.PositionX -= enemyBall.SpeedX;
                enemyBall.PositionY += enemyBall.SpeedY;
            }

            MainBallBounceOffTheWalls();
            EnemyBallsBounceOffTheWalls();
            //MainBallBounceOffEnemyBalls();

            UpdateDebugInfoTextBox();

            Invalidate();
            
        }

        private void MainBallBounceOffEnemyBalls() {
            foreach (var enemyBall in balls) {
                if (Math.Abs(circlePositionX - enemyBall.PositionX) < ConstantsHelper.BallSize &&
                    Math.Abs(circlePositionY - enemyBall.PositionY) < ConstantsHelper.BallSize) {
                    speedX = speedX*1;
                    speedY = speedY*-1;
                    enemyBall.SpeedX = enemyBall.SpeedX*-1;
                    enemyBall.SpeedY = enemyBall.SpeedY*-1;
                }
            }
        }

        private void UpdateDebugInfoTextBox() {
            debugInfo.Text = string.Format(
                "debug info:" + Environment.NewLine +
                "circle pos x = {0}," + Environment.NewLine +
                "circle pos y = {1}," + Environment.NewLine +
                "number of steps = {2}" + Environment.NewLine +
                "positionChangeX = {3}," + Environment.NewLine +
                "positionChangeY = {4}," + Environment.NewLine +
                "speed X = {5}" + Environment.NewLine +
                "speed Y = {6}" + Environment.NewLine +
                "bounce angle = {7}" + Environment.NewLine +
                "window height = {8}," + Environment.NewLine +
                "window width = {9}",
                circlePositionX + positionChangeX, circlePositionY + positionChangeY, 0, positionChangeX,
                positionChangeY, speedX, speedY, ConstantsHelper.Angle, Height, Width);
        }

        private void EnemyBallsBounceOffTheWalls() {
            //enemy bounce - wall
            foreach (var enemyBall in balls) {
                if (enemyBall.PositionX <= ConstantsHelper.BoxLeftBorder) {
                    enemyBall.SpeedX = (float) (enemyBall.SpeedX*(Math.Cos(ConstantsHelper.Angle)));
                }
                if (enemyBall.PositionY <= ConstantsHelper.BoxTopBorder) {
                    enemyBall.SpeedY = (float) (enemyBall.SpeedY*(Math.Cos(ConstantsHelper.Angle)));
                }
                if (enemyBall.PositionX >= ConstantsHelper.BoxRightBorder) {
                    enemyBall.SpeedX = (float) (enemyBall.SpeedX*(Math.Cos(ConstantsHelper.Angle)));
                }
                if (enemyBall.PositionY >= ConstantsHelper.BoxBottomBorder) {
                    enemyBall.SpeedY = (float) (enemyBall.SpeedY*(Math.Cos(ConstantsHelper.Angle)));
                }
            }
        }

        private void MainBallBounceOffTheWalls() {
            //bounce - wall
            if (circlePositionX <= ConstantsHelper.BoxLeftBorder) {
                speedX = (float) (speedX*(Math.Cos(ConstantsHelper.Angle)));
            }
            if (circlePositionY <= ConstantsHelper.BoxTopBorder) {
                speedY = (float) (speedY*(Math.Cos(ConstantsHelper.Angle)));
            }
            if (circlePositionX >= ConstantsHelper.BoxRightBorder) {
                speedX = (float) (speedX*(Math.Cos(ConstantsHelper.Angle)));
            }
            if (circlePositionY >= ConstantsHelper.BoxBottomBorder) {
                speedY = (float) (speedY*(Math.Cos(ConstantsHelper.Angle)));
            }
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
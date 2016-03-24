using System;
using System.Collections.Generic;

namespace zadanie3 {
    public class EnemyBall {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public int Size { get; set; }

        public static List<EnemyBall> CreateEnemies() {
            var rand = new Random();
            var result = new List<EnemyBall>();
            for (var i = 1; i < 5; i++) {
                var ball = new EnemyBall {
                    PositionX = ConstantsHelper.BoxLeftBorder + rand.Next(ConstantsHelper.BoxRightBorder - ConstantsHelper.BoxLeftBorder),
                    PositionY = ConstantsHelper.BoxTopBorder + rand.Next(ConstantsHelper.BoxBottomBorder - ConstantsHelper.BoxTopBorder),
                    SpeedX = (float) (Math.Pow(-1, i)*10),
                    SpeedY = (float) (Math.Pow(-1, i)*10),
                    //Size = i*5
                };
                result.Add(ball);
            }
            return result;
        }
    }
}
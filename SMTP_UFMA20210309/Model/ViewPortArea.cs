using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP_UFMA20210309.Model {
    class ViewPortArea {

        #region Fields, Properties and Variables
        private double xMin;
        public double XMin {
            get { return xMin; }
            set { xMin = value; }
        }

        private double xMax;
        public double XMax {
            get { return xMax; }
            set { xMax = value; }
        }

        private double yMin;
        public double YMin {
            get { return yMin; }
            set { yMin = value; }
        }

        private double yMax;
        public double YMax {
            get { return yMax; }
            set { yMax = value; }
        }

        private double roverX;
        public double RoverX {
            get { return roverX; }
            set { roverX = value; }
        }

        private double roverY;
        public double RoverY {
            get { return roverY; }
            set { roverY = value; }
        }

        private double targetX;
        public double TargetX {
            get { return targetX; }
            set { targetX = value; }
        }

        private double targetY;
        public double TargetY {
            get { return targetY; }
            set { targetY = value; }
        }
        #endregion

        #region Constructors
        public ViewPortArea() {
        }

        public ViewPortArea(double xMin, double xMax, double yMin, double yMax) {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
        #endregion

        #region Normalize
        public double XNormalize(double x, double viewPortWidth) {
            double result = (x - xMin) * viewPortWidth / (xMax - xMin);
            return result;
        }

        public double YNormalize(double y, double viewPortHeight) {
            double result = viewPortHeight - (y - yMin) * viewPortHeight / (yMax - yMin);
            return result;
        }
        #endregion

        public double PathDistance() {
            double distance = Math.Sqrt(Math.Pow(TargetY-RoverY,2)+ Math.Pow(TargetX - RoverX, 2));
            return distance;
        }

        public void ForwardRover(double deltaX) {
            double m = (TargetY - RoverY) / (TargetX - RoverX);
            double b = TargetY - (m * TargetX);
            RoverX = RoverX + deltaX;
            RoverY = (m * RoverX) + b;
        }

        // ToDo: Implement ToString

    }
}

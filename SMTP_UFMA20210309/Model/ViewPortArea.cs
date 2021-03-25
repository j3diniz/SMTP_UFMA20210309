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

        // ToDo: Implement ToString

    }
}

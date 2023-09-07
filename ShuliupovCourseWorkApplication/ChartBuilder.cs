using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShuliupovCourseWorkApplication
{
    
        public partial class ChartBuilder : Form
        {
            public ChartBuilder(CourseWorkClassLib.Point[] points1, string f, CourseWorkClassLib.Point[] points2, string g)               
            {
                InitializeComponent();                                 
                chart1.Series.Clear();                                

                chart1.Series.Add(f);
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line ;  
                
                chart1.Series[0].Color = Color.Red;
                chart1.Series[0].BorderWidth = 3;
                foreach (CourseWorkClassLib.Point point in points1)                      
                    chart1.Series[0].Points.AddXY(point.X, point.Y);

                chart1.Series.Add(g);                                
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.Series[1].Color = Color.Black;
                chart1.Series[1].BorderWidth = 3;
                foreach (CourseWorkClassLib.Point point in points2)                      
                    chart1.Series[1].Points.AddXY(point.X, point.Y);
            }
        }
}

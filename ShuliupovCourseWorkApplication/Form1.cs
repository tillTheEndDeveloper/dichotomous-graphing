using System;
using CourseWorkClassLib;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShuliupovCourseWorkApplication
{
    public partial class Form1 : Form
    {
        public class StateBlock                             
        {
            public Polynom polynom;                        
            public PointsList pointslist;                
            public double a;                               
            public double b;                           

            public StateBlock() { }
        }
            
        private Polynom polynom = new Polynom();          
        private PointsList pointsList = new PointsList();

        private int pointsListCount = 3;                  
        private int XlistCount = 4;

        private bool open = false;

        dynamic XDichotomy;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.Rows.Add(0, 0, 0, 1);     
            dataGridView1.Rows[0].HeaderCell.Value = "X^";  

            dataGridView2.Rows.Add(-11, 0, 11);         
            dataGridView2.Rows.Add(-26, -4, 18);         
            dataGridView2.Rows[0].HeaderCell.Value = "X"; 
            dataGridView2.Rows[1].HeaderCell.Value = "Y";

            UpdatePolynomDegreeFunction();
            UpdatePointsListFunction();

        }
        
        private void UpdatePolynomDegreeFunction() 
        {
            if (dataGridView1.Columns.Count == 0)
                return;
            if (dataGridView1.Rows.Count == 0)
                return;
            polynom.Clear();
            for (int i = 0; i < XlistCount; i++)
            {
                if (dataGridView1.Rows[0].Cells[i].Value != null)
                    polynom.Push(
                       new XdegreePoint(double.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString(), new CultureInfo("en-us"))));                
            }
        }

        private void UpdatePointsListFunction()
        {
            if (dataGridView2.Columns.Count == 0)
                return;
            if (dataGridView2.Rows.Count == 0)
                return;
            pointsList.Clear();
            for (int i = 0; i < pointsListCount; i++)
                if (dataGridView2.Rows[0].Cells[i].Value != null && dataGridView2.Rows[1].Cells[i].Value != null)
                    pointsList.Push(
                        new CourseWorkClassLib.Point(
                            double.Parse(dataGridView2.Rows[0].Cells[i].Value.ToString(), new CultureInfo("en-us")),
                            double.Parse(dataGridView2.Rows[1].Cells[i].Value.ToString(), new CultureInfo("en-us"))));
        }


        

        private void button2_Click(object sender, System.EventArgs e)
        {
            XlistCount++;
            dataGridView1.Columns.Add((XlistCount - 1).ToString(), (XlistCount - 1).ToString());
            dataGridView1.Rows[0].Cells[XlistCount - 1].Value = 0;
            UpdatePolynomDegreeFunction();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            pointsListCount++;
            dataGridView2.Columns.Add(pointsListCount.ToString(), pointsListCount.ToString());
            dataGridView2.Rows[0].Cells[pointsListCount - 1].Value = 0;
            dataGridView2.Rows[1].Cells[pointsListCount - 1].Value = 0;
            UpdatePointsListFunction();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (XlistCount == 1)
                return;
            XlistCount--;
            dataGridView1.Columns.RemoveAt(XlistCount);
            UpdatePolynomDegreeFunction();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            if (pointsListCount == 1)
                return;
            pointsListCount--;
            dataGridView2.Columns.RemoveAt(pointsListCount);
            UpdatePointsListFunction();
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            UpdatePolynomDegreeFunction();
            UpdatePointsListFunction();

            Calculations calculator = new Calculations(polynom, pointsList, (a, b) =>
            a - b);

            try
            {
                List<CourseWorkClassLib.Point> f = new List<CourseWorkClassLib.Point>();
                List<CourseWorkClassLib.Point> g = new List<CourseWorkClassLib.Point>();

                for (double x = (double)numericUpDown1.Value; x <= (double)numericUpDown2.Value; x += 0.5)
                {
                    f.Add(new CourseWorkClassLib.Point(x, polynom.GetSolution(x)));
                    g.Add(new CourseWorkClassLib.Point(x, pointsList.GetSolution(x)));
                }

                ChartBuilder ch1 = new ChartBuilder(f.ToArray(), "f(x)", g.ToArray(), "g(x)");   
                ch1.Show(this);                                 

                XDichotomy = calculator.GetSolutionDichotomy((double)numericUpDown1.Value, (double)numericUpDown2.Value, 0.001);
                textBox2.Text = XDichotomy.ToString();
            }
            catch (ArgumentException ex)
            {
                XDichotomy = "No root";
                textBox2.Text = XDichotomy.ToString();
            }
                SaveFileDialog saveFileDialog = new SaveFileDialog();      
                saveFileDialog.Filter = "HTML File | *.html";              
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("<html>");
                        writer.WriteLine("<head>");
                        writer.WriteLine("<title>Xисельне знаходження коренів рівняння методом дихотомії.</title>");
                        writer.WriteLine("</head>");
                        writer.WriteLine("<body>");
                        writer.WriteLine("<h2>Звіт</h2>");
                        writer.WriteLine("<p>У результаті знаходження максимуму функції методом Фібоначчі, з наступними вихідними даними:</p>");
                        writer.WriteLine("<p>F(x):</p>");
                        writer.WriteLine(polynom);

                        writer.WriteLine("<p>G(x):</p>");
                        writer.WriteLine("<table border='1' cellpadding=4 cellspacing=0>");
                        writer.WriteLine("<tr>");
                        writer.WriteLine("<th>#</th>");
                        writer.WriteLine("<th>X</th>");
                        writer.WriteLine("<th>Y</th>");
                        writer.WriteLine("</tr>");
                        for (int i = 0; i < pointsList.Count(); i++)
                        {
                            writer.WriteLine("<tr>");
                            writer.WriteLine("<td>" + (i + 1) + "</td>");
                            writer.WriteLine("<td>" + pointsList.At(i).X + "</td>");
                            writer.WriteLine("<td>" + pointsList.At(i).Y + "</td>");
                            writer.WriteLine("</tr>");
                        }
                        writer.WriteLine("</table>");
                        writer.WriteLine("<p>Знайдений корінь:</p>");
                        writer.WriteLine("<p>X = " + XDichotomy + "</p>");
                        writer.WriteLine("</body>");
                    }
                }
            
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
                XmlSerializer serializer = new XmlSerializer(typeof(StateBlock));
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Document (*.xml)|*.xml";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog.FileName);
                    StateBlock stateBlock = new StateBlock();
                    stateBlock.polynom = polynom;
                    stateBlock.pointslist = pointsList;
                    stateBlock.a = (double)numericUpDown1.Value;
                    stateBlock.b = (double)numericUpDown2.Value;
                    serializer.Serialize(writer, stateBlock);
                    writer.Close();
                }            
            
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
                XmlSerializer serializer = new XmlSerializer(typeof(StateBlock));
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML Document (*.xml)|*.xml";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader reader = new StreamReader(openFileDialog.FileName);
                    StateBlock stateBlock = serializer.Deserialize(reader) as StateBlock;
                    reader.Close();
                    if (stateBlock != null)
                    {
                        open = true;

                        XlistCount = stateBlock.polynom.Count();
                        pointsListCount = stateBlock.pointslist.Count();

                        dataGridView1.Columns.Clear();
                        for (int i = 0; i < stateBlock.polynom.Count(); i++)
                            dataGridView1.Columns.Add((i).ToString(), (i).ToString());
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows.Add();
                        for (int i = 0; i < stateBlock.polynom.Count(); i++)
                        {
                            dataGridView1.Rows[0].Cells[i].Value = (decimal)stateBlock.polynom.At(i).X;
                        }

                        dataGridView2.Columns.Clear();
                        for (int i = 0; i < stateBlock.pointslist.Count(); i++)
                            dataGridView2.Columns.Add((i + 1).ToString(), (i + 1).ToString());
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows.Add();
                        for (int i = 0; i < stateBlock.pointslist.Count(); i++)
                        {
                            dataGridView2.Rows[0].Cells[i].Value = (decimal)stateBlock.pointslist.At(i).X;
                            dataGridView2.Rows[1].Cells[i].Value = (decimal)stateBlock.pointslist.At(i).Y;
                        }

                        polynom = stateBlock.polynom;
                        pointsList = stateBlock.pointslist;
                        numericUpDown1.Value = (decimal)stateBlock.a;
                        numericUpDown2.Value = (decimal)stateBlock.b;
                        open = false;
                    }
                }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (open || e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            double v;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || !double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0.0;
            UpdatePolynomDegreeFunction();
        }


        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (open || e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            double v;
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || !double.TryParse(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0.0;
            UpdatePointsListFunction();
        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            About form = new About();
            form.Show();
        }
    }
}

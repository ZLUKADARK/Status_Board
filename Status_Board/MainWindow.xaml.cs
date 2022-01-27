using Microsoft.Data.SqlClient;
using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

using System.Windows.Media;
using System.Windows.Controls;

namespace Status_Board
{

    public partial class MainWindow : Window

    {
        public static string connectString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Status_Board;Integrated Security=True";
        readonly SqlConnection connection = new SqlConnection(connectString);

        static Random rand = new Random();
        static string caldate;


        public MainWindow()
        {
            InitializeComponent();
            
            connection.Open();
            
            System.Windows.Threading.DispatcherTimer timer2 = new System.Windows.Threading.DispatcherTimer();
            timer2.Tick += new EventHandler(switchCompressor);
            timer2.Interval = new TimeSpan(0, 0, 1);
            timer2.Start();

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Tick += new EventHandler(timerChart);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();



        }
        public void ExitApp(object sender, ExitEventArgs e)
        {
            // заркываем соединение с БД
            connection.Close();
            cartChart.AxisX.Clear();
            cartChart.AxisY.Clear();
        }


        public void switchCompressor(object sender, EventArgs e)
        {
            if (a1check.IsChecked.Equals(false))
            {
                a1slide.Value = 0;
                a1slide.IsEnabled = false;
            }
            else
            {
                a1slide.IsEnabled = true;
            }
            if (a2check.IsChecked.Equals(false))
            {
                a2slide.Value = 0;
                a2slide.IsEnabled = false;
            }
            else
            {
                a2slide.IsEnabled = true;
            }
            if (a3check.IsChecked.Equals(false))
            {
                a3slide.Value = 0;
                a3slide.IsEnabled = false;
            }
            else
            {
                a3slide.IsEnabled = true;
            }
            if (a4check.IsChecked.Equals(false))
            {
                a4slide.Value = 0;
                a4slide.IsEnabled = false;
            }
            else
            {
                a4slide.IsEnabled = true;
            }
        }

        public void timerTick(object sender, EventArgs e)
        {
            //Значение компрессоров
            double A1 = a1slide.Value;
            double A2 = a2slide.Value;
            double A3 = a3slide.Value;
            double A4 = a4slide.Value;

            
            Compressor(A1, A2, A3, A4);

            //Значение давления в цехах
            double azot = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) - 1, 2);
            double h = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) - 1, 2);
            double compress = Math.Round((A1 + A2 + A3 + A4), 4); 
            double ctfs_pech = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) - 1, 2);
            double ctfs_perezhim = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) - 1, 2);
            double ctfs_reznoe = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) - 1, 2);
            double dsc_gal = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) -1, 2);
            double dsc_ramp = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) -1, 2);
            double dsc_drob = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) -1, 2);
            double dsc_shihta = Math.Round(((rand.NextDouble() * (10 - 0) + 0)) -1, 2);

            Departments(azot, h, compress, ctfs_pech, ctfs_perezhim, ctfs_reznoe, dsc_gal, dsc_ramp, dsc_drob, dsc_shihta);

            
        }

        public void Departments(double azot, double h, double compress, double ctfs_pech, double ctfs_perezhim, 
                                double ctfs_reznoe, double dsc_gal, double dsc_ramp, double dsc_drob, double dsc_shihta) 
        {
            //Вывод на экран значений ЦЕХОВ
            compressc.Content = compress;
            ctfs_perezhimc.Content = ctfs_perezhim;
            ctfs_pechc.Content = ctfs_pech;
            ctfs_reznoec.Content = ctfs_reznoe;
            dsc_galc.Content = dsc_gal;
            dsc_rampc.Content = dsc_ramp;
            dsc_drobc.Content = dsc_drob;
            dsc_shihtac.Content = dsc_shihta;
            hc.Content = h;
            azotc.Content = azot;
            //Запрос в таблицу с отделами
            string query = "INSERT Departments(DATETIME, Compress, CTFS_Perezhim, CTFS_Pech, CTFS_Reznoe, DSC_Gal, DSC_Ramp, DSC_Drob, DSC_Shihta, H, AZOT) " 
                         + "VALUES(SYSDATETIME(), "
                         + Convert.ToString(compress).Replace(",", ".") + "," 
                         + Convert.ToString(ctfs_perezhim).Replace(",", ".") + "," 
                         + Convert.ToString(ctfs_pech).Replace(",", ".") + "," 
                         + Convert.ToString(ctfs_reznoe).Replace(",", ".") + "," 
                         + Convert.ToString(dsc_gal).Replace(",", ".") + "," 
                         + Convert.ToString(dsc_ramp).Replace(",", ".") + "," 
                         + Convert.ToString(dsc_drob).Replace(",", ".") + "," 
                         + Convert.ToString(dsc_shihta).Replace(",", ".") + "," 
                         + Convert.ToString(h).Replace(",", ".") + "," 
                         + Convert.ToString(azot).Replace(",", ".") + ")";

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void Compressor(double A1, double A2, double A3, double A4) 
        {
            //Вывод на экран значений компрессоров
            pressA1.Content = A1;
            pressA2.Content = A2;
            pressA3.Content = A3;
            pressA4.Content = A4;
       
            //Строка запроса
            string query = "INSERT A1(DATETIME, NAME, PRESS) VALUES(SYSDATETIME(), 'A1'," + Convert.ToString(A1).Replace(",", ".") + ")"+";" 
                         + "INSERT A2(DATETIME, NAME, PRESS) VALUES(SYSDATETIME(), 'A2'," + Convert.ToString(A2).Replace(",", ".") + ")"+";" 
                         + "INSERT A3(DATETIME, NAME, PRESS) VALUES(SYSDATETIME(), 'A3'," + Convert.ToString(A3).Replace(",", ".") + ")"+";" 
                         + "INSERT A4(DATETIME, NAME, PRESS) VALUES(SYSDATETIME(), 'A4'," + Convert.ToString(A4).Replace(",", ".") + ")";          

            // Создаем объект SqlDbCommand для выполнения запроса к БД 
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
           
            //Вывод времени на экран
            string datequery = "SELECT SYSDATETIME() FROM A1";
            SqlCommand command3 = new SqlCommand(datequery, connection);
            dateA1.Content = command3.ExecuteScalar().ToString();
        }

        public void GraphButton_Click(object sender, RoutedEventArgs e)
        {

            LiveChart();

        }
      
        public void LiveChart()
        {

            string query1 ="SELECT DATETIME, Compress, CTFS_Pech, CTFS_Perezhim, CTFS_Reznoe, DSC_Gal, DSC_Ramp, DSC_Drob, DSC_Shihta, H, AZOT FROM Departments";
            string query2 = "SELECT DATETIME, Compress, CTFS_Pech, CTFS_Perezhim, CTFS_Reznoe, DSC_Gal, DSC_Ramp, DSC_Drob, DSC_Shihta, H, AZOT FROM Departments where convert(date, DATETIME) = ' " + caldate + "'";
           

            List<string> dates = new List<string>();
            ChartValues<double> compress = new ChartValues<double>();
            ChartValues<double> ctfs_pech = new ChartValues<double>();
            ChartValues<double> ctfs_perezhim = new ChartValues<double>();
            ChartValues<double> ctfs_reznoe = new ChartValues<double>();
            ChartValues<double> dsc_gal = new ChartValues<double>();
            ChartValues<double> dsc_ramp = new ChartValues<double>();
            ChartValues<double> dsc_drob = new ChartValues<double>();
            ChartValues<double> dsc_shihta = new ChartValues<double>();
            ChartValues<double> h = new ChartValues<double>();
            ChartValues<double> azot = new ChartValues<double>();

            //Получаем результаты времени
            if (datecheck.IsChecked.Equals(false))
            {
                SqlCommand com = new SqlCommand(query1, connection);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dates.Add(Convert.ToString(reader.GetDateTime(0)));
                    compress.Add((double)reader.GetDecimal(1));
                    ctfs_pech.Add((double)reader.GetDecimal(2));
                    ctfs_perezhim.Add((double)reader.GetDecimal(3));
                    ctfs_reznoe.Add((double)reader.GetDecimal(4));
                    dsc_gal.Add((double)reader.GetDecimal(5));
                    dsc_ramp.Add((double)reader.GetDecimal(6));
                    dsc_drob.Add((double)reader.GetDecimal(7));
                    dsc_shihta.Add((double)reader.GetDecimal(8));
                    h.Add((double)reader.GetDecimal(9));
                    azot.Add((double)reader.GetDecimal(10));

                }
                reader.Close();
            }
            else
            {
                SqlCommand com = new SqlCommand(query2, connection);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dates.Add(Convert.ToString(reader.GetDateTime(0)));
                    compress.Add((double)reader.GetDecimal(1));
                    ctfs_pech.Add((double)reader.GetDecimal(2));
                    ctfs_perezhim.Add((double)reader.GetDecimal(3));
                    ctfs_reznoe.Add((double)reader.GetDecimal(4));
                    dsc_gal.Add((double)reader.GetDecimal(5));
                    dsc_ramp.Add((double)reader.GetDecimal(6));
                    dsc_drob.Add((double)reader.GetDecimal(7));
                    dsc_shihta.Add((double)reader.GetDecimal(8));
                    h.Add((double)reader.GetDecimal(9));
                    azot.Add((double)reader.GetDecimal(10));

                }
                reader.Close();
            }

            SeriesCollection series = new SeriesCollection();

            cartChart.AxisX.Clear();
            cartChart.AxisY.Clear();

            cartChart.AxisX.Add(new Axis
            {
                Title = "Дата и время",
                Labels = dates
            }) ; 

            cartChart.AxisY.Add(new Axis
            {
                Title = "Давление",
            }) ;

            //Линия Компрессорный
            LineSeries line_compress = new LineSeries
            {
                Title = "Компрессорный",
                Values = compress,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };

            //Линия ЦТФС-печь
            LineSeries line_ctfs_pech = new LineSeries
            {
                Title = "ЦТФС-Печь",
                Values = ctfs_pech,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия ЦТФС-Пережим
            LineSeries line_ctfs_perezhim = new LineSeries
            {
                Title = "ЦТФС-Пережим",
                Values = ctfs_perezhim,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия ЦТФС-Резное
            LineSeries line_ctfs_reznoe = new LineSeries
            {
                Title = "ЦТФС-Резное",
                Values = ctfs_reznoe,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия ДСЦ-Галерея
            LineSeries line_dsc_gal = new LineSeries
            {
                Title = "ДСЦ-Галерея",
                Values = dsc_gal,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия ДСЦ-Рампа
            LineSeries line_dsc_ramp = new LineSeries
            {
                Title = "ДСЦ-Рампа",
                Values = dsc_ramp,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия ДСЦ-Дробильный
            LineSeries line_dsc_drob = new LineSeries
            {
                Title = "ДСЦ-Дробильный",
                Values = dsc_drob,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия ДСЦ-Шихта
            LineSeries line_dsc_shihta = new LineSeries
            {
                Title = "ДСЦ-Шихта",
                Values = dsc_shihta,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия Водородный
            LineSeries line_h = new LineSeries
            {
                Title = "Водородный",
                Values = h,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };
            //Линия Азотный
            LineSeries line_azot = new LineSeries
            {
                Title = "Азотный",
                Values = azot,
                Fill = Brushes.Transparent,
                StrokeThickness = .4,
                PointGeometrySize = 0,
                DataLabels = false
            };

            if (compressch.IsChecked.Equals(true))
            {
                series.Add(line_compress);
            }
            if (ctfs_pechch.IsChecked.Equals(true))
            {
                series.Add(line_ctfs_pech);
            }
            if (ctfs_perezhimch.IsChecked.Equals(true))
            {
                series.Add(line_ctfs_perezhim);
            }
            if (ctfs_reznoech.IsChecked.Equals(true))
            {
                series.Add(line_ctfs_reznoe);
            }
            if (dsc_galch.IsChecked.Equals(true))
            {
                series.Add(line_dsc_gal);
            }
            if (dsc_rampch.IsChecked.Equals(true))
            {
                series.Add(line_dsc_ramp);
            }
            if (dsc_drobch.IsChecked.Equals(true))
            {
                series.Add(line_dsc_drob);
            }
            if (dsc_shihtach.IsChecked.Equals(true))
            {
                series.Add(line_dsc_shihta);
            }
            if (hch.IsChecked.Equals(true))
            {
                series.Add(line_h);
            }
            if (azotch.IsChecked.Equals(true))
            {
                series.Add(line_azot);
            }

            cartChart.Series = series;
            
            //Чек бокс для регулеровки вида "Полный график"
            if (fullg.IsChecked.Equals(false)) { 
            cartChart.AxisX[0].MinValue = 0;
            cartChart.AxisX[0].MaxValue = resg.Value;
            cartChart.AxisX[0].MinValue = dates.Count - resg.Value;
            cartChart.AxisX[0].MaxValue = dates.Count + resg.Value;
            }
            

        }

        //Кнопка назад
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            cartChart.AxisX[0].MinValue -= resg.Value;
            cartChart.AxisX[0].MaxValue -= resg.Value;
        }
        //Кнопка вперед
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            cartChart.AxisX[0].MinValue += resg.Value;
            cartChart.AxisX[0].MaxValue += resg.Value;
        }
        //Таймер для постройки графика
        private void timerChart(object sender, EventArgs e) 
        {
            if (run.IsChecked.Equals(true))
            {
                LiveChart();
            }
        }
        
        public void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = calendar.SelectedDate.Value;
            caldate = selectedDate.ToString("yyyy-MM-dd");
        }

        private void a1check_Checked(object sender, RoutedEventArgs e)
        {
                
        }

        private void a2check_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void a3check_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void a4check_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

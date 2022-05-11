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
using LiveCharts.Geared;

namespace Status_Board
{

    public partial class MainWindow : Window

    {
        public static string connectString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\Status_Board\\Status_Board\\Status_Board\\DB.mdf;Integrated Security=True";
        readonly SqlConnection connection = new SqlConnection(connectString);

        static Random rand = new Random();
        static string caldate;


        public MainWindow()
        {
            InitializeComponent();
            
            connection.Open();
            a1slide.IsEnabled = false;
            a2slide.IsEnabled = false;
            a3slide.IsEnabled = false;
            a4slide.IsEnabled = false;

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Tick += new EventHandler(TimerChart);
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




        public void TimerTick(object sender, EventArgs e)
        {
            //Значение компрессоров
            double A1 = Math.Round(a1slide.Value, 3);
            double A2 = Math.Round(a2slide.Value, 3);
            double A3 = Math.Round(a3slide.Value, 3);
            double A4 = Math.Round(a4slide.Value, 3);

            
            Compressor(A1, A2, A3, A4);

            //Значение давления в цехах
            double compress = Math.Round((A1 + A2 + A3 + A4), 3);
            double azot = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double h = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 4));
            double ctfs_pech = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double ctfs_perezhim = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double ctfs_reznoe = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double dsc_gal = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double dsc_ramp = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double dsc_drob = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));
            double dsc_shihta = Math.Abs(Math.Round(compress - (rand.NextDouble() * (5)), 3));

            Departments(azot, h, compress, ctfs_pech, ctfs_perezhim, ctfs_reznoe, dsc_gal, dsc_ramp, dsc_drob, dsc_shihta);

            
        }

        public void Departments(double azot, double h, double compress, double ctfs_pech, double ctfs_perezhim, 
                                double ctfs_reznoe, double dsc_gal, double dsc_ramp, double dsc_drob, double dsc_shihta) 
        {
            if ((azot > 7) || (h >7) || (compress > 7) || (ctfs_pech > 7) || (ctfs_perezhim > 7) || (ctfs_reznoe > 7) || (dsc_gal > 7) || (dsc_ramp > 7) || (dsc_drob > 7) || (dsc_shihta > 7)) 
            {
                Warningi.Visibility = Visibility.Visible;
                Warningl.Visibility = Visibility.Visible;
            }
            else 
            {
                Warningi.Visibility = Visibility.Hidden;
                Warningl.Visibility = Visibility.Hidden;
            }
            
            //Вывод на экран значений ЦЕХОВ

            //----------------------------------------------------------------
            compressc.Content = compress;
            if (compress > 7)
            {
                compressc.Foreground = new SolidColorBrush(Colors.Red);
                compressc.Content += " !";

            }
            else
            {
                compressc.Foreground = new SolidColorBrush(Colors.Blue);
            }
            
            //----------------------------------------------------------------
            ctfs_perezhimc.Content = ctfs_perezhim;
            if (ctfs_perezhim > 7)
            {
                ctfs_perezhimc.Foreground = new SolidColorBrush(Colors.Red);
                ctfs_perezhimc.Content += " !";
            }
            else
            {
                ctfs_perezhimc.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            ctfs_pechc.Content = ctfs_pech;
            if (ctfs_pech > 7)
            {
                ctfs_pechc.Foreground = new SolidColorBrush(Colors.Red);
                ctfs_pechc.Content += " !";

            }
            else
            {
                ctfs_pechc.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            ctfs_reznoec.Content = ctfs_reznoe;
            if (ctfs_reznoe > 7)
            {
                ctfs_reznoec.Foreground = new SolidColorBrush(Colors.Red);
                ctfs_reznoec.Content += " !";

            }
            else
            {
                ctfs_reznoec.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            dsc_galc.Content = dsc_gal;
            if (dsc_gal > 7)
            {
                dsc_galc.Foreground = new SolidColorBrush(Colors.Red);
                dsc_galc.Content += " !";
            }
            else
            {
                dsc_galc.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            dsc_rampc.Content = dsc_ramp;
            if (dsc_ramp > 7)
            {
                dsc_rampc.Foreground = new SolidColorBrush(Colors.Red);
                dsc_rampc.Content += " !";
            }
            else
            {
                dsc_rampc.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            dsc_drobc.Content = dsc_drob;
            if (dsc_drob > 7)
            {
                dsc_drobc.Foreground = new SolidColorBrush(Colors.Red);
                dsc_drobc.Content += " !";
            }
            else
            {
                dsc_drobc.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            dsc_shihtac.Content = dsc_shihta;
            if (dsc_shihta > 7)
            {
                dsc_shihtac.Foreground = new SolidColorBrush(Colors.Red);
                dsc_shihtac.Content += " !";
            }
            else
            {
                dsc_shihtac.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            hc.Content = h;
            if (h > 7)
            {
                hc.Foreground = new SolidColorBrush(Colors.Red);
                hc.Content += " !";

            }
            else
            {
                hc.Foreground = new SolidColorBrush(Colors.Blue);
            }

            //----------------------------------------------------------------
            azotc.Content = azot;
            if (azot > 7)
            {
                azotc.Foreground = new SolidColorBrush(Colors.Red);
                azotc.Content += " !";

            }
            else
            {
                azotc.Foreground = new SolidColorBrush(Colors.Blue);

            }






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
            if(A1 > 7)
            {
                pressA1.Foreground = new SolidColorBrush(Colors.Red);
            }
            else 
            { 
                pressA1.Foreground = new SolidColorBrush(Colors.Blue); 
            }
            pressA2.Content = A2;
            if (A2 > 7)
            {
                pressA2.Foreground = new SolidColorBrush(Colors.Red);
                
            }
            else
            {
                pressA2.Foreground = new SolidColorBrush(Colors.Blue);
            }
            pressA3.Content = A3;
            if (A3 > 7)
            {
                pressA3.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                pressA3.Foreground = new SolidColorBrush(Colors.Blue);
            }
            pressA4.Content = A4;
            if (A4 > 7)
            {
                pressA4.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                pressA4.Foreground = new SolidColorBrush(Colors.Blue);
            }


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
            SqlCommand com;
            //Получаем результаты времени
            if (datecheck.IsChecked.Equals(false))
            {
                SqlCommand com1 = new SqlCommand(query1, connection);
                com = com1;
            }
            else
            {
                SqlCommand com2 = new SqlCommand(query2, connection);
                com = com2;
            }

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
                Title = "Давление в | кгс/см2|",
            }) ;

            //Линия компрессоров
            //Линия компрессоров
            GLineSeries line_compress = new GLineSeries
            {
                Title = "Компрессорный",
                Values = compress.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };

            //Линия ЦТФС-печь
            GLineSeries line_ctfs_pech = new GLineSeries
            {
                Title = "ЦТФС-Печь",
                Values = ctfs_pech.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия ЦТФС-Пережим
            GLineSeries line_ctfs_perezhim = new GLineSeries
            {
                Title = "ЦТФС-Пережим",
                Values = ctfs_perezhim.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия ЦТФС-Резное
            GLineSeries line_ctfs_reznoe = new GLineSeries
            {
                Title = "ЦТФС-Резное",
                Values = ctfs_reznoe.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия ДСЦ-Галерея
            GLineSeries line_dsc_gal = new GLineSeries
            {
                Title = "ДСЦ-Галерея",
                Values = dsc_gal.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия ДСЦ-Рампа
            GLineSeries line_dsc_ramp = new GLineSeries
            {
                Title = "ДСЦ-Рампа",
                Values = dsc_ramp.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия ДСЦ-Дробильня
            GLineSeries line_dsc_drob = new GLineSeries
            {
                Title = "ДСЦ-Дробильный",
                Values = dsc_drob.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия ДСЦ-Шихта
            GLineSeries line_dsc_shihta = new GLineSeries
            {
                Title = "ДСЦ-Шихта",
                Values = dsc_shihta.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия Водород
            GLineSeries line_h = new GLineSeries
            {
                Title = "Водородный",
                Values = h.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

            };
            //Линия Азот
            GLineSeries line_azot = new GLineSeries
            {
                Title = "Азотный",
                Values = azot.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,

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
        private void TimerChart(object sender, EventArgs e) 
        {
            if (run.IsChecked.Equals(true))
            {
                LiveChart();
            }
        }
        
        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = calendar.SelectedDate.Value;
            caldate = selectedDate.ToString("yyyy-MM-dd");
        }

        private void fullg_Checked(object sender, RoutedEventArgs e)
        {
            resg.IsEnabled = false;
            resg.Value = 0;
        }

        private void fullg_Unchhecked(object sender, RoutedEventArgs e)
        {
            resg.IsEnabled = true;
            
        }

        private void A1check_Checked(object sender, RoutedEventArgs e)
        {
            a1slide.IsEnabled = true;
           
        }
        private void A1check_Unchecked(object sender, RoutedEventArgs e)
        {
            a1slide.Value = 0;
            a1slide.IsEnabled = false;
        }

        private void A2check_Checked(object sender, RoutedEventArgs e)
        {
            a2slide.IsEnabled = true;
        }
        private void A2check_Unchecked(object sender, RoutedEventArgs e)
        {
            
            a2slide.Value = 0;
            a2slide.IsEnabled = false;
        }

        private void A3check_Checked(object sender, RoutedEventArgs e)
        {
            a3slide.IsEnabled = true;
        }
        private void A3check_Unchecked(object sender, RoutedEventArgs e)
        {
            
            a3slide.Value = 0;
            a3slide.IsEnabled = false;
        }
        private void A4check_Checked(object sender, RoutedEventArgs e)
        {
            a4slide.IsEnabled = true;
        }

        private void A4check_Unchecked(object sender, RoutedEventArgs e)
        {
            
            a4slide.Value = 0;
            a4slide.IsEnabled = false;
        }
    }
}

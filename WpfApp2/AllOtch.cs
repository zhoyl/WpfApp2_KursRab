using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Drawing;

using Color = System.Drawing.Color;


namespace WpfApp2
{
    internal class AllOtch
    {
        static public void exc(DatePicker dp_dateFrom, DatePicker dp_dateTo,ComboBox cb_Trainer, ComboBox cb_Type, ComboBox cb_View, ComboBox cb_SeasonTicket)
        {

            dp_dateFrom.SelectedDate = DateTime.MinValue;
            dp_dateTo.SelectedDate = DateTime.Now;
            DateTime start = dp_dateFrom.SelectedDate.Value.Date;
            DateTime finish = dp_dateTo.SelectedDate.Value.Date;
            int i = 5;
            var all = FitnesEntities.GetContext().Contracts.ToList().ToList();
            var app = new Excel.Application();
            Excel.Workbook workb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet works = app.Worksheets[1];
            works.Name = "Отчет клиентов";
            works.Range["A2"].Value = "Количество клиентов: ";
            works.Range["A3"].Value = "Доход: ";
            works.Range["A4"].Value = "Дата договора";
            works.Range["B4"].Value = "Имя клиента";
            works.Range["C4"].Value = "Фамилия клиента";
            works.Range["D4"].Value = "Отчество клиента";
            works.Range["E4"].Value = "Тип занятия";
            works.Range["F4"].Value = "Занятие";
            works.Range["G4"].Value = "Количество дней абонемента";
            works.Range["H4"].Value = "Тренер";
            works.Range["I4"].Value = "Стоимость";
            works.Range["J4"].Value = "Оформляющий сотрудник";
            Excel.Range r1 = works.get_Range("A4", "J4");
            r1.Font.Bold = true;
            Excel.Range r = works.get_Range("A1", "S40");
            r.Font.Name = "Arial";
            r.Cells.Font.Size = 10;
            r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            foreach (var p in all)
            {
                if (start <= p.Date_of_conclusion && p.Date_of_conclusion <= finish)
                {
                    works.Range["A1"].Value = "Отчет по клиентам от " + start + " до " + finish;
                    works.Range["A1"].Font.Bold = true;
                    Excel.Range range3 = works.get_Range("A1", "J1");
                    range3.Merge(Type.Missing);
                   
                    if (p.Clients.Status == "Неактивный")
                    {
                        works.Range[$"A{i}:J{i}"].Interior.Color = ColorTranslator.ToOle(Color.IndianRed);
                    }
                    if(p.Date_of_conclusion.AddYears(2)<DateTime.Now)
                    {
                        if (p.Clients.Status == "Активный")
                        {
                            works.Range[$"A{i}:J{i}"].Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                        }
                    }
                    
                   works.Range[$"A{i}:J{i}"].Borders.Color = ColorTranslator.ToOle(Color.Gray);

                    if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                    {
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == "" &&    cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                    {
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name && cb_SeasonTicket.Text == "")
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text +" по типу: " + cb_Type.Text + " и виду занятия: " + cb_View.Text;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == "" && cb_Type.Text == "" && cb_View.Text == "" && cb_SeasonTicket.Text == "")
                    {
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == "" && cb_Type.Text == p.Class.Type && cb_View.Text == "" && cb_SeasonTicket.Text == "")
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + " по типу занятий";
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == "" && cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name &&  cb_SeasonTicket.Text == "")
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + " по типу " + cb_Type.Text + " и виду занятия " + cb_View.Text;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == "" && cb_Type.Text == "" && cb_View.Text == "" && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + " по сроку абонемента: " + cb_SeasonTicket.Text + " дней" ;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }

                    if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == "" && cb_View.Text == "" && cb_SeasonTicket.Text == "")
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + " по тренеру: " + cb_Trainer.Text;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }

                    if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == p.Class.Type && cb_View.Text == "" && cb_SeasonTicket.Text == "")
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text + " по типу занятий: " + cb_Type.Text;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == "" && cb_View.Text == "" && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text + " по количеству дней абонемента: " + cb_SeasonTicket.Text;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }
                    if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == p.Class.Type && cb_View.Text == "" && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                    {
                        works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text +". По типу занятия " + cb_Type.Text +"; По количеству дней абонемента: " + cb_SeasonTicket.Text;
                        works.Range["A" + i].Value = p.Date_of_conclusion;
                        works.Range["B" + i].Value = p.Clients.Name;
                        works.Range["C" + i].Value = p.Clients.Surname;
                        works.Range["D" + i].Value = p.Clients.Patronymic;
                        works.Range["E" + i].Value = p.Class.Type;
                        works.Range["F" + i].Value = p.Class.Name;
                        works.Range["G" + i].Value = p.SeasonTicket.Days;
                        works.Range["H" + i].Value = p.Trainers.Surname;
                        works.Range["I" + i].Value = p.Cost;
                        works.Range["J" + i].Value = p.Workers.Surname;
                        i++;
                    }

                    works.Range["I3:I300"].NumberFormat = "## ###,00";
                    works.Range["B3"].Formula = $"=SUM(I5:I300)";
                    works.Range["B2"].Formula = $"=COUNTA(B5:B300)";
                }
            }
            works.Columns.AutoFit();
            app.Visible = true;
            //ChartObjects xlCharts = (ChartObjects)works.ChartObjects(Type.Missing);
            //ChartObject myChart = (ChartObject)xlCharts.Add(110, 0, 350, 250);
            //Chart chart = myChart.Chart;
            //SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Type.Missing);
            //Series series = seriesCollection.NewSeries();
            //series.XValues = works.get_Range("F5", "F" + i);
            //series.Values = works.get_Range("C5", "C" + i);
            //chart.ChartType = XlChartType.xlXYScatterSmooth;

        }

    }
}

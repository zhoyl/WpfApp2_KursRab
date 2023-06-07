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
            int i = 18;
            var all = FitnesEntities.GetContext().Contracts.ToList().ToList();
            var app = new Excel.Application();
            Excel.Workbook workb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet works = app.Worksheets[1];

            works.Range["A3"].Value = "Виды занятий ";
            works.Range["B3"].Value = "Количество клиентов";
            works.Range["C3"].Value = "Доход";
            Excel.Range range = works.get_Range("A4", "C4");
            range.Merge(Type.Missing);
            range.Font.Name = "Arial";
            range.Cells.Font.Size = 10;
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            works.Range[$"A4"].Interior.Color = ColorTranslator.ToOle(Color.LightSlateGray);
            works.Range["A4"].Value = "Групповые";
            works.Range["A5"].Value = "Спортивный бассейн";
            works.Range["A6"].Value = "Зал";
            works.Range["A7"].Value = "Йога";
            works.Range["A8"].Value = "Тайский бокс";
            works.Range["A9"].Value = "Степ-аэробика";
            works.Range["A10"].Value = "Индвидуальные";
            Excel.Range r = works.get_Range("A10", "C10");
            r.Merge(Type.Missing);
            works.Range[$"A10"].Interior.Color = ColorTranslator.ToOle(Color.LightSlateGray);
            works.Range["A11"].Value = "Спортивный бассейн";
            works.Range["A12"].Value = "Зал";
            works.Range[$"A3:C12"].Borders.Color = ColorTranslator.ToOle(Color.Gray);
            works.Range[$"A4"].Font.Bold = true;
            works.Range[$"A10"].Font.Bold = true;
            works.Range[$"A3:C12"].Borders.Color = ColorTranslator.ToOle(Color.Gray);
            works.Range[$"A14:B15"].Borders.Color = ColorTranslator.ToOle(Color.Gray);

            works.Name = "Отчет клиентов";
            works.Range["A14"].Value = "ВСЕГО клиентов: ";
            works.Range["A15"].Value = "Доход за период: ";
            works.Range["A17"].Value = "Дата договора";
            works.Range["B17"].Value = "Имя клиента";
            works.Range["C17"].Value = "Фамилия клиента";
            works.Range["D17"].Value = "Отчество клиента";
            works.Range["E17"].Value = "Тип занятия";
            works.Range["F17"].Value = "Занятие";
            works.Range["G17"].Value = "Количество дней абонемента";
            works.Range["H17"].Value = "Тренер";
            works.Range["I17"].Value = "Стоимость";
            works.Range["J17"].Value = "Оформляющий сотрудник";
            Excel.Range r1 = works.get_Range("A4", "J4");
            r1.Font.Bold = true;
            Excel.Range r2 = works.get_Range("A1", "S40");
            r.Font.Name = "Arial";
            r.Cells.Font.Size = 10;
            r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            foreach (var p in all)
            {
                var date = DateTime.MaxValue;
                if (date>p.Date_of_conclusion)
                {
                    date = p.Date_of_conclusion;
                }
                try
                {
                    DateTime start = dp_dateFrom.SelectedDate.Value.Date;
                    DateTime finish = dp_dateTo.SelectedDate.Value.Date;
                if (start <= p.Date_of_conclusion && p.Date_of_conclusion <= finish)
                    {
                        works.Range["A1"].Value = "Отчет по клиентам от " + start + " до " + finish;
                        works.Range["A1"].Font.Bold = true;
                        Excel.Range range3 = works.get_Range("A1", "J1");
                        range3.Merge(Type.Missing);
                        range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        if (p.Clients.Status == "Неактивный")
                        {
                            works.Range[$"A{i}:J{i}"].Interior.Color = ColorTranslator.ToOle(Color.IndianRed);
                        }
 
                        if (p.Date_of_conclusion.AddDays(p.SeasonTicket.Days) < DateTime.Now)
                        {
                            works.Range[$"A{i}:J{i}"].Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                        }

                        works.Range[$"A{i}:J{i}"].Borders.Color = ColorTranslator.ToOle(Color.Gray);
                        works.Range[$"A17:J17"].Font.Bold = true;

                        if (cb_Trainer.Text == p.Trainers.Surname && cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                        {
                            works.Range["A1"].Value = works.Range["A1"].Value + " Тренер: " + cb_Trainer.Text + ". Тип занятий: " + cb_Type.Text + ". Вид занятия: " + cb_View.Text + ". Количество дней абонемента: " + cb_SeasonTicket.Text;
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
                        if (cb_Trainer.Text == "" && cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                        {
                            works.Range["A1"].Value = works.Range["A1"].Value +  " Тип занятий: " + cb_Type.Text + ". Вид занятия: " + cb_View.Text + ". Количество дней абонемента: " + cb_SeasonTicket.Text;
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
                            works.Range["A1"].Value = works.Range["A1"].Value + " Тренер: " + cb_Trainer.Text + ". Тип занятий: " + cb_Type.Text + ". Вид занятия: " + cb_View.Text;
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
                            works.Range["A1"].Value = works.Range["A1"].Value + " Тип занятий" + cb_Type.Text ;
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
                        if (cb_Trainer.Text == "" && cb_Type.Text == p.Class.Type && cb_View.Text == p.Class.Name && cb_SeasonTicket.Text == "")
                        {
                            works.Range["A1"].Value = works.Range["A1"].Value + " Тип: " + cb_Type.Text + ". Вид занятия: " + cb_View.Text;
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
                            works.Range["A1"].Value = works.Range["A1"].Value + " Срок абонемента: " + cb_SeasonTicket.Text + " дней";
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
                            works.Range["A1"].Value = works.Range["A1"].Value + " Тренер: " + cb_Trainer.Text;
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
                            works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text + ". Тип занятий: " + cb_Type.Text;
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
                            works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text + ". Количество дней абонемента: " + cb_SeasonTicket.Text;
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
                            works.Range["A1"].Value = works.Range["A1"].Value + "Тренер: " + cb_Trainer.Text + ". По типу занятия " + cb_Type.Text + "; По количеству дней абонемента: " + cb_SeasonTicket.Text;
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
                        if (cb_Trainer.Text == "" && cb_Type.Text == p.Class.Type && cb_View.Text == "" && cb_SeasonTicket.Text == p.SeasonTicket.Days.ToString())
                        {
                            works.Range["A1"].Value = works.Range["A1"].Value + " По типу занятия " + cb_Type.Text + "; По количеству дней абонемента: " + cb_SeasonTicket.Text;
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
                        works.Range["B15"].Formula = $"=SUM(I18:I300)";
                        works.Range["B14"].Formula = $"=COUNTA(B18:B300)";
                        works.Range["B5"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Спортивный бассейн\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["B6"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Зал\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["B7"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Йога\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["B8"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Тайский бокс\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["B9"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Степ-аэробика\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["B11"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Спортивный бассейн\""}, $E$18:$E$300, {"\"Индивидуальный\""})";
                        works.Range["B12"].Formula = $"=COUNTIFS( $F$18:$F$300, {"\"Зал\""}, $E$18:$E$300, {"\"Индивидуальный\""})";
                        works.Range["C5"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Спортивный бассейн\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["C6"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Зал\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["C7"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Йога\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["C8"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Тайский бокс\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["C9"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Степ-аэробика\""}, $E$18:$E$300, {"\"Групповой\""})";
                        works.Range["C11"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Спортивный бассейн\""}, $E$18:$E$300, {"\"Индивидуальный\""})";
                        works.Range["C12"].Formula = $"=SUMIFS( $I$18:$I$300, $F$18:$F$300, {"\"Зал\""}, $E$18:$E$300, {"\"Индивидуальный\""})";
                    }
                }
                catch
                {
                    dp_dateFrom.SelectedDate = date;
                    dp_dateTo.SelectedDate = DateTime.Now;
                }
        }
            works.Columns.AutoFit();
            app.Visible = true;
        }

    }
}

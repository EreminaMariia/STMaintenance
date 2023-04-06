using Entities.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QRCoder;
using System.Diagnostics;
using System.Drawing;

namespace LogicLibrary
{
    public class PrintFormsMaker
    {
        ExcelWorksheet sheet;
        ExcelPackage package;

        public PrintFormsMaker(string name)
        {
            package = new ExcelPackage();
            sheet = package.Workbook.Worksheets.Add(name);
        }

        public void PrintAllFiltredInfoForm(List<int> techIds)
        {
            List<TechPassport> techs = new List<TechPassport>();
            foreach (int techId in techIds)
            {
                var t = Data.Instance.GetPassportById(techId);
                if (t != null)
                {
                    techs.Add(t);
                }
            }
            PrintInfoForm("Список оборудования в эксплуатации", "Список оборудования в эксплуатации в ООО \"ПВ - Транс\"", techs.ToArray());
        }
        public void PrintAllFiltredErrorsForm(List<int> techIds)
        {
            var date = DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");

            string name = "Информация о простое оборудования" + "-" + date;

            MakeErrorGrid(techIds, date);

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }

        public void PrintAllFiltredErrorsEverydayForm(List<int> techIds)
        {
            var date = (DateTime.Now.Month) + "-" + DateTime.Now.Year;
            var timeDate = DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");

            string outerPath = @"C:\Users\User\Downloads\";
            //string outerPath = @"P:\Цех\Общая\Отчет по состоянию оборудования ПВ-транс\";
            //string outerPath = @"\\192.168.1.252\Share\Update\Программа ТО\";
            //string outerPath = "";
            string name = outerPath + "Информация о простое оборудования" + "-" + date;

            MakeFullErrorGrid(techIds, timeDate);
            FinalMaking(sheet, package, name);
        }

        public void PrintErrorOneMachineForm(TechPassport tech, DateTime start, DateTime end)
        {
            var date = (DateTime.Now.Month) + "-" + DateTime.Now.Year;

            string name = "Информация о простое оборудования" + tech.Name + "-" + date;
            PrintOneMachineErrorCard(tech, 1, 1, start, end);
            FinalMaking(sheet, package, name);
            StartProcess(name);
        }

        private void MakeFullErrorGrid(List<int> techIds, string date)
        {
            try
            {
                List<TechPassport> stopedTechs = new List<TechPassport>();
                PrintHorizontalLineItem(1, 2, "Расшифровка:");
                PrintHorizontalLineItem(2, 2, "работает");
                PrintHorizontalLineItem(3, 2, "не работает");

                var green = sheet.Cells[2, 2, 2, 2].Style;
                green.Fill.PatternType = ExcelFillStyle.Solid;
                green.Fill.BackgroundColor.SetColor(Color.LightYellow);
                green.Border.BorderAround(ExcelBorderStyle.Thin);
                var red = sheet.Cells[3, 2, 3, 2].Style;
                red.Fill.PatternType = ExcelFillStyle.Solid;
                red.Fill.BackgroundColor.SetColor(Color.Coral);
                red.Border.BorderAround(ExcelBorderStyle.Thin);

                List<string> infos = new List<string>()
            {
                "Участок", "Наименование", "Марка/модель/характеристики", "Серийный номер", "Инвентарный номер", "Тип оборудования", "Работает (да/нет)"
            };
                for (DateTime i = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); i < new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1); i = i.AddDays(1))
                {
                    infos.Add(i.Day.ToString());
                }
                infos.Add("Итого,час");

                int headerY = 4;
                int headerX = 1;
                headerY = MakeHeader(headerY, headerX + 1, "Информация о простое оборудования на " + date, 4, infos.ToArray());

                var techs = Data.Instance.GetPassportByIds(techIds).OrderBy(d => d.Department.Number);

                foreach (var tech in techs)
                {
                    if (tech != null &&
                        (!string.IsNullOrEmpty(tech.Name) || !string.IsNullOrEmpty(tech.SerialNumber) || !string.IsNullOrEmpty(tech.InventoryNumber)))
                    {
                        var passport = tech;

                        string type = passport.Type != null ? passport.Type.Type : "";
                        string departmentNumber = passport.Department != null && passport.Department.Number != null ? passport.Department.Number : "";
                        string departmentName = passport.Department != null && passport.Department.Name != null ? passport.Department.Name : "";
                        string department = departmentNumber + " - " + departmentName;

                        bool IsNotWorking = true;
                        //string comment = "";
                        List<MaintenanceError> errors = new List<MaintenanceError>();
                        if (passport.Errors != null)
                        {
                            errors = passport.Errors.Where
                            (x => (x.Date != null) &&
                            (x.DateOfSolving == null || x.DateOfSolving.Value.Date == DateTime.MinValue) &&
                            (x.IsActive == null || x.IsActive.Value) &&
                            (x.IsWorking == null || !x.IsWorking.Value)).OrderByDescending(d => d.Date).ToList();

                            IsNotWorking = errors.Count > 0;
                        }

                        if (IsNotWorking)
                            stopedTechs.Add(passport);

                        string working = !IsNotWorking ? "да" : "нет";
                        Color color = !IsNotWorking ? Color.LightYellow : Color.Coral;

                        List<string> passportInfo = new List<string>()
                        {
                        department, passport.Name, passport.Version, passport.SerialNumber, passport.InventoryNumber, type, working
                        };

                        var allDowntimes = passport.Downtimes.Where(x => x.End == null || x.End.Value == DateTime.MinValue || x.End.Value >= new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).ToList();
                        if (allDowntimes != null && allDowntimes.Count>0)
                        {
                            color = Color.Coral; 

                            if (!stopedTechs.Contains(passport))
                            stopedTechs.Add(passport);

                            color = Color.Coral;
                            double hours = 0;
                            for (DateTime d = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); d < new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1); d = d.AddDays(1))
                            {
                                var downtimes = passport.Downtimes.Where(x => x.Start <= d && (x.End == null || x.End.Value == DateTime.MinValue || x.End.Value >= d)).ToList();
                                if (downtimes.Count > 0 && d <= DateTime.Now)
                                {
                                    if (downtimes.Any(x => x.Start.Date < d.Date && (x.End == null || x.End.Value == DateTime.MinValue || x.End.Value.Date > d.Date)))
                                    {
                                        passportInfo.Add("24");
                                        hours += 24;
                                    }
                                    else if (downtimes.Any(x => x.Start.Date == d.Date))
                                    {
                                        var startTime = downtimes.Where(x => x.Start.Date == d.Date).Min(d => d.Start);
                                        passportInfo.Add((24 - startTime.Hour).ToString());
                                        hours += (24 - startTime.Hour);
                                    }
                                    else if (downtimes.Any(x => x.End != null && x.End.Value.Date == d.Date))
                                    {
                                        var endTime = downtimes.Where(x => x.End != null && x.End.Value.Date == d.Date).Max(d => d.End.Value);
                                        passportInfo.Add(endTime.Hour.ToString());
                                        hours += endTime.Hour;
                                    }
                                    else
                                    {
                                        passportInfo.Add("");
                                    }
                                }
                                else
                                {
                                    passportInfo.Add("");
                                }
                            }
                            passportInfo.Add(hours.ToString());
                        }
                        else
                        {
                            for (DateTime d = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); d <= new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1); d = d.AddDays(1))
                            {
                                passportInfo.Add("");
                            }
                        }


                        int endPoint = PrintHorizontalLine(headerY, headerX + 1, passportInfo.ToArray());

                        var style = sheet.Cells[headerY, headerX + 1, headerY, endPoint - 1].Style;
                        style.Fill.PatternType = ExcelFillStyle.Solid;
                        style.Fill.BackgroundColor.SetColor(color);

                        headerY++;
                    }
                }

                headerY++;
                if (stopedTechs.Count > 0)
                {
                    PrintHorizontalLineItem(headerY, 2, "Простой");

                    for (int i = 0; i < stopedTechs.Count; i++)
                    {
                        headerY = PrintOneMachineErrorCard(stopedTechs[i], headerY, headerX, null, null);
                        //headerY = PrintOneMachineErrorCard(stopedTechs[i], headerY, headerX, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month+1, 1));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private int PrintOneMachineErrorCard(TechPassport tech, int headerY, int headerX, DateTime? startDate, DateTime? endDate)
        {
            if (tech != null &&
                            (!string.IsNullOrEmpty(tech.Name) || !string.IsNullOrEmpty(tech.SerialNumber) || !string.IsNullOrEmpty(tech.InventoryNumber)))
            {
                headerY = MakeHeader(headerY, headerX + 1, "", 1,
                "Наименование оборудования", "Дата", "Проблема", "Дата", "Ответ/ результат");

                var passport = tech;

                var errors = passport.Errors.Where
                (x => x.Date != null &&                
                (x.IsActive == null || x.IsActive.Value)
                && (x.IsWorking == null || !x.IsWorking.Value)
                && (startDate == null || x.DateOfSolving == null || x.DateOfSolving == DateTime.MinValue || x.DateOfSolving >= startDate) && (endDate == null || startDate <= endDate)).OrderBy(d => d.Date).ToList();

                if (errors.Count > 0)
                {
                    int start = headerY;
                    for (int e = 0; e < errors.Count; e++)
                    {                        
                        string name = e > 0 ? "" : (passport.Name != null ? passport.Name : "") + " " + (passport.Version != null ? passport.Version : "");
                        var repairings = Data.Instance.GetRepairings().Where(x => x.Error.Id == errors[e].Id).OrderBy(d => d.Date).ToList();
                        if (repairings != null && repairings.Count > 0)
                        {
                            int eStart = headerY;
                            for (int r = 0; r < repairings.Count; r++)
                            {
                                var eName = r > 0 ? "" : (errors[e].Name != null ? errors[e].Name : "");
                                var eDate = r > 0 ? "" : (errors[e].Date != null ? errors[e].Date.ToString() : "");
                                name = r > 0 ? "" : (passport.Name != null ? passport.Name : "") + " " + (passport.Version != null ? passport.Version : "");
                                int endPoint = PrintHorizontalLine(headerY, headerX + 1,
                                name, eDate, eName, repairings[r].Date.ToString(), repairings[r].Comment);
                                headerY++;
                            }
                            int eEnd = headerY;
                            sheet.Cells[eStart, 3, eEnd - 1, 3].Merge = true;
                            sheet.Cells[eStart, 4, eEnd - 1, 4].Merge = true;
                        }
                        else
                        {
                            int endPoint = PrintHorizontalLine(headerY, headerX + 1,
                            name, errors[e].Date.ToString(), errors[e].Name, "", "");
                            headerY++;
                        }
                    }
                    int end = headerY;
                    sheet.Cells[start, 2, end - 1, 2].Merge = true;

                    var downtimes = passport.Downtimes.Where(x => x.End == null || x.End.Value >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToList();
                    if (downtimes != null && downtimes.Count > 0)
                    {
                        var lastStartDate = downtimes.Max(x => x.Start);
                        var lastDownTime = downtimes.Where(x => x.Start == lastStartDate).FirstOrDefault();
                        double downtimeHours = 0;

                        var st = startDate != null? startDate.Value: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        var en = endDate != null ? endDate.Value : DateTime.Now;
                        foreach (var d in downtimes)
                        {
                            if (d.Start >= st)
                            {
                                if (d.End != null && d.End.Value != DateTime.MinValue && (endDate == null || d.End.Value <= endDate.Value))
                                {
                                    downtimeHours += (d.End - d.Start).Value.TotalHours;
                                }
                                else
                                {
                                    downtimeHours += (en - d.Start).TotalHours;
                                }
                            }
                            else
                            {
                                var firstDay = st;
                                if (d.End != null && d.End.Value != DateTime.MinValue && (endDate == null || d.End.Value <= endDate.Value))
                                {
                                    downtimeHours += (d.End - firstDay).Value.TotalHours;
                                }
                                else
                                {
                                    downtimeHours += (en - firstDay).TotalHours;
                                }
                            }
                        }

                        DateTime s = (startDate != null && lastDownTime.Start < startDate.Value) ? startDate.Value : lastDownTime.Start;
                        DateTime? e = (endDate != null && (lastDownTime.End == null || lastDownTime.End.Value > endDate.Value)) ? endDate.Value : lastDownTime.End;

                        PrintHorizontalLineItem(headerY, 2, "Дата начала простоя");
                        PrintHorizontalLineItem(headerY++, 3, s.ToLongDateString() + " " + s.ToLongTimeString());
                        PrintHorizontalLineItem(headerY, 2, "Дата окончания простоя");
                        PrintHorizontalLineItem(headerY++, 3, (e != null && e.Value != DateTime.MinValue) ? (e.Value.ToLongDateString() + " " + e.Value.ToLongTimeString()) : "");
                        PrintHorizontalLineItem(headerY, 2, "Общее количество часов простоя");
                        PrintHorizontalLineItem(headerY++, 3, downtimeHours.ToString("N2"));
                    }
                    else
                    {
                        PrintHorizontalLineItem(headerY++, 2, "Дата начала простоя");
                        PrintHorizontalLineItem(headerY++, 2, "Дата окончания простоя");
                        PrintHorizontalLineItem(headerY++, 2, "Общее количество часов простоя");
                    }
                }
            }
            return headerY;
        }

        private void MakeErrorGrid(List<int> techIds, string date)
        {
            List<TechPassport> techs = new List<TechPassport>();
            foreach (int techId in techIds)
            {
                var t = Data.Instance.GetPassportById(techId);
                if (t != null)
                {
                    techs.Add(t);
                }
            }

            PrintHorizontalLineItem(1, 3, "Расшифровка:");
            PrintHorizontalLineItem(2, 3, "работает");
            PrintHorizontalLineItem(3, 3, "не работает");

            var green = sheet.Cells[2, 2, 2, 2].Style;
            green.Fill.PatternType = ExcelFillStyle.Solid;
            green.Fill.BackgroundColor.SetColor(Color.LightYellow);
            green.Border.BorderAround(ExcelBorderStyle.Thin);
            var red = sheet.Cells[3, 2, 3, 2].Style;
            red.Fill.PatternType = ExcelFillStyle.Solid;
            red.Fill.BackgroundColor.SetColor(Color.Coral);
            red.Border.BorderAround(ExcelBorderStyle.Thin);

            int headerY = 4;
            int headerX = 1;
            headerY = MakeHeader(headerY, headerX + 1, "Информация о простое оборудования на " + date, 4,
                "№ п/п", "Наименование", "Серийный номер", "Инвентарный номер", "Тип оборудования", "Участок", "Работает (да/нет)", "Комментарий");

            for (int i = 0; i < techs.Count; i++)
            {
                if (techs[i] != null &&
                    (!string.IsNullOrEmpty(techs[i].Name) || !string.IsNullOrEmpty(techs[i].SerialNumber) || !string.IsNullOrEmpty(techs[i].InventoryNumber)))
                {
                    var passport = techs[i];

                    string type = passport.Type != null ? passport.Type.Type : "";
                    string departmentNumber = passport.Department != null && passport.Department.Number != null ? passport.Department.Number : "";
                    string departmentName = passport.Department != null && passport.Department.Name != null ? passport.Department.Name : "";
                    string department = departmentNumber + " - " + departmentName;

                    bool IsNotWorking = true;
                    string comment = "";

                    if (passport.Errors != null)
                    {
                        var errors = passport.Errors.Where
                        (x => (x.Date != null) &&
                        (x.DateOfSolving == null || x.DateOfSolving.Value.Date == DateTime.MinValue) &&
                        (x.IsActive == null || x.IsActive.Value) &&
                        (x.IsWorking == null || !x.IsWorking.Value)).OrderByDescending(d => d.Date).ToList();

                        IsNotWorking = errors.Count > 0;

                        if (IsNotWorking)
                        {
                            comment = errors.FirstOrDefault().Comment != null ? errors.FirstOrDefault().Comment : "";
                        }
                    }
                    string working = !IsNotWorking ? "да" : "нет";
                    Color color = !IsNotWorking ? Color.LightYellow : Color.Coral;


                    int endPoint = PrintHorizontalLine(headerY, headerX + 1,
                    (i + 1).ToString(), passport.Name + " " + passport.Version, passport.SerialNumber, passport.InventoryNumber, type,
                    department, working, comment);

                    var style = sheet.Cells[headerY, headerX + 1, headerY, endPoint - 1].Style;
                    style.Fill.PatternType = ExcelFillStyle.Solid;
                    style.Fill.BackgroundColor.SetColor(color);

                    headerY++;
                }
            }
        }
        public void PrintInfoForm(TechPassport passport)
        {
            PrintInfoForm("Карточка оборудования", "Карточка оборудования: " + passport.Name, passport);
        }
        public void PrintInfoForm(string fileName, string header, params TechPassport[] techs)
        {
            string name = fileName + "-" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;

            int headerY = 4;
            int headerX = 1;
            //"Информация по оборудованию в эксплуатации в ООО \"ПВ - Транс\""
            headerY = MakeHeader(headerY, headerX + 1, header, 4,
                "№ п/п", "Наименование", "Серийный номер", "Инвентарный номер", "Тип оборудования",
                "Дата изготовления", "Дата начала эксплуатации", "Гарантия заканчивается",
                "Участок эксплуатации", "Ответственный за эксплуатацию", "Точка подключения",
                "Потребляемая мощность, кВт", "Поставщик");

            //var techs = Data.Instance.GetFullTechPassports().Where(t => ids.Contains(t.Id)).ToList();
            for (int i = 0; i < techs.Length; i++)
            {
                if (techs[i] != null &&
                    (!string.IsNullOrEmpty(techs[i].Name) || !string.IsNullOrEmpty(techs[i].SerialNumber) || !string.IsNullOrEmpty(techs[i].InventoryNumber)))
                {
                    headerY = PrintPassportLine(techs[i], headerY, headerX + 1, i + 1);
                }
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }
        private int MakeHeader(int headerY, int headerX, string title, int koef, params string[] values)
        {
            sheet.Cells[headerY, 1].Value = title;
            sheet.Column(1).Width = 1;
            headerY += koef;

            int endPoint = PrintHorizontalLine(headerY, headerX, values);

            sheet.Cells[headerY, headerX, headerY, endPoint - 1].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[headerY, headerX, headerY, endPoint - 1].Style.Border.Right.Style = ExcelBorderStyle.Double;

            headerY++;
            return headerY;
        }
        private int PrintPassportLine(TechPassport passport, int headerY, int headerX, int number)
        {
            string type = passport.Type != null ? passport.Type.Type : "";
            string release = passport.ReleaseYear != null ? ((DateTime)passport.ReleaseYear).ToShortDateString() : "";
            string commisioning = passport.CommissioningDate != null ? ((DateTime)passport.CommissioningDate).ToShortDateString() : "";
            string decommisioning = passport.DecommissioningDate != null ? ((DateTime)passport.DecommissioningDate).ToShortDateString() : "";
            string op = passport.Operator != null && passport.Operator.Name != null ? passport.Operator.Name : "";
            string point = passport.ElectroPoint != null && passport.ElectroPoint.Name != null ? passport.ElectroPoint.Name : "";
            string power = passport.Power != null ? ((double)passport.Power).ToString() : "";
            string supplier = passport.Supplier != null && passport.Supplier.Name != null ? passport.Supplier.Name : "";


            string departmentNumber = passport.Department != null && passport.Department.Number != null ? passport.Department.Number : "";
            string departmentName = passport.Department != null && passport.Department.Name != null ? passport.Department.Name : "";
            string department = departmentNumber + " - " + departmentName;

            int endPoint = PrintHorizontalLine(headerY, headerX,
            number.ToString(), passport.Name + " " + passport.Version, passport.SerialNumber, passport.InventoryNumber, type,
            release, commisioning, decommisioning, department, op, point, power, supplier);

            //sheet.Cells[headerY, headerX, headerY, endPoint - 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            //sheet.Cells[headerY, headerX, headerY, endPoint - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            headerY++;
            return headerY;
        }
        private List<IPlanedView> MakePlannedList(DateTime start, DateTime end, List<IPlanedView> planned, out List<MaintenanceNewView> maintenances)
        {
            maintenances = new List<MaintenanceNewView>();

            List<IPlanedView> temp = new List<IPlanedView>();
            foreach (IPlanedView plan in planned)
            {
                if (plan is AdditionalWorkView || plan is MaintenanceEpisodeView)
                {
                    if (plan.FutureDate.Date <= end.Date && plan.FutureDate.Date >= start.Date)
                    {
                        temp.Add(plan);
                    }
                }
                else if (plan is MaintenanceNewView)
                {
                    var maints = (MaintenanceNewView)plan;
                    maintenances.Add(maints);
                    List<DateTime> dates = maints.GetPlannedDates(start, end);
                    foreach (DateTime date in dates)
                    {
                        temp.Add(new MaintenanceEpisodeView
                        {
                            FutureDate = date,
                            Machine = maints.Machine,
                            MachineId = maints.MachineId,
                            Name = maints.Name,
                            Type = maints.Type,
                            WorkingHours = maints.GetWorkingHours()
                        });
                    }

                }
            }

            return temp;
        }
        public void PrintPlanForm(DateTime start, DateTime end, List<IPlanedView> planned)
        {
            string name = "План работ_" + start.ToShortDateString() + "-" + end.ToShortDateString() + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;

            int headerY = 4;
            int headerX = 1;
            headerY = MakeHeader(headerY, headerX + 1,
                "План работ по ТО и Р оборудования на период с " + start.ToShortDateString() + " по " + end.ToShortDateString(), 4,
                "Дата", "Наименование оборудования", "Наименование работ", "Вид работ", "Требуемые ТМЦ", "Трудоемкость, ч", "Планируемый сотрудник ФИО");

            List<IPlanedView> temp = MakePlannedList(start, end, planned, out List<MaintenanceNewView> maintenances);

            IEnumerable<IGrouping<DateTime, IPlanedView>>? filtred;
            filtred = temp.GroupBy(t => t.FutureDate).OrderBy(a => a.Key);

            foreach (var view in filtred)
            {
                foreach (var item in view)
                {
                    if (item is AdditionalWorkView)
                    {
                        var work = (AdditionalWorkView)item;
                        MakeQrForXls(headerY - 1, headerX - 1,
                            work.FutureDate.ToShortDateString() + "\n" + work.Machine + "\n" + work.Name + "\n" + work.Id);
                        PrintHorizontalLine(headerY, headerX + 1,
                        work.FutureDate.ToShortDateString(), work.Machine, work.Name, work.Type, work.Materials, work.WorkingHours, work.Operators);
                        headerY++;

                    }
                    else if (item is MaintenanceEpisodeView)
                    {
                        var episode = (MaintenanceEpisodeView)item;
                        MakeQrForXls(headerY - 1, headerX - 1,
                            episode.FutureDate.ToShortDateString() + "\n" + episode.Machine + "\n" + episode.Name + "\n" + episode.Id);
                        var m = maintenances.FirstOrDefault(n => n.Id == episode.MaintenanceId);
                        string materials = m != null ? m.Materials : "";
                        PrintHorizontalLine(headerY, headerX + 1,
                        episode.FutureDate.ToShortDateString(), episode.Machine, episode.Name, episode.Type, materials, episode.WorkingHours.ToString(), episode.Operator);
                        headerY++;
                    }
                }
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }

        public void ExportPlanForm(DateTime start, DateTime end, List<List<KeyValuePair<System.Drawing.Color, string>>> planLocal)
        {
            string name = "План работ_" + start.ToShortDateString() + "_" + end.ToShortDateString() + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;
            int headerY = 1;
            int headerX = 1;

            PrintHorizontalLineItem(1, 1, "Расшифровка:");
            PrintHorizontalLineItem(2, 1, "Есть запланированные");
            PrintHorizontalLineItem(3, 1, "Есть просроченные");
            PrintHorizontalLineItem(4, 1, "Есть архивные");

            var green = sheet.Cells[2, 1, 2, 1].Style;
            green.Fill.PatternType = ExcelFillStyle.Solid;
            green.Fill.BackgroundColor.SetColor(Color.Aquamarine);
            green.Border.BorderAround(ExcelBorderStyle.Thin);
            var red = sheet.Cells[3, 1, 3, 1].Style;
            red.Fill.PatternType = ExcelFillStyle.Solid;
            red.Fill.BackgroundColor.SetColor(Color.Coral);
            red.Border.BorderAround(ExcelBorderStyle.Thin);
            var beige = sheet.Cells[4, 1, 4, 1].Style;
            beige.Fill.PatternType = ExcelFillStyle.Solid;
            beige.Fill.BackgroundColor.SetColor(Color.Beige);
            beige.Border.BorderAround(ExcelBorderStyle.Thin);

            var types = Data.Instance.GetMaintenanceTypes();
            foreach (var type in types)
            {
                PrintHorizontalLineItem(headerY, 4, type.Type, true);
                PrintHorizontalLineItem(headerY, 5, type.Description, true);
                sheet.Cells[headerY, 5].Style.WrapText = false;
                headerY++;
            }
            headerY++;

            foreach (var line in planLocal)
            {
                foreach (var cell in line)
                {
                    if (cell.Value == line.FirstOrDefault().Value)
                        PrintHorizontalLineItem(headerY, headerX, cell.Value);
                    else
                        PrintHorizontalLineItem(headerY, headerX, cell.Value, true);
                    var style = sheet.Cells[headerY, headerX, headerY, headerX].Style;
                    style.Fill.PatternType = ExcelFillStyle.Solid;
                    style.Fill.BackgroundColor.SetColor(cell.Key);
                    style.Border.BorderAround(ExcelBorderStyle.Thin);
                    headerX++;
                }
                headerY++;
                headerX = 1;
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }
        public void PrintCommonPlanForm(DateTime start, DateTime end, List<IPlanedView> planned)
        {
            string name = "План работ(общий)_" + start.ToShortDateString() + "_" + end.ToShortDateString() + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;
            int headerY = 4;
            int headerX = 1;

            List<IPlanedView> temp = MakePlannedList(start, end, planned, out List<MaintenanceNewView> maintenances);

            IEnumerable<IGrouping<DateTime, IPlanedView>>? orderedByDate = temp.GroupBy(t => t.FutureDate).OrderBy(a => a.Key);
            IEnumerable<IGrouping<int, IPlanedView>>? orderedByMachine = temp.GroupBy(t => t.MachineId);

            List<string> sList = new List<string>();
            sList.Add("№ п/п");
            sList.Add("Наименование оборудования");
            sList.Add("Серийный номер оборудования");

            int count = sList.Count;

            List<DateTime> dList = new List<DateTime>();

            foreach (var f in orderedByDate)
            {
                if (f.Key.Date <= end.Date && f.Key.Date >= start.Date)
                {
                    dList.Add(f.Key);
                    sList.Add(f.Key.ToShortDateString());
                }
            }

            headerY = MakeHeader(headerY, headerX + 1,
                "План работ по ТО и Р оборудования на период с " + start.ToShortDateString() + " по " + end.ToShortDateString(), 4,
                sList.ToArray());

            int number = 1;
            foreach (var view in orderedByMachine)
            {
                string[] line = new string[dList.Count + 3];
                line[0] = number.ToString();
                var passport = Data.Instance.GetTechPassports().FirstOrDefault(x => x.Id == view.Key);
                if (passport != null)
                {
                    line[1] = passport.Name;
                    line[2] = passport.SerialNumber;

                    foreach (var item in view)
                    {
                        for (int i = 0; i < dList.Count; i++)
                        {
                            if (item.FutureDate.Date == dList[i].Date)
                            {
                                line[i + 3] = item.Type + "/" + item.GetWorkingHours().ToString();
                            }
                        }

                    }
                    MakeQrForXls(headerY - 1, headerX - 1,
                        passport.Name + "\n" + start.ToShortDateString() + "\n" + end.ToShortDateString());
                    PrintHorizontalLine(headerY, headerX + 1, line);
                    headerY++;
                    number++;
                }
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }
        public void PrintWorkOrderForm(DateTime day, List<IPlanedView> planned)
        {
            string name = "Наряд на работы_" + day.ToShortDateString() + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;
            int headerY = 4;
            int headerX = 1;

            headerY = MakeHeader(headerY, headerX + 1,
                "Наряд на работы на " + day.ToShortDateString(), 4,
                "№ п/п", "Наименование оборудования", "Наименование выполняемых работ", "Вид работ", "Требуемые ТМЦ", "Трудоемкость плановая, ч", "Трудоемкость фактическая, ч", "Наработка оборудования, ч", "Работы выполнил ФИО");

            List<IPlanedView> temp = MakePlannedList(day, day, planned, out List<MaintenanceNewView> maintenances);

            int number = 1;
            foreach (var item in temp)
            {
                if (item is AdditionalWorkView)
                {
                    var work = (AdditionalWorkView)item;
                    MakeQrForXls(headerY - 1, headerX - 1,
                        DateTime.Today.ToShortDateString() + "\n" + work.Machine + "\n" + work.Name + "\n" + work.Id);
                    PrintHorizontalLine(headerY, headerX + 1,
                    number.ToString(), work.Machine, work.Name, work.Type, work.Materials, work.WorkingHours);
                    headerY++;

                }
                else if (item is MaintenanceEpisodeView)
                {
                    var episode = (MaintenanceEpisodeView)item;
                    MakeQrForXls(headerY - 1, headerX - 1,
                        DateTime.Today.ToShortDateString() + "\n" + episode.Machine + "\n" + episode.Name + "\n" + episode.Id);
                    var m = maintenances.FirstOrDefault(n => n.Id == episode.MaintenanceId);
                    string materials = m != null ? m.Materials : "";
                    PrintHorizontalLine(headerY, headerX + 1,
                    number.ToString(), episode.Machine, episode.Name, episode.Type, materials, episode.WorkingHours.ToString());
                    headerY++;
                }
                number++;
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }

        public void PrintArchiveForm(string passportName, List<InnerArchiveView> instruments)
        {
            string name = "Архив" + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;

            int headerY = 3;
            int headerX = 1;

            sheet.Cells[headerY, 1].Value = "Отчет по выполненным работам по оборудованию: " + passportName;
            sheet.Cells[headerY + 1, 1].Value = "Отчёт сформирован на " + DateTime.Now.ToString("dd-MM-yy");

            headerY = 4;
            headerY = MakeHeader(headerY, headerX + 1, "Отчёт сформирован на " + DateTime.Now.ToString("dd-MM-yy"), 4,
                "№", "Дата", "Вид работ", "Наименование работ",
                "Плановая трудоемкость ч/час", "Фактическая трудоемкость ч/час", "ФИО лица проводившего работы");

            for (int i = 0; i < instruments.Count; i++)
            {
                var date = instruments[i].Date != null ? instruments[i].Date.Value.ToString("dd-MM-yy") : "";
                PrintHorizontalLine(headerY + i, headerX + 1, (i + 1).ToString(),
                        date, instruments[i].Type, instruments[i].Name,
                        instruments[i].WorkingHours, instruments[i].FactWorkingHours, instruments[i].Operators);
            }
            FinalMaking(sheet, package, name);
            StartProcess(name);
        }

        public void PrintArchiveForm(DateTime start, DateTime end, List<OuterArchiveView> instruments)
        {
            var filtredInstruments = instruments.Where(x => x.Date != null && x.Date >= start && x.Date <= end).ToList();
            PrintArchiveForm(filtredInstruments, "Отчёт с " + start.ToString("dd-MM-yy") + " по " + end.ToString("dd-MM-yy"));
        }

        public void PrintArchiveForm(List<OuterArchiveView> instruments)
        {
            PrintArchiveForm(instruments, "Отчёт сформирован на " + DateTime.Now.ToString("dd-MM-yy"));
        }


        public void PrintArchiveForm(List<OuterArchiveView> instruments, string header)
        {
            string name = "Архив" + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;

            int headerY = 3;
            int headerX = 1;

            sheet.Cells[headerY, 1].Value = "Отчет по выполненным работам";
            sheet.Cells[headerY + 1, 1].Value = header;

            headerY = 4;
            headerY = MakeHeader(headerY, headerX + 1, "Отчёт сформирован на " + DateTime.Now.ToString("dd-MM-yy"), 4,
                "№", "Дата выполнения", "Наименование оборудования", "Вид работ", "Наименование работ",
                "Плановая трудоемкость ч/час", "Фактическая трудоемкость ч/час", "ФИО лица проводившего работы");

            for (int i = 0; i < instruments.Count; i++)
            {
                var date = instruments[i].Date != null ? ((DateTime)instruments[i].Date).ToString("dd-MM-yy") : "";
                PrintHorizontalLine(headerY + i, headerX + 1, (i + 1).ToString(),
                        date, instruments[i].MachineName, instruments[i].Type, instruments[i].Name,
                        instruments[i].WorkingHours, instruments[i].FactWorkingHours, instruments[i].Operators);
            }
            FinalMaking(sheet, package, name);
            StartProcess(name);
        }


        public void PrintInstrumentsForm(string passportName, List<InstrumentView> instruments)
        {
            string name = "Инструменты" + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;

            int headerY = 3;
            int headerX = 1;

            sheet.Cells[headerY, 1].Value = "Отчёт сформирован на " + DateTime.Now.ToString("dd-MM-yy");

            headerY = 4;
            headerY = MakeHeader(headerY, headerX + 1, "Инструменты/оснастка оборудования в эксплуатации: " + passportName, 4,
                "№", "Наименование", "Артикул", "Количество", "Ед. измерения",
                "Дата ввода в эксплуатацию", "Комментарий");

            var inWork = instruments.Where(n => n.RemoveDate == null || n.RemoveDate == DateTime.MinValue).ToList();
            for (int i = 0; i < inWork.Count; i++)
            {
                if (inWork[i] != null &&
                    (!string.IsNullOrEmpty(inWork[i].Name) || !string.IsNullOrEmpty(inWork[i].Art)))
                {
                    string date = inWork[i].CreateDate != null ? inWork[i].CreateDate.Value.ToShortDateString() : "";
                    headerY = PrintHorizontalLine(headerY, headerX + 1, (i + 1).ToString(),
                        inWork[i].Name, inWork[i].Art, inWork[i].Count, inWork[i].Unit, date, inWork[i].Commentary);
                }
            }

            headerY += 4;

            var notInWork = instruments.Where(n => n.RemoveDate != null && n.RemoveDate != DateTime.MinValue).ToList();

            if (notInWork.Count > 0)
            {
                headerY = MakeHeader(headerY, headerX + 1, "Инструменты/оснастка оборудования списанные: " + passportName, 4,
                    "№", "Наименование", "Артикул", "Количество", "Ед. измерения",
                    "Дата ввода в эксплуатацию", "Комментарий", "Дата списания", "Причина удаления");

                for (int i = 0; i < notInWork.Count; i++)
                {
                    if (notInWork[i] != null &&
                        (!string.IsNullOrEmpty(notInWork[i].Name) || !string.IsNullOrEmpty(notInWork[i].Art)))
                    {
                        string date = notInWork[i].CreateDate != null ? notInWork[i].CreateDate.Value.ToShortDateString() : "";
                        string removeDate = notInWork[i].RemoveDate != null ? notInWork[i].RemoveDate.Value.ToShortDateString() : "";

                        headerY = PrintHorizontalLine(headerY, headerX + 1, (i + 1).ToString(),
                            notInWork[i].Name, notInWork[i].Art, notInWork[i].Count, notInWork[i].Unit,
                            date, notInWork[i].Commentary, removeDate, notInWork[i].RemoveReason);
                    }
                }
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }
        public void PrintControlParamsForm(string passportName, List<ControledParametrView> parametrs, List<ControledParametrEpisodeView> episodes)
        {
            string name = "Контролируемые параметры" + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;
            int headerY = 4;
            int headerX = 1;

            List<DateTime> dList = new List<DateTime>();
            List<string> sList = new List<string>();
            sList.Add("Наименование параметра");
            sList.Add("Номинальное значение");
            sList.Add("Единица измерения");

            IEnumerable<IGrouping<DateTime, ControledParametrEpisodeView>>? orderedByDate = episodes.GroupBy(t => t.Date).OrderBy(a => a.Key);

            foreach (var f in orderedByDate)
            {
                dList.Add(f.Key);
                sList.Add(f.Key.ToString());
            }

            headerY = MakeHeader(headerY, headerX,
                "Наименование оборудования: " + passportName, 4,
                sList.ToArray());

            int number = 1;
            foreach (var p in parametrs)
            {
                string[] line = new string[dList.Count + 3];
                line[0] = p.Name;
                line[1] = Math.Round(p.Nominal, 4, MidpointRounding.AwayFromZero).ToString();
                line[2] = p.Unit;
                var episodesByParametr = episodes.Where(e => e.GetParamId() == p.Id);
                foreach (var item in episodesByParametr)
                {
                    for (int i = 0; i < dList.Count; i++)
                    {
                        if (item.Date.Date == dList[i].Date)
                        {
                            line[i + 3] = Math.Round(item.Count, 4, MidpointRounding.AwayFromZero).ToString();
                        }
                    }
                }
                PrintHorizontalLine(headerY, headerX, line.ToArray());
                headerY++;
                number++;
            }
            FinalMaking(sheet, package, name);
            StartProcess(name);
        }
        private int PrintHorizontalLine(int Y, int X, params string[] values)
        {
            int result = X;
            foreach (string value in values)
            {
                result = PrintHorizontalLineItem(Y, result, value);
            }
            //sheet.Cells[Y, X, Y, result].Style.Border.Diagonal.Style = ExcelBorderStyle.Thin;
            sheet.Cells[Y, X, Y, result - 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Cells[Y, X, Y, result - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            return result;
        }
        private int PrintHorizontalLineItem(int Y, int X, string value, bool isCalendar = false)
        {
            double width = sheet.Column(X).Width;
            sheet.Cells[Y, X].Value = value;
            sheet.Cells[Y, X].Style.WrapText = true;
            int letters = 0;
            if (value != null)
            {
                letters = value.Length;
            }
            if (isCalendar)
            {
                sheet.Column(X).Width = 12;
            }
            else
            {
                sheet.Column(X).Width = letters > width ? letters : width;
                sheet.Column(X).Width = sheet.Column(X).Width > 60 ? 60 : sheet.Column(X).Width;
            }

            X++;
            return X;
        }
        private void FinalMaking(ExcelWorksheet sheet, ExcelPackage package, string name)
        {
            //sheet.Protection.IsProtected = true;
            sheet.Protection.AllowFormatColumns = true;
            sheet.Protection.AllowFormatRows = true;
            sheet.PrinterSettings.PaperSize = ePaperSize.A4;
            sheet.PrinterSettings.FitToWidth = 1;
            sheet.PrinterSettings.Orientation = eOrientation.Landscape;
            sheet.PrinterSettings.LeftMargin = 0.16M;
            sheet.PrinterSettings.RightMargin = 0.16M;
            sheet.Names.AddFormula("_xlnm.Print_Titles", $"'{sheet.Name}'!$A:$C,'{sheet.Name}'!$1:$5");
            var report = package.GetAsByteArray();
            File.WriteAllBytes(name + ".xlsx", report);
        }

        private void StartProcess(string name)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(name + ".xlsx")
            {
                UseShellExecute = true
            };
            process.Start();
        }

        private void MakeQrForXls(int Y, int X, string info)
        {
            BitmapByteQRCode qrCode = MakeQRInfo(info);

            byte[] arr = qrCode.GetGraphic(2);
            using (var ms = new MemoryStream(arr))
            {
                Bitmap tempqrBitmap = new Bitmap(ms);
                //qrBitmap.SetResolution(qrBitmap.HorizontalResolution / 1.5f, qrBitmap.VerticalResolution / 1.5f);
                //qrBitmap.SetResolution(80, 80);
                Bitmap qrBitmap = new Bitmap(tempqrBitmap, new Size(80, 80));
                sheet.Row(Y + 1).Height = qrBitmap.Height * 0.8;
                sheet.Column(X + 1).Width = qrBitmap.Width * 0.15;
                var picture = sheet.Drawings.AddPicture("qr" + Y, qrBitmap);
                picture.SetPosition(Y, 0, X, 0);
                qrBitmap.Dispose();
            }
        }

        private BitmapByteQRCode MakeQRInfo(string str)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
            return new BitmapByteQRCode(qrCodeData);
        }

    }
}

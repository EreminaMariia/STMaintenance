using Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;

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

        //public void PrintAllInfoForm()
        //{
        //    PrintInfoForm("Список оборудования в эксплуатации", "Список оборудования в эксплуатации в ООО \"ПВ - Транс\"", Data.Instance.GetFullTechPassports().ToArray());
        //}
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
            var date = DateTime.Now.ToString("dd-MM-yy");
            var timeDate = DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");

            string outerPath = @"C:\Users\User\Downloads\";
            //string outerPath = @"P:\Цех\Общая\Отчет по состоянию оборудования ПВ-транс\";
            string name = outerPath + "Информация о простое оборудования" + "-" + date;

            if (!File.Exists(name+ ".xlsx"))
            {
                MakeErrorGrid(techIds, timeDate);
                FinalMaking(sheet, package, name);
            }
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
            headerY = MakeHeader(headerY, headerX + 1, "Информация о простое оборудования на " + date,
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
                    (i + 1).ToString(), passport.Name, passport.SerialNumber, passport.InventoryNumber, type,
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
            headerY = MakeHeader(headerY, headerX+1, header,
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
                    headerY = PrintPassportLine(techs[i], headerY, headerX+1, i + 1);
                }
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }
        private int MakeHeader(int headerY, int headerX, string title, params string[] values)
        {
            sheet.Cells[headerY, 1].Value = title;
            sheet.Column(1).Width = 1;
            headerY += 4;

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
            number.ToString(), passport.Name, passport.SerialNumber, passport.InventoryNumber, type,
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
            headerY = MakeHeader(headerY, headerX+1,
                "План работ по ТО и Р оборудования на период с " + start.ToShortDateString() + " по " + end.ToShortDateString(), 
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
                        MakeQrForXls(headerY-1, headerX-1,
                            work.FutureDate.ToShortDateString() + "\n" + work.Machine + "\n" + work.Name + "\n" + work.Id);
                        PrintHorizontalLine(headerY, headerX+1,
                        work.FutureDate.ToShortDateString(), work.Machine, work.Name, work.Type, work.Materials, work.WorkingHours, work.Operators);
                        headerY++;

                    }
                    else if (item is MaintenanceEpisodeView)
                    {
                        var episode = (MaintenanceEpisodeView)item;
                        MakeQrForXls(headerY-1, headerX-1,
                            episode.FutureDate.ToShortDateString() + "\n" + episode.Machine + "\n" + episode.Name + "\n" + episode.Id);
                        var m = maintenances.FirstOrDefault(n => n.Id == episode.MaintenanceId);
                        string materials = m != null ? m.Materials : "";
                        PrintHorizontalLine(headerY, headerX+1,
                        episode.FutureDate.ToShortDateString(), episode.Machine, episode.Name, episode.Type, materials, episode.WorkingHours.ToString(), episode.Operator);
                        headerY++;
                    }
                }
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

            headerY = MakeHeader(headerY, headerX+1,
                "План работ по ТО и Р оборудования на период с " + start.ToShortDateString() + " по " + end.ToShortDateString(),
                sList.ToArray());

            int number = 1;
            foreach (var view in orderedByMachine)
            {
                string[] line = new string[dList.Count+3];
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
                                line[i+3] = item.Type + "/" + item.GetWorkingHours().ToString();
                            }
                        }                        

                    }
                    MakeQrForXls(headerY-1, headerX-1,
                        passport.Name + "\n" + start.ToShortDateString() + "\n" + end.ToShortDateString());
                    PrintHorizontalLine(headerY, headerX+1, line);
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

            headerY = MakeHeader(headerY, headerX+1,
                "Наряд на работы на " + day.ToShortDateString(),
                "№ п/п", "Наименование оборудования", "Наименование выполняемых работ", "Вид работ", "Требуемые ТМЦ", "Трудоемкость плановая, ч", "Трудоемкость фактическая, ч", "Наработка оборудования, ч", "Работы выполнил ФИО");

            List<IPlanedView> temp = MakePlannedList(day, day, planned, out List<MaintenanceNewView> maintenances);

            int number = 1;
            foreach (var item in temp)
            {
                if (item is AdditionalWorkView)
                {
                    var work = (AdditionalWorkView)item;
                    MakeQrForXls(headerY-1, headerX-1, 
                        DateTime.Today.ToShortDateString() + "\n" + work.Machine + "\n" + work.Name + "\n" + work.Id);
                    PrintHorizontalLine(headerY, headerX+1,
                    number.ToString(), work.Machine, work.Name, work.Type, work.Materials, work.WorkingHours);
                    headerY++;

                }
                else if (item is MaintenanceEpisodeView)
                {
                    var episode = (MaintenanceEpisodeView)item;
                    MakeQrForXls(headerY-1, headerX-1, 
                        DateTime.Today.ToShortDateString() + "\n" + episode.Machine + "\n" + episode.Name + "\n" + episode.Id);
                    var m = maintenances.FirstOrDefault(n => n.Id == episode.MaintenanceId);
                    string materials = m != null ? m.Materials : "";
                    PrintHorizontalLine(headerY, headerX+1,
                    number.ToString(), episode.Machine, episode.Name, episode.Type, materials, episode.WorkingHours.ToString());
                    headerY++;
                }
                number++;
            }

            FinalMaking(sheet, package, name);
            StartProcess(name);
        }

        public void PrintInstrumentsForm(string passportName, List<InstrumentView> instruments)
        {
            string name = "Инструменты" + "--" + DateTime.Now.ToString("dd-MM-yy(hh-mm-ss)");
            sheet.DefaultColWidth = 6.5;

            int headerY = 4;
            int headerX = 1;
            headerY = MakeHeader(headerY, headerX + 1, "Инструменты/оснастка оборудования: " + passportName,
                "№", "Наименование", "Артикул", "Количество", "Ед. измерения",
                "Дата добавления", "Комментарий");

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
                headerY = MakeHeader(headerY, headerX + 1, "Инструментя/оснастка оборудования: " + passportName,
                    "№", "Наименование", "Артикул", "Количество", "Ед. измерения",
                    "Дата добавления", "Комментарий", "Дата удаления", "Причина удаления");

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
                "Наименование оборудования: " + passportName,
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
            sheet.Cells[Y, X, Y, result-1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Cells[Y, X, Y, result-1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            return result;
        }
        private int PrintHorizontalLineItem(int Y, int X, string value)
        {
            double width = sheet.Column(X).Width;
            sheet.Cells[Y, X].Value = value;                                   
            sheet.Cells[Y, X].Style.WrapText = true;
            //sheet.Column(X).AutoFit();
            int letters = value.Length;
            //double newWidth = sheet.Column(X).Width * 0.8;            
            sheet.Column(X).Width = letters > width? letters: width;
            sheet.Column(X).Width = sheet.Column(X).Width > 60 ? 60 : sheet.Column(X).Width;

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
                var picture = sheet.Drawings.AddPicture("qr"+Y, qrBitmap);
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

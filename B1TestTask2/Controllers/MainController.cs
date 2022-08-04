using B1TestTask2.Data;
using B1TestTask2.Data.Models;
using B1TestTask2.Data.ViewModels;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B1TestTask2.Controllers
{
    public class MainController : Controller
    {

        private readonly IWebHostEnvironment hostEnvironment;
        private readonly DataContext dataContext;

        public MainController(IWebHostEnvironment hostEnvironment, DataContext dataContext)
        {
            this.hostEnvironment = hostEnvironment;
            this.dataContext = dataContext;

        }
        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel();
            //Получение списка загруженных файлов
            model.Files = await dataContext.Files.ToListAsync();
            return await Task.Run(() => View(model));
        }

        [HttpPost]
        public async Task<IActionResult> LoadFile(IndexViewModel model)
        {
            //Генерация имени нового файла
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.File.FileName);
            string extension = Path.GetExtension(model.File.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/files/", fileName);
            //Копирования файла из формы по сгенерированному пути
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.File.CopyToAsync(fileStream);
            }
            //Добавление информации о файлу в базу данных
            var fileId = Guid.NewGuid().ToString();
            var file = new FileXlsx() { Name = fileName,  Id = fileId};
            await dataContext.Files.AddAsync(file);
            await dataContext.SaveChangesAsync();

            //Открытие файла Excel
            using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(path, false))
            {
               //Получение WorkbookPart
                WorkbookPart workbookPart = myWorkbook.WorkbookPart;

                // Получение первого рабочего листи. 
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.ElementAt(0);

                // Объект SheetData хранит всю информацию.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                //Парсинг Excel документа
                var classId = 0;
                var columnIndex = 1;
                //Пострчный обход по первому столбцу
                foreach (Row row in sheetData.Elements<Row>())
                {
                    var cell = row.Elements<Cell>().SingleOrDefault(p => p.CellReference == GetLetterOfColumn(columnIndex) + row.RowIndex);
                    if (cell != null)
                    {
                        //Если в ячейке есть запись о классе, то текущим классом для записи становится этот класс
                        var balanceAccountId = GetVal(cell, myWorkbook.WorkbookPart);
                        if (balanceAccountId.Contains("КЛАСС  1"))
                        {
                            classId = 1;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  2"))
                        {
                            classId = 2;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  3"))
                        {
                            classId = 3;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  4"))
                        {
                            classId = 4;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  5"))
                        {
                            classId = 5;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  6"))
                        {
                            classId = 6;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  7"))
                        {
                            classId = 7;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  8"))
                        {
                            classId = 8;
                            continue;
                        }
                        if (balanceAccountId.Contains("КЛАСС  9"))
                        {
                            classId = 9;
                            continue;
                        }
                        //Все итоговые записи опускаются при записи в БД
                        if (balanceAccountId.Contains("ПО КЛАССУ"))
                        {
                            continue;
                        }
                        if (balanceAccountId.Contains("БАЛАНС"))
                        {
                            break;
                        }

                        if (classId > 0 && balanceAccountId.Length > 2)
                        {
                            //Формируем объект для записи в бд
                            BalanceАccount balanceАccount = new BalanceАccount();

                            //Получнеие номера Б/сч. из документа
                            balanceАccount.BalacnceAccountNumber = balanceAccountId;

                            //Генерация Id
                            balanceАccount.Id = Guid.NewGuid().ToString();

                            //Установка текущего класса
                            balanceАccount.ClassId = classId.ToString();

                            //Получение оставшихся числовых данных из документа
                            var activeIncomingBalanceCell = row.Elements<Cell>().SingleOrDefault(p => p.CellReference == GetLetterOfColumn(columnIndex + 1) + row.RowIndex);
                            var activeIncomingBalance = GetVal(activeIncomingBalanceCell, myWorkbook.WorkbookPart);

                            var passiveIncomingBalanceCell = row.Elements<Cell>().SingleOrDefault(p => p.CellReference == GetLetterOfColumn(columnIndex + 2) + row.RowIndex);
                            var passiveIncomingBalance = GetVal(passiveIncomingBalanceCell, myWorkbook.WorkbookPart);

                            balanceАccount.IncomingBalance = new IncomingBalance() { Id = Guid.NewGuid().ToString() ,Active = Convert.ToDecimal(activeIncomingBalance.Replace(".",",")), Passive = Convert.ToDecimal(passiveIncomingBalance.Replace(".", ",")), BalanceAccountId = balanceAccountId};


                            var debitCell = row.Elements<Cell>().SingleOrDefault(p => p.CellReference == GetLetterOfColumn(columnIndex + 3) + row.RowIndex);
                            var debit = GetVal(debitCell, myWorkbook.WorkbookPart);

                            var creditCell = row.Elements<Cell>().SingleOrDefault(p => p.CellReference == GetLetterOfColumn(columnIndex + 4) + row.RowIndex);
                            var credit = GetVal(creditCell, myWorkbook.WorkbookPart);

                            balanceАccount.Turnover = new Turnover() { Id = Guid.NewGuid().ToString(), Debit = Convert.ToDecimal(debit.Replace(".", ",")), Credit = Convert.ToDecimal(credit.Replace(".", ",")), BalanceAccountId = balanceAccountId};

                            //Связывание записи с текущим файлой
                            balanceАccount.FileId = fileId;
                            balanceАccount.File = file;
                            // Добавление в БД
                            await dataContext.BalanceАccounts.AddAsync(balanceАccount);
                            await dataContext.SaveChangesAsync();
                        }
                    }

                }


            }
            return RedirectToAction(nameof(Index));
        }

        //Получение значения, которое лежит в ячейке
        private string GetVal(Cell cell, WorkbookPart wbPart)
        {
            string value = cell.InnerText;

            if (cell.DataType == null)
            {
                return value;
            }
            switch (cell.DataType.Value)
            {
                case CellValues.SharedString:

                    var stringTable =
                        wbPart.GetPartsOfType<SharedStringTablePart>()
                            .FirstOrDefault();

                    if (stringTable != null)
                    {
                        value =
                            stringTable.SharedStringTable
                                .ElementAt(int.Parse(value)).InnerText;
                    }
                    break;
            }

            return value;
        }

        //Получение буквы по индексу
        private string GetLetterOfColumn(int numberOfColumn)
        {
            switch (numberOfColumn)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                default: return "F";
            }
        }

        //Отображение данных из файла
        public async Task<IActionResult> ShowFile(string id)
        {
            ShowFileViewModel model = new ShowFileViewModel();
            //Получение необходимых данных из БД
            model.Records = await dataContext.BalanceАccounts.Where(balanceAccount => balanceAccount.FileId == id)
                                       .Include(b => b.IncomingBalance).Include(b => b.Turnover).Include(b => b.Class).Select(b => new Record()
                                       {
                                           ClassName = b.Class.Name,
                                           Credit = b.Turnover.Credit,
                                           Debit = b.Turnover.Debit,
                                           Id = b.BalacnceAccountNumber,
                                           IncomingActive = b.IncomingBalance.Active,
                                           IncomingPassive = b.IncomingBalance.Passive,
                                           //Подсчёт выходного сальдо
                                           OutgoingActive = b.IncomingBalance.Active != 0 ? (b.Turnover.Debit - b.Turnover.Credit + b.IncomingBalance.Active) : (0),
                                           OutgoingPassive = b.IncomingBalance.Passive != 0 ? (b.Turnover.Credit - b.Turnover.Debit + b.IncomingBalance.Passive) : (0)
                                       }).ToListAsync();
            return await Task.Run(() => View(model));
        }

        //Скачивание файла с сервера
        public async Task<FileResult> DownloadFile(string id)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            //Получение данных о файле из БД
            FileXlsx? file = await dataContext.Files.FirstOrDefaultAsync(file => file.Id == id);
            string path = Path.Combine(wwwRootPath + "/files/", file?.Name);
            //Загрузка файла с сервера
            return PhysicalFile(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{file.Name}.xlsx");
        }
    }
}

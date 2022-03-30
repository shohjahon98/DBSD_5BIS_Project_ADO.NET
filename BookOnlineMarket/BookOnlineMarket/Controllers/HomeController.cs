using ApprovalUtilities.SimpleLogger.Writers;
using BookOnlineMarket.Models;
using BookOnlineMarket.Models.Services;
using BookOnlineMarket.Models.viewModel;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BookOnlineMarket.Controllers
{
    public class HomeController : Controller
    {
        BookRepository _book = new BookRepository();


        public ActionResult Index(int page = 1)
        {
            ViewBag.ermsg = ViewMSG.ermsg;
            ViewBag.msg = ViewMSG.msg;
            int pageSize = 3;
            
            IEnumerable < Book > bookPerPages = _book.GetAllBooks().Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _book.GetAllBooks().Count };
            BookViewModel bvm = new BookViewModel { pageInfo = pageInfo, GetBooks = bookPerPages };
            return View(bvm);

        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Book book, HttpPostedFileBase uploadImage)
        {
            try
            {
                if (uploadImage != null)
                {
                    if (uploadImage.FileName.EndsWith("jpg") || uploadImage.FileName.EndsWith("jpeg") || uploadImage.FileName.EndsWith("png"))
                    {
                        byte[] imageData = null;

                        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        }

                        book.BookImg = imageData;
                        _book.AddBook(book);
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.erimgmsg = "Select an image of type jpg, jpeg, or png";return View();
                    }
                }
                else
                {
                    ViewBag.erimgmsg = "Select an image of type jpg, jpeg, or png";
                    return View();
                }

                
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            return View(_book.GetAllBooks().Find(Books => Books.Id == id));
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase uploadImage)
        {
            try
            {
                if (uploadImage == null)
                {
                    book.BookImg = _book.GetAllBooks().Find(Books => Books.Id == book.Id).BookImg;
                }
                else
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    book.BookImg = imageData;

                }
                book.AddDate = _book.GetAllBooks().Find(Books => Books.Id == book.Id).AddDate;
                _book.UpdateBook(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            return View(_book.GetAllBooks().Find(Books => Books.Id == id));
        }


        public ActionResult Delete(int id)
        {
            try
            {
                _book.DeleteBook(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ImportFile(HttpPostedFileBase importFile)
        {
            Book book = new Book();
            {
                importFile.SaveAs(Server.MapPath("~/FileFolder/" + Path.GetFileName(importFile.FileName)));
                if (importFile.FileName.EndsWith("json"))
                {
                    
                    StreamReader sr = new StreamReader(Server.MapPath("~/FileFolder/" + Path.GetFileName(importFile.FileName)));
                    string json = sr.ReadToEnd();
                    List<Book> jsondata = JsonConvert.DeserializeObject<List<Book>>(json);
                    foreach (var item in jsondata)
                    {
                        book.AddDate = item.AddDate.ToString();
                        book.Author = item.Author.ToString();
                        book.BookImg = (byte[])item.BookImg;
                        book.BookName = item.BookName.ToString();
                        book.Quentity = item.Quentity;
                        book.Price = item.Price;
                        book.Publisher = item.Publisher;
                        book.Title = item.Title;
                        book.Genre = item.Genre;
                        _book.AddBook(book);
                    }
                    ViewMSG.msg = "Saved successfuly";
                }
                if (importFile.FileName.EndsWith("xml"))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Server.MapPath("~/FileFolder/" + Path.GetFileName(importFile.FileName)));
                    XmlNodeList bookx = xmldoc.SelectNodes("Book/Book");
                    foreach (XmlNode usr in bookx)
                    {
                        book = new Book();
                        book.AddDate = usr["AddDate"].InnerText;
                        book.Author = usr["Author"].InnerText;
                        book.BookImg = (byte[])Convert.FromBase64String(usr["BookImg"].InnerText);
                        book.BookName = usr["BookName"].InnerText;
                        book.Genre = usr["Genre"].InnerText;
                        decimal l =decimal.Parse( usr["Price"].InnerText, CultureInfo.InvariantCulture);
                        book.Price =l;
                        book.Publisher = usr["Publisher"].InnerText;
                        book.Quentity = Convert.ToInt32(usr["Quentity"].InnerText);
                        book.Title = usr["Title"].InnerText;

                        _book.AddBook(book);
                    }
                    ViewMSG.msg = "Saved successfuly";
                }
                if (importFile.FileName.EndsWith("csv"))
                {
                    using (var reader = new StreamReader(Server.MapPath("~/FileFolder/" + Path.GetFileName(importFile.FileName))))
                    {
                        List<BookCSV> booklist = new List<BookCSV>();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(';');
                            if (values[0] == "Id") { }
                            else
                            {
                                BookCSV cSV = new BookCSV
                                {

                                    Id = Convert.ToInt32(values[0].ToString()),
                                    BookName = values[1].ToString(),
                                    Author = values[2].ToString(),
                                    Publisher = values[3].ToString(),
                                    Price = Convert.ToDecimal(values[4].ToString(), CultureInfo.InvariantCulture),
                                    AddDate = values[5].ToString(),
                                    Genre = values[6].ToString(),
                                    Quentity = Convert.ToInt32(values[7].ToString()),
                                    BookImg = values[8].ToString(),
                                    Title = values[9].ToString()
                                };
                                booklist.Add(cSV);
                            }
                            
                        }
                        foreach (var item in booklist)
                        {
                            book = new Book
                            {
                                AddDate = item.AddDate,
                                Author = item.Author,
                                BookImg = Convert.FromBase64String(item.BookImg),
                                BookName = item.BookName,
                                Price = item.Price,
                                Quentity = item.Quentity,
                                Id = item.Id,
                                Publisher = item.Publisher,
                                Genre = item.Genre,
                                Title = item.Title
                            };
                            _book.AddBook(book);

                        }
                    }

                }
                else
                {
                    ViewMSG.ermsg = "Select a file in Json or Xml format";
                }
                 
            }
            return RedirectToAction("Index");
           
        }
        public ActionResult ExportJson()
        {
            var json = JsonConvert.SerializeObject(_book.GetAllBooks());
            System.IO.File.WriteAllText("e:\\bookJson.json", json);
            ViewMSG.msg = "Succes";
            return RedirectToAction("Index");
        }
        public ActionResult ExportXML()
        {
            try
            {
                List<Book> books = _book.GetAllBooks();
                var xEle = new XElement("Book",
                            from s in books
                            select new XElement("Book",
                                       new XAttribute("Id", s.Id),
                                       new XElement("Author", s.Author),
                                       new XElement("BookName", s.BookName),
                                       new XElement("AddDate", s.AddDate),
                                       new XElement("BookImg", Convert.ToBase64String(s.BookImg)),
                                       new XElement("Genre", s.Genre),
                                       new XElement("Quentity", s.Quentity),
                                       new XElement("Title", s.Title),
                                       new XElement("Price", s.Price),
                                       new XElement("Publisher", s.Publisher)
                                       ));

                xEle.Save("e:\\booksXML.xml");
            }
            catch
            {
                ViewMSG.ermsg = "Error";
            }
            
            return RedirectToAction("index");

        }
        public ActionResult ExportCSV()
        {
            List<BookCSV> bookscsv = new List<BookCSV>();
            foreach (var item in _book.GetAllBooks())
            {
                BookCSV bookCSV = new BookCSV
                {
                    AddDate = item.AddDate,
                    Author = item.Author,
                    BookImg = Convert.ToBase64String(item.BookImg),
                    BookName = item.BookName,
                    Price = item.Price,
                    Quentity = item.Quentity,
                    Id = item.Id,
                    Publisher = item.Publisher,
                    Genre = item.Genre,
                    Title = item.Title
                };
                bookscsv.Add(bookCSV);
            }
            var sb = new StringBuilder();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath,"e:\\books.csv");
            var header = "";
            var info = typeof(BookCSV).GetProperties();
            if (!System.IO.File.Exists(finalPath))
            {
                var file = System.IO.File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(BookCSV).GetProperties())
                {
                    header += prop.Name + "; ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in bookscsv)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }

            
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return RedirectToAction("Index");
        }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
        }
    } 

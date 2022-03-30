using BookOnlineMarket.Models.Services;
using BookOnlineMarket.Models.viewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace BookOnlineMarket.Controllers
{
    public class SalesProcessController : Controller
    {
        SalesProcess _salesProcess = new SalesProcess();
        // GET: SalesProcess
        
        public ActionResult Index(int page = 1)
        {
            
            ViewBag.ermsg = ViewMSG.ermsg;
            int pageSize = 3;
            IEnumerable<Procces> ProcessPerPages =_salesProcess.Procees().Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _salesProcess.Procees().Count };
            ProcessViewModel pvm = new ProcessViewModel { pageInfo = pageInfo, Procces = ProcessPerPages };
            return View(pvm);
            
        }
        [HttpPost]
        public ActionResult Search(Search search,int page = 1)
        {
            List<Procces> procces;
            if (search.Author == null && search.BookName == null && search.ClientName == null && search.OrderDate == null && search.SupName == null)
            {
                procces = _salesProcess.Procees();
            }
            else
            {
                var result = from s in _salesProcess.Procees()
                             where s.Author == search.Author || s.BookName == search.BookName || s.SupName == search.SupName || s.OrderDate == search.OrderDate || s.LastName == search.ClientName || s.FirstName == search.ClientName
                             orderby s.FirstName, s.LastName, s.SupName, s.OrderDate, s.BookName, s.Author
                             select s;
                procces = result.ToList();
                ViewMSG.Author = search.Author;
                ViewMSG.BookName = search.BookName;
                ViewMSG.ClientName = search.ClientName;
                ViewMSG.SupName = search.SupName;
                ViewMSG.OrderDate = search.OrderDate;
            }
            ViewBag.ermsg = ViewMSG.ermsg;
            int pageSize = 3;
            IEnumerable<Procces> ProcessPerPages = procces.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _salesProcess.Procees().Count };
            ProcessViewModel pvm = new ProcessViewModel { pageInfo = pageInfo, Procces = ProcessPerPages,Search=search };
            return View("Index",pvm);
        }
        [HttpPost]
        public ActionResult ConvertFile(string fileType)
        {
            try
            {       List<Procces> procces;
                    if (ViewMSG.Author == null && ViewMSG.BookName == null && ViewMSG.ClientName == null && ViewMSG.OrderDate == null && ViewMSG.SupName == null)
                    {
                        procces = _salesProcess.Procees();
                    }
                    else
                    {
                        var result = from s in _salesProcess.Procees()
                                     where s.Author == ViewMSG.Author || s.BookName == ViewMSG.BookName || s.SupName == ViewMSG.SupName || s.OrderDate == ViewMSG.OrderDate || s.LastName == ViewMSG.ClientName || s.FirstName == ViewMSG.ClientName
                                     orderby s.FirstName, s.LastName, s.SupName, s.OrderDate, s.BookName, s.Author
                                     select s;
                        procces = result.ToList();
                        ViewMSG.Author = null;
                        ViewMSG.BookName = null;
                        ViewMSG.ClientName = null;
                        ViewMSG.SupName = null;
                        ViewMSG.OrderDate = null;
                    }
                if (fileType == "XML")
                {
                   
                    var xEle = new XElement("Process",
                                from s in procces
                                select new XElement("Process",
                                            new XElement("Author", s.Author),
                                            new XElement("BookName", s.BookName),
                                            new XElement("FirstName", s.FirstName),
                                            new XElement("LastName", s.LastName),
                                            new XElement("OrderDate", s.OrderDate),
                                            new XElement("Price", s.Price),
                                            new XElement("SupName", s.SupName)
                                           ));

                    xEle.Save("e:\\SalesProcessXML.xml");
                }
                else if (fileType == "JSON")
                {
                var json = JsonConvert.SerializeObject(procces);
                System.IO.File.WriteAllText("e:\\processJSON.json", json);
                ViewMSG.msg = "Succes";
                }
                else if (fileType == "CSV")
                {
                    var sb = new StringBuilder();
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var finalPath = Path.Combine(basePath, "e:\\processCSV.csv");
                    var header = "";
                    var info = typeof(Procces).GetProperties();
                    if (!System.IO.File.Exists(finalPath))
                    {
                        var file = System.IO.File.Create(finalPath);
                        file.Close();
                        foreach (var prop in typeof(Procces).GetProperties())
                        {
                            header += prop.Name + "; ";
                        }
                        header = header.Substring(0, header.Length - 2);
                        sb.AppendLine(header);
                        TextWriter sw = new StreamWriter(finalPath, true);
                        sw.Write(sb.ToString());
                        sw.Close();
                    }
                    foreach (var obj in procces)
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
                }
            }
            catch
            {
                ViewMSG.ermsg = "Error";
            }
            return RedirectToAction("index");
            
        }
       
    }
}
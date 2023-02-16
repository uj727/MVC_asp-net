using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

using prj.Models;
namespace prj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
      
        dbEmployeeEntities db = new dbEmployeeEntities();
         int pageSize = 8;
        String name = "";

        [Authorize]//設定屬性讓登入名稱可執行First
        [Authorize(Users = "1081649,1080000")]
        public String First()
		{
            return "<p>" + User.Identity.Name + "<a href='Home/Logout'>登出</a><p>" +
                                                "<img src='../pic/unnamed,jpg' width='100%'>";
		}
        
        public ActionResult Logout()
		{
            FormsAuthentication.SignOut();//登出
            return RedirectToAction("Login");//回到登入頁面
		}

        [AllowAnonymous]
        public ActionResult Login()//登入頁面
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string uid,string pas)
		{//設定帳號密碼兩個陣列 只要輸入的帳號密碼和兩陣列同位置的值相同代表登入成功
            string[] uidAry = new string[] { "1081649", "1080000" };
            string[] pasAry = new string[] { "1081649", "1080000" };

            //測輸入帳密
            int index = -1;
            for(int i=0;i<uidAry.Length;i++)
			{
				if (uidAry[i] == uid && pasAry[i] == pas)
				{
                    index = 1;
                    break;
				}
			}
            if(index==-1)//登入失敗會顯示
			{
                ViewBag.err = "帳密錯誤";
			}
			else
			{
                 name+=User.Identity.Name.ToString();
                //輸入正確帳密
                FormsAuthentication.RedirectFromLoginPage(uid, true);
                return RedirectToAction("pic");//登入後執行pic(管理系統首頁)
			}
            return View();
        }
        public ActionResult pic()//管理系統首頁
        {
            return View();
        }
      
         //病歷表管理
        public ActionResult Index(int depId=1,int page=1)
        {//紀錄點選負責醫生姓名 在點選後顯示在網頁
            ViewBag.DepName = db.TableDoctors1081649
              .Where(m => m.負責醫生編號 == depId)
              .FirstOrDefault().醫生姓名 ; 

           CVMDepEmp vm = new CVMDepEmp()
            {
               //點選負責醫生 把相同負責醫生的病歷表全列出來
                doctor = db.TableDoctors1081649.ToList(),
                patient = db.TablePatients1081649
                 .Where(m => m.負責醫生編號 == depId).ToList()
            };

            return View(vm);
        }
        //新增病例表
         public ActionResult Create()
        {
            return View(db.TableDoctors1081649.ToList());
        }

        //Post:Home/Create
        [HttpPost]
        public ActionResult Create(TablePatients1081649 emp)
        {
            //把輸入的新資料加到資料表中
            try
               {
                    db.TablePatients1081649.Add(emp);
                    db.SaveChanges();
                    return RedirectToAction
                       ("Index", new { depId = emp.負責醫生編號 });
               }
             catch (Exception ex)
                    {}
                
                return View(emp);
        }
        public ActionResult prog1()
        {
            return View();
        }

        public ActionResult toorman(string depId)
		{
          //員工管理列出所有員工
            var toorman = db.TableToorman1081649.ToList();
            return View(toorman);
		}

        //編及員工 
        public ActionResult Edittoorman(string fEmpId)
        {
            //執行時會由fempId找要修改的資料 讓資料顯示在cshtml
            var employee = db.TableToorman1081649
                .Where(m => m.員工編號 == fEmpId).FirstOrDefault();

            //紀錄點選員工的姓名MAIL編號並顯示在網頁編輯資料頁面中
            ViewBag.Name = db.TableToorman1081649
              .Where(m => m.員工編號 == fEmpId)
              .FirstOrDefault().姓名;
            ViewBag.Mail = db.TableToorman1081649
             .Where(m => m.員工編號 == fEmpId)
             .FirstOrDefault().Mail;
            ViewBag.Num = db.TableToorman1081649
            .Where(m => m.員工編號 == fEmpId)
            .FirstOrDefault().員工編號;
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edittoorman(TableToorman1081649 toorman)
        {
            //資料修改完成後用post傳資料 將更改欄位對應到新的資料並更新儲存
            if (ModelState.IsValid)//如果有傳資料
            {
                var temp = (from m in db.TableToorman1081649//將選擇的定單號碼資料回傳準備更改
                            where m.員工編號 == toorman.員工編號
                            select m)
                           .FirstOrDefault();
                //更新改變的欄位內容
                temp.月考績分數 = toorman.月考績分數;
                temp.Mail = toorman.Mail;
                temp.姓名 = toorman.姓名;
   
                db.SaveChanges();//儲存改變的資料庫
                return RedirectToAction("toorman");//回到員工管理顯示資料
            }
            return View(toorman);
        }

        public ActionResult Deletetoorman(string fEmpId)
        {
            //傳入fEmpId參數 找要山的資料刪除後回到index顯示其餘所有資料
            var toorman = (from m in db.TableToorman1081649
                        where m.員工編號 == fEmpId
                        select m)
                           .FirstOrDefault();
            db.TableToorman1081649.Remove(toorman);//移除資料庫
    
            db.SaveChanges();//儲存改變的資料庫
            return RedirectToAction("toorman");//回到員工管理顯示其餘所有資料
        }

        //醫療用品管理
        public ActionResult toor(int page = 1)
        {
            //上面一開始有設定一頁有8筆資料 pagesize=8
            //把資料分頁顯示
            int currentPage = page < 1 ? 1 : page;
            var products = db.TableToors1081649.OrderBy(m => m.用品編號).ToList();
            var result = products.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        public ActionResult Deletepat(string pId)
        {
            var emp = db.TablePatients1081649.Where
                (m => m.看診編號 == pId).FirstOrDefault();
            db.TablePatients1081649.Remove(emp);
            db.SaveChanges();
            return RedirectToAction
                ("Index", new { Id = emp.負責醫生編號 });
        }
    }
}
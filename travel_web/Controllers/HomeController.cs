using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using travel.BusinessLayer;
using travel.Entities;
using travel.Entities.Messages;
using travel_web.Entities.ValueObjects;

namespace travel_web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            travel.BusinessLayer.Test test = new travel.BusinessLayer.Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            test.CommentTest();

            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Travel()
        {
            return View();
        }
        public ActionResult AboutMe()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Vegas()
        {
            return View();
        }
        public ActionResult SantaMon()
        {
            return View();
        }
        public ActionResult NYC()
        {
            return View();
        }
        public ActionResult DC()
        {
            return View();
        }
        public ActionResult Leesburg()
        {
            return View();
        }
        public ActionResult LA()
        {
            return View();
        }
        public ActionResult Video()
        {
            return View();
        }
        public ActionResult Eating()
        {
            return View();
        }
        public ActionResult Accommodation()
        {
            return View();
        }
        public ActionResult Writing()
        {
            return View();
        }


        public ActionResult ShowProfile()
        {
            travelUser currentUser = Session["login"] as travelUser;
            travelUserManager eum = new travelUserManager();
            BusinessLayerResult<travelUser> res = eum.GetUserById(currentUser.Id);

            if(res.Errors.Count > 0)
            {
                //Kullanıcıyı hata ekranına yönlendirme.
            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(travelUser user)
        {
            return View();
        }

        public ActionResult RemoveProfile()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                travelUserManager eum = new travelUserManager();
                BusinessLayerResult<travelUser> res = eum.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code ==ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-78980";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index");

            }
            //Giriş kontrolü ve yönlendirme...
            //Session'a kullanıcı bilgi saklama...
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                travelUserManager eum = new travelUserManager();
                BusinessLayerResult<travelUser> res = eum.RegisterUser(model);

                if(res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                
                //travelUser user = null;

                //try
                //{
                //    user = eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}
                //if(model.Username == "aaa")
                //{
                //    ModelState.AddModelError("", "Kullanıcı adı kullanılıyor.");
                //}

                //if(model.Email == "aaa@aa.com")
                //{
                //    ModelState.AddModelError("", "E-posta adresi kullanılıyor.");
                //}
                 
                //foreach (var item in ModelState)
                //{
                //    if(item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}

                //if(user == null)
                //{
                //    return View(model);
                //}
                return RedirectToAction("RegisterOk");
            }

            //Kullanıcı username kontrolü...
            //Kullanıcı e-posta kontrolü...
            //Kayıt işlemi...
            //Aktivasyon e-postası gönderimi.
            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid id)
        {
            travelUserManager eum = new travelUserManager();
            BusinessLayerResult<travelUser> res = eum.ActivateUser(id);

            if(res.Errors.Count > 0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction("UserActivateCancel");
            }
            return RedirectToAction("UserActivateOk");
        }

        public ActionResult UserActivateOk()
        {
            return View();
        }

        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObj> errors = null;

            if (TempData["errors"] != null)
            {
               errors = TempData["errors"] as List<ErrorMessageObj>;
            }

            return View(errors);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
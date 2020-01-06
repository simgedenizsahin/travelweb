using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travel.DataAccessLayer.EntityFramework;
using travel.Entities;
using travel.Entities.Messages;
using travel_web.Common.Helpers;
using travel_web.Entities.ValueObjects;

namespace travel.BusinessLayer
{
    public class travelUserManager
    {
        private Repository<travelUser> repo_user = new Repository<travelUser>();

        public BusinessLayerResult<travelUser> RegisterUser(RegisterViewModel data)
        {
            travelUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<travelUser> res = new BusinessLayerResult<travelUser>();

            if (user != null)
            {
                if(user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.usernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if(user.Email==data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailALreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = repo_user.Insert(new travelUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "system",
                    IsActive = false,
                    IsAdmin = false
                });

                if(dbResult >1)
                {
                    res.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);
                    //TODO aktivasyon maili atılacak..
                    //layerResult.Result.ActivateGuid

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";
                    MailHelper.SendMail(body,res.Result.Email,"travelWeb Hesap Aktifleştirme");
                }
            }
            return res;
        }

        public BusinessLayerResult<travelUser> GetUserById(int id)
        {
            BusinessLayerResult<travelUser> res = new BusinessLayerResult<travelUser>();
            res.Result = repo_user.Find(x => x.Id == id);

            if(res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "User not found.");
            }

            return res;
        }

        public BusinessLayerResult<travelUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<travelUser> res = new BusinessLayerResult<travelUser>();
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (res.Result !=null)
            {
                if(res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor.");
            }
            return res;
        }

        public BusinessLayerResult<travelUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<travelUser> res = new BusinessLayerResult<travelUser>();
            res.Result = repo_user.Find(x => x.ActivateGuid == activateId); 

            if(res.Result != null)
            {
                if(res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }
                res.Result.IsActive = true;
                repo_user.Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }
    }
}

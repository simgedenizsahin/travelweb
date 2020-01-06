using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel_web.ViewModels
{
    public class okViewModel : NotifyViewModelBase<string>
    {
        public okViewModel()
        {
            Title = "İşlem Başarılı.";
        }
    }
}
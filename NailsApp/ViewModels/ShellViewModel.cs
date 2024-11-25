using NailsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.ViewModels
{
    public class ShellViewModel
    {
        private NailsWebAPIProxy proxy;
        public ShellViewModel(NailsWebAPIProxy proxy_) 
        {
            proxy =proxy_;
        }
    }
}

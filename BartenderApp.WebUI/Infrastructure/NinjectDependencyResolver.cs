using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using BartenderApp.Domain.Abstract;
using BartenderApp.Domain.Concrete;
using BartenderApp.Domain.Entities;
using System.Web.Configuration;

namespace BartenderApp.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam){
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType){
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType){
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(WebConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
    }
    }
}
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Helpers
{
    public class AutofacContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IAutoDependencyService).Assembly)
             .AssignableTo<IAutoDependencyService>()
             .As<IAutoDependencyService>()
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

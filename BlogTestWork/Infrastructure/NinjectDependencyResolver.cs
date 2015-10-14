using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTestWork.Models;
using BlogTestWork.Models.DbModels;
using Ninject;
using Ninject.Web.Common;

namespace BlogTestWork.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            bind();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void bind()
        {
            _kernel
                .Bind<ICommentService>()
                .To<CommentService>()
                .InRequestScope();
//                .WithConstructorArgument("context", new BlogContext());
        }
    }
}
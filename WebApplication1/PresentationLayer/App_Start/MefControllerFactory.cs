using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace PresentationLayer.Bootstrapper
{
    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _container;
        public MefControllerFactory(CompositionContainer container)
        {
            _container = container;
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            Lazy<object, object> export = _container.GetExports(controllerType, null, null).FirstOrDefault();

            return null == export
                                ? base.GetControllerInstance(requestContext, controllerType)
                                : (IController)export.Value;
        }
        public override void ReleaseController(IController controller)
        {
            ((IDisposable)controller).Dispose();
        }
    }
}

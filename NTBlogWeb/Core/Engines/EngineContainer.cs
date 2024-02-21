using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Core.Engines
{
    public class EngineContainer : IEngine
    {
        private readonly IContainer _container;

        public EngineContainer(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return Scope().Resolve<IEnumerable<T>>();
        }

        public object GetInstance(Type type)
        {
            return Scope().Resolve(type);
        }

        public T GetInstance<T>() where T : class
        {
            return Scope().Resolve<T>();
        }

        /// <summary>
        /// Get current scope
        /// </summary>
        /// <returns>Scope</returns>
        public virtual ILifetimeScope Scope()
        {
            return _container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
        }
    }
}
using System;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NTBlogWeb.Core
{
    public class EntityContext //: DbContext
    {
        //public EntityContext() : base("name=DefaultConnectionString") { }
        //public EntityContext(string nameOrConnectionString) : base(nameOrConnectionString) {
        //    Database.SetInitializer<EntityContext>(null);
        //    //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContext>());
        //}
        /// <summary>
        /// This method is called when the model for a derived context has been initialized, 
        /// but before the model has been locked down and used to initialize the context. 
        /// The default implementation of this method does nothing, 
        /// but it can be overridden in a derived class such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
        //        .Where(type => !string.IsNullOrEmpty(type.Namespace))
        //        .Where(type => type.BaseType != null && 
        //        type.BaseType.BaseType != null && 
        //        type.BaseType.BaseType.IsGenericType && 
        //        type.BaseType.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();
        //    foreach (var type in typesToRegister)
        //    {
        //        dynamic configurationInstance = Activator.CreateInstance(type);
        //        modelBuilder.Configurations.Add(configurationInstance);
        //    }
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
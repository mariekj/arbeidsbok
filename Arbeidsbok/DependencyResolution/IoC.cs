using Raven.Client;
using Raven.Client.Embedded;
using StructureMap;
namespace Arbeidsbok {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
            //                x.For<IExample>().Use<Example>();
                            x.For<IDocumentStore>().Singleton().Use(y=>
                                                                    {
                                                                        var store = new EmbeddableDocumentStore();                                                                                                             
                                                                        store.Initialize();
                                                                        return store;
                                                                    });
                            x.For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(y => y.GetInstance<IDocumentStore>().OpenSession());

                        });
            return ObjectFactory.Container;
        }
    }
}
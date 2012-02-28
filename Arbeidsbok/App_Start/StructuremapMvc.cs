using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Arbeidsbok.App_Start.StructuremapMvc), "Start")]

namespace Arbeidsbok.App_Start {
    public static class StructuremapMvc {
        public static void Start() {

        }
    }
}
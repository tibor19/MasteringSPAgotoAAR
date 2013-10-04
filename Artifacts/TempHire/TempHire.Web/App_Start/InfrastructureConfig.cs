using System.Web.Optimization;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(TempHire.Web.InfrastructureConfig), "PreStart")]

namespace TempHire.Web
{
    public static class InfrastructureConfig
    {
        public static void PreStart()
        {
            // Add your start logic here
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
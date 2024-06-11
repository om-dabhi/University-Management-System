namespace UMS.DAL
{
    public class DAL_Helper
    {
        public static string myConnectionString = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build().GetConnectionString("myConnectionString");
    }
}

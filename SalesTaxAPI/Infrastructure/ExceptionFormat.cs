using System;
namespace SalesTaxClientAPI.Infrastructure
{
    public static class ExceptionFormat
    {
        public static string FullInformation(Exception ex)
        {
            return string.Format("Exception Message : {0} , Inner exception : {1} , Source : {2} , Stack trace: {3}", ex.Message, ex.InnerException?.Message, ex?.Source, ex?.StackTrace + ex?.Data);
        }
        public static string BasicInformation(Exception ex)
        {
            return string.Format("Exception Message : {0} , Inner exception : {1}, Source : {2}", ex.Message, ex.InnerException?.Message, ex?.Source);
        }
        public static string ExceptionMessage(Exception ex)
        {
            return string.Format("{0}  {1}", ex.Message, ex.InnerException?.Message);
        }
    }

}

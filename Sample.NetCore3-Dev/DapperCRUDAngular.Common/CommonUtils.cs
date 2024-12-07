using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DapperCRUDAngular.Common
{
    public class CommonUtils
    {
        public static string PagingQueryClause(int pageIndex, int pageSize)
        {
            return $"OFFSET { pageSize * (pageIndex - 1)} ROWS FETCH NEXT { pageSize} ROWS ONLY";
        }

        //public static string GetHashValue(string transaction)
        //{
        //    using (var algorithm = SHA512Managed.Create())
        //    {
        //        byte[] hash;
        //        var datab = Encoding.UTF8.GetBytes(transaction);
        //        using (SHA512 shaM = new SHA512Managed())
        //        {
        //            hash = shaM.ComputeHash(datab);
        //        }

        //        return BitConverter.ToString(hash).Replace("-", "").ToLower();
        //    }
        //}

        //public static string GenerateHashValue(string jsonObject)
        //{
        //    byte[] key = Encoding.UTF8.GetBytes("ba42d518582b49915af85489ad3da607fd8f5298898e91c7124a865f3612efd3");

        //    using (var algorithm = new HMACSHA256(key))
        //    {
        //        byte[] hash;
        //        var datab = Encoding.UTF8.GetBytes(jsonObject);
        //        hash = algorithm.ComputeHash(datab);

        //        return Convert.ToBase64String(hash);
        //    }
        //}
        public static string GetHashValue(string transaction)
        {
            using (var algorithm = SHA512Managed.Create())
            {
                byte[] hash;
                var datab = Encoding.UTF8.GetBytes(transaction);
                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(datab);
                }

                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        // public static string GenerateHashValue(string jsonObject)
        public static string GenerateHashValue(string jsonObject, IConfiguration appConfig)
        {
            
            byte[] key = Encoding.UTF8.GetBytes(appConfig["GeneratedHashKey"]);
            using (var algorithm = new HMACSHA256(key))
            {
                byte[] hash;
                var datab = Encoding.UTF8.GetBytes(jsonObject);
                hash = algorithm.ComputeHash(datab);

                return Convert.ToBase64String(hash);
            }
        }

        //public static string SaveException(ex,ILogExceptionService _logExceptionService, LogException logException, ExceptionType exceptionType)
        //{
        //    ObjLogException.ex
        //    long id = await _logExceptionService.AddAsync(ObjLogException);
        //    statuscode = id > 0 ? 200 : 400;
        //    ResponseMsg = id > 0 ? "Exception Saved To DB" : "Saved To file";
        //}

        public static string TransactionId()
        {
            Random rand = new Random();
            string randomId = (rand.Next() + DateTime.Now.Millisecond).ToString();
            string trasactionIdToHash = "artDom" + randomId;
            return GetHashValue(trasactionIdToHash).Substring(0, 12);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace rtcreditcard.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/

        public ActionResult Index()
        {
            ViewBag.Sign = GetHash();
            return View();
        }

        public ActionResult Result()
        {
            return View();
        }

        private string GetHash()
        {
            StringBuilder sb = new StringBuilder("M>)\"&nR7")
            .Append("02700701127375000697")
            .Append("txn001")
            .Append("50.00");
            byte[] b = Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            SHA512 x = SHA512.Create();
            byte[] r = x.ComputeHash(b);
            string k = "";

            foreach (byte v in r)
            {
                k += string.Format("{0:x2}", v);
            }

            return k;
        }
    }
}

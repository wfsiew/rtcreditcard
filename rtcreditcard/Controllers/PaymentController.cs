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
            .Append("TX0002")
            .Append("0.01");
            byte[] b = Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            SHA512Managed x = new SHA512Managed();
            byte[] r = x.ComputeHash(b);
            byte[] hash = x.Hash;
            x.Dispose();

            sb.Clear();

            foreach (byte v in hash)
            {
                sb.Append(v.ToString("x2"));
            }

            string k = sb.ToString();
            return k;
        }
    }
}

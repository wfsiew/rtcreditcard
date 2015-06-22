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
            string g = Guid.NewGuid().ToString();
            string signature = "TXNRT-" + g;
            ViewBag.TXNID = signature;
            ViewBag.Sign = GetHash(signature);
            return View();
        }

        public ActionResult Result()
        {
            var keys = Request.Form.Keys;
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (string k in keys)
            {
                d[k] = Request.Form[k];
            }

            string h = GetHash2(d["MERCHANT_TRANID"], d["TRANSACTION_ID"]);

            ViewBag.Dic = d;
            return View();
        }

        private string GetHash(string txnid)
        {
            StringBuilder sb = new StringBuilder("M>)\"&nR7")
            .Append("02700701127375000697")
            .Append(txnid)
            .Append("500.65");
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

        private string GetHash2(string txnid, string tid)
        {
            StringBuilder sb = new StringBuilder("M>)\"&nR7")
            .Append("02700701127375000697")
            .Append(txnid)
            .Append("500.65")
            .Append(tid);
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

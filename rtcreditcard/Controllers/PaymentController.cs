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
            ViewBag.TRANSACTION_ID = Request.Form["TRANSACTION_ID"];
            ViewBag.TXN_STATUS = Request.Form["TXN_STATUS"];
            ViewBag.TXN_SIGNATURE = Request.Form["TXN_SIGNATURE"];
            ViewBag.AUTH_ID = Request.Form["AUTH_ID"];
            ViewBag.TRAN_DATE = Request.Form["TRAN_DATE"];
            ViewBag.AUTH_DATE = Request.Form["AUTH_DATE"];
            ViewBag.RESPONSE_CODE = Request.Form["RESPONSE_CODE"];
            ViewBag.RESPONSE_DESC = Request.Form["RESPONSE_DESC"];
            ViewBag.MERCHANT_TRANID = Request.Form["MERCHANT_TRANID"];
            ViewBag.CUSTOMER_ID = Request.Form["CUSTOMER_ID"];
            ViewBag.FR_LEVEL = Request.Form["FR_LEVEL"];
            ViewBag.FR_SCORE = Request.Form["FR_SCORE"];

            var keys = Request.Form.Keys;
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (string k in keys)
            {
                d[k] = Request.Form[k];
            }

            ViewBag.Dic = d;
            return View();
        }

        private string GetHash(string txnid)
        {
            StringBuilder sb = new StringBuilder("M>)\"&nR7")
            .Append("02700701127375000697")
            .Append(txnid)
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

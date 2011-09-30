using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwilioRest;
using System.Configuration;
using System.Collections;

namespace MeterMaid
{
    public partial class Execute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            Account account = new Account(ConfigurationManager.AppSettings["TwilioAccountSid"], ConfigurationManager.AppSettings["TwilioAuthToken"]);

            foreach (Reminder r in db.Reminders.Where(z => z.DueTime <= DateTime.UtcNow))
            {
                Hashtable data = new Hashtable();
                data.Add("To", r.PhoneNumber);
                data.Add("From", ConfigurationManager.AppSettings["TwilioNumber"]);
                data.Add("Body", "Your parking meter will expire in 15 minutes.");

                account.request(string.Format("/2010-04-01/Accounts/{0}/SMS/Messages", ConfigurationManager.AppSettings["TwilioAccountSid"]), "POST", data);

                db.Reminders.DeleteOnSubmit(r);
            }

            db.SubmitChanges();
        }
    }
}
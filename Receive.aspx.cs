using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using TwilioRest;

namespace MeterMaid
{
    public partial class Receive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            Account account = new Account(ConfigurationManager.AppSettings["TwilioAccountSid"], ConfigurationManager.AppSettings["TwilioAuthToken"]);

            if (Request["Body"].Trim().ToLower() == "" ||
                Request["Body"].Trim().ToLower() == "help")
            {
                Hashtable values1 = new Hashtable();
                values1.Add("To", Request["From"]);
                values1.Add("From", ConfigurationManager.AppSettings["TwilioNumber"]);
                values1.Add("Body", "Hi I'm Meter Maid. Send me a text with how much time your parking meter has left and I will remind you before it expires. Example: \"2 hours\" or \"30 minutes\".");

                account.request(string.Format("/2010-04-01/Accounts/{0}/SMS/Messages", ConfigurationManager.AppSettings["TwilioAccountSid"]), "POST", values1);
            }
            else if (Request["Body"].Trim().ToLower() == "time")
            {
                try
                {
                    Reminder r = db.Reminders.Where(z => z.PhoneNumber == Request["From"]).First();

                    Hashtable data = new Hashtable();
                    data.Add("To", r.PhoneNumber);
                    data.Add("From", ConfigurationManager.AppSettings["TwilioNumber"]);
                    data.Add("Body", "You have " + r.DueTime.Subtract(DateTime.UtcNow).Minutes + " minutes left on your parking meter.");

                    account.request(string.Format("/2010-04-01/Accounts/{0}/SMS/Messages", ConfigurationManager.AppSettings["TwilioAccountSid"]), "POST", data);
                }
                catch (Exception ex)
                {
                    // could not be found
                }
            }
            else
            {
                try
                {
                    Reminder data = new Reminder();

                    data.ReminderID = Guid.NewGuid();
                    data.PhoneNumber = Request["From"];
                    data.DueTime = ParseDueTime(Request["Body"]);
                    data.CreatedDate = DateTime.UtcNow;

                    db.Reminders.InsertOnSubmit(data);

                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Hashtable values2 = new Hashtable();
                    values2.Add("To", Request["From"]);
                    values2.Add("From", ConfigurationManager.AppSettings["TwilioNumber"]);
                    values2.Add("Body", "I couldn't understand that, please try again. Text \"help\" for more information.");

                    account.request(string.Format("/2010-04-01/Accounts/{0}/SMS/Messages", ConfigurationManager.AppSettings["TwilioAccountSid"]), "POST", values2);

                    return;
                }

                Hashtable values3 = new Hashtable();
                values3.Add("To", Request["From"]);
                values3.Add("From", ConfigurationManager.AppSettings["TwilioNumber"]);
                values3.Add("Body", "OK, got it. I will text you 15 minutes before your meter expires.");

                account.request(string.Format("/2010-04-01/Accounts/{0}/SMS/Messages", ConfigurationManager.AppSettings["TwilioAccountSid"]), "POST", values3);
            }
        }

        private DateTime ParseDueTime(string value)
        {
            int duration = Convert.ToInt32(value.Split(Convert.ToChar(" "))[0]);
            string unit = value.Split(Convert.ToChar(" "))[1];

            DateTime result = DateTime.UtcNow;

            switch (unit)
            {
                case "minute":
                    result = result.AddMinutes(duration);
                    break;
                case "minutes":
                    result = result.AddMinutes(duration);
                    break;
                case "hour":
                    result = result.AddHours(duration);
                    break;
                case "hours":
                    result = result.AddHours(duration);
                    break;
                default:
                    throw new Exception("Invalid DateTime format for '" + value + "'.");
            }

            return result;
        }
    }
}
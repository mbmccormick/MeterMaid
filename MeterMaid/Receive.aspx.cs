using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeterMaid
{
    public partial class Receive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            Reminder data = new Reminder();

            data.ReminderID = Guid.NewGuid();
            data.PhoneNumber = Request["From"];
            data.DueTime = ParseDueTime(Request["Body"]);
            data.CreatedDate = DateTime.UtcNow;

            db.Reminders.InsertOnSubmit(data);

            db.SubmitChanges();
        }

        private DateTime ParseDueTime(string value)
        {
            int duration = Convert.ToInt32(value.Split(Convert.ToChar(" "))[0]);
            string unit = value.Split(Convert.ToChar(" "))[1];

            DateTime result = DateTime.UtcNow;

            switch (unit)
            {
                case "minutes":
                    result = result.AddMinutes(duration);
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
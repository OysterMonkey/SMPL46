using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMPL46.Controllers;

namespace SMPL46.Reports
{
    public partial class AHInspImage : System.Web.UI.Page
    {
        private AccountDataService ds = new AccountDataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            BuildInspPhoto(Request.QueryString["id"]);
        }

        private void BuildInspPhoto(string strID)
        {
            DataTable dt = ds.GetAHInspectionPhoto(strID);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    Byte[] bytes = (Byte[])dt.Rows[0][0];
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "image";
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}
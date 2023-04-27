using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace csharplis
{
    /// <summary>
    /// Summary description for revc
    /// </summary>
    public class revf : IHttpHandler
    {

        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        string labtestd = "";
        string testn = "";
        public void ProcessRequest(HttpContext context)
        {
            string devid = context.Request["devid"];
            try
            {

                //string devid1 = context.Request.Params["devid"];

                //using (var reader= new StreamReader(context.Request.InputStream))
                //{
                //    String val = reader.ReadToEnd();
                //}


                //            string df1 = "MSH|^~\\&|Mindray|BS-200|||20181031160659||ORU^R01|26|P|2.3.1||||0||ASCII|||"+
                //"PID | 26 |||| EX5094 ||||||||||||||||||||||||||"+
                //"OBR | 1 || 25 | Mindray ^ BS - 200 | N || 20181031155645 |||||||| Serum |||||||||||||||||||||||||||||||||"+
                //"OBX | 1 | NM | AST | AST | 29.133164 | U / L | 7.000000 - 40.000000 | Normal ||| F || 29.133164 | 20181031155645 ||||";


                //var msg = new Message1();
                //msg.Parse(devid);
                //char[] MyChar = { '/', '"', ' ' };
                //if (!String.IsNullOrEmpty(devid))
                //{
                //    devid = devid.Trim(MyChar);
                //}

                //devid = Regex.Replace(devid,@"\s","");
                // devid = devid.Replace(" ", "");

                String[] mek = devid.Split(' ');

                mek = mek.Where(x => x != "").ToArray();
                string pid = mek[0];
                pid = pid.Substring(5);
                if (mek[10]== "HbA1c#")
                {
                    testn = mek[11];
                }
                else
                {
                    testn = mek[12];
                }
                testn = testn.Substring(0,5);
                labtestd = getlabtid("HBA1C", pid);
                setlabtest(testn, "", labtestd, pid);

              

                //testn = msg.Messagn(5, 43);
                //labtestd = getlabtid("plcc", pid);
                //setlabtest(testn, "", labtestd, pid);

                //testn = msg.Messagn(5, 44);
                //labtestd = getlabtid("plcr", pid);
                //setlabtest(testn, "", labtestd, pid);
            }
            catch (Exception ex)
            {
                string json1 = JsonConvert.SerializeObject("error!");
                context.Response.ContentType = "text/json";
                context.Response.Write(json1);
            }

            string json = JsonConvert.SerializeObject("ok");
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
        }

        public string setlabtest(String testr, String testst, string labtestid, string pid)
        {
            sqlQuery = "UPDATE [MMS].[dbo].[Lab_Report]  SET  testResult='" + testr + "' , teststatus='" + testst + "' , Issued='1',IssuedTime=GETDATE()   WHERE  LabTestID = '" + labtestid + "' and TestSID='" + pid + "'";
            oSqlCommand = new SqlCommand();
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlCommand.Connection.Open();
            string ret = oSqlCommand.ExecuteNonQuery().ToString();

            oSqlCommand.Connection.Close();

            return ret;
        }
        public String getlabtid(String testn, String pid)
        {
            string labtestid = "";
            DataSet odsvoltxndata = new DataSet();

            oSqlConnection = new SqlConnection("data source = 135.22.210.105; initial catalog = MMS; user id = mmsuser; password = password; MultipleActiveResultSets = True;");
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT  a.[LabTestID] FROM [MMS].[dbo].[Lab_Report] as a inner join [MMS].[dbo].[Lab_SubCategory] as b on a.[LabTestID]=b.[LabTestID] where  a.TestSID='" + pid + "' and b.SubCategoryName='" + testn + "' ";

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            if (odsvoltxndata.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow r in odsvoltxndata.Tables[0].Rows)
                {
                    labtestid = r["LabTestID"].ToString();
                }
            }

            return labtestid;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
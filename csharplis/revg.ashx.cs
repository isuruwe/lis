using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace csharplis
{
    /// <summary>
    /// Summary description for revc
    /// </summary>
    public class revg : IHttpHandler
    {

        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;


        public void ProcessRequest(HttpContext context)
        {
            string devid = context.Request["devid"];
            string labtestid = "";
            string ret = "";
            try
            {

                //            string df1 = "MSH|^~\\&|Mindray|BS-200|||20181031160659||ORU^R01|26|P|2.3.1||||0||ASCII|||"+
                //"PID | 26 |||| EX5094 ||||||||||||||||||||||||||"+
                //"OBR | 1 || 25 | Mindray ^ BS - 200 | N || 20181031155645 |||||||| Serum |||||||||||||||||||||||||||||||||"+
                //"OBX | 1 | NM | AST | AST | 29.133164 | U / L | 7.000000 - 40.000000 | Normal ||| F || 29.133164 | 20181031155645 ||||";
                var msg = new Message2();
                msg.Parse(devid);
                string pid = msg.Messagd();
                string testn = msg.Messagd1();
                testn = testn.Replace("^", "");
                if (testn.ToLower().Contains("tsh"))
                {
                   testn= "TSH";
                }
                if (testn.ToLower().Contains("t3"))
                {
                    testn = "t3";
                }
                if (testn.ToLower().Contains("t4"))
                {
                    testn = "t4";
                }
                string testr = msg.Messagbr();
                string testst = msg.Messagd2();
                DataSet odsvoltxndata = new DataSet();

                oSqlConnection = new SqlConnection("data source = 135.22.210.105; initial catalog = MMS; user id = mmsuser; password = password; MultipleActiveResultSets = True;");
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  a.[LabTestID] FROM [MMS].[dbo].[Lab_Report] as a inner join [MMS].[dbo].[Lab_SubCategory] as b on a.[LabTestID]=b.[LabTestID] where  a.TestSID='" + pid + "' and b.SubCategoryName='" + testn + "'  ";

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

                sqlQuery = " UPDATE [MMS].[dbo].[Lab_Report]  SET  testResult='" + testr + "' , teststatus='" + testst + "' , Issued='1',IssuedTime=GETDATE()   WHERE  LabTestID = '" + labtestid + "' and TestSID='" + pid + "' ";
                oSqlCommand = new SqlCommand();
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.Connection.Open();
                ret = oSqlCommand.ExecuteNonQuery().ToString();
                oSqlCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                string json1 = JsonConvert.SerializeObject("error!");
                context.Response.ContentType = "text/json";
                context.Response.Write(json1);
            }

            string json = JsonConvert.SerializeObject(ret);
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
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
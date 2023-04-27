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
    public class reve : IHttpHandler
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
            try {
             
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
                string pid = mek[6];
                pid = pid.Substring(0, (pid.Length - 2));
                if (!pid.ToLower().Contains("x"))
                {
                    pid = mek[5];
                    pid = pid.Substring(0, (pid.Length - 2));


                    testn = mek[11];
                    labtestd = getlabtid("wbc", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[31];
                    labtestd = getlabtid("ba#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[21];
                    labtestd = getlabtid("ba", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[23];
                    labtestd = getlabtid("ne#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[13];
                    labtestd = getlabtid("ne", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[29];
                    labtestd = getlabtid("eo#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[19];
                    labtestd = getlabtid("eo", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[25];
                    labtestd = getlabtid("ly#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[15];
                    labtestd = getlabtid("ly", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[27];
                    labtestd = getlabtid("mo#", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[17];
                    labtestd = getlabtid("mo", pid);
                    setlabtest(testn, "", labtestd, pid);



                    testn = mek[33];
                    labtestd = getlabtid("rbc", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[35];
                    labtestd = getlabtid("hgb", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[39];
                    labtestd = getlabtid("mcv", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[41];
                    labtestd = getlabtid("mch", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[43];
                    labtestd = getlabtid("mchc", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[45];
                    labtestd = getlabtid("rdw-cv", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[55];
                    labtestd = getlabtid("rdw-sd", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[37];
                    labtestd = getlabtid("hct", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[47];
                    labtestd = getlabtid("plt", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[51];
                    labtestd = getlabtid("mpv", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[53];
                    labtestd = getlabtid("pdw", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[49];
                    labtestd = getlabtid("pct", pid);
                    setlabtest(testn, "", labtestd, pid);


                }
                else
                {
                    testn = mek[12];
                    labtestd = getlabtid("wbc", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[32];
                    labtestd = getlabtid("ba#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[22];
                    labtestd = getlabtid("ba", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[24];
                    labtestd = getlabtid("ne#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[14];
                    labtestd = getlabtid("ne", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[30];
                    labtestd = getlabtid("eo#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[20];
                    labtestd = getlabtid("eo", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[26];
                    labtestd = getlabtid("ly#", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[16];
                    labtestd = getlabtid("ly", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[28];
                    labtestd = getlabtid("mo#", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[18];
                    labtestd = getlabtid("mo", pid);
                    setlabtest(testn, "", labtestd, pid);



                    testn = mek[34];
                    labtestd = getlabtid("rbc", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[36];
                    labtestd = getlabtid("hgb", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[40];
                    labtestd = getlabtid("mcv", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[42];
                    labtestd = getlabtid("mch", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[44];
                    labtestd = getlabtid("mchc", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[46];
                    labtestd = getlabtid("rdw-cv", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[56];
                    labtestd = getlabtid("rdw-sd", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[38];
                    labtestd = getlabtid("hct", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[48];
                    labtestd = getlabtid("plt", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[52];
                    labtestd = getlabtid("mpv", pid);
                    setlabtest(testn, "", labtestd, pid);

                    testn = mek[54];
                    labtestd = getlabtid("pdw", pid);
                    setlabtest(testn, "", labtestd, pid);


                    testn = mek[50];
                    labtestd = getlabtid("pct", pid);
                    setlabtest(testn, "", labtestd, pid);
                }

               


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
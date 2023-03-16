using DCElectricWebAPI.Modules;
using Intuit.QuickBase.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DCElectricWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {

        // appJL = getQBApp(2)
        [HttpGet]

        public async Task<IActionResult> Get() 
        {
          
            //Variables
            string strTableId = "_DBID_JOBS";
            int intDtSize = 0;
            int intCtr = 0;
          //  DataTable dtJobs = dsJobs.Tables["Jobs_List"];

            var qbc = new QuickBaseConnector();
            var appJL = qbc.getQBApp(2);
            Dictionary<string, Intuit.QuickBase.Client.IQTable> dictTables = appJL.GetTables();
            intDtSize = dictTables.Count;
            for (intCtr = 0; intCtr < intDtSize; intCtr++)
            {
                switch (dictTables.ElementAt(intCtr).Value.ToString())
                {
                    case "Jobs":
                        strTableId = dictTables.ElementAt(intCtr).Key.ToString();
                        break;
                    default:
                        break;
                }//end switch
            }//end for

            // gets the table definition
            var vJobsTable = appJL.GetTable(strTableId);

            // query for jobs to purge
            Query qJobs = new Query();
            string jobNumber = "C2637";
            QueryStrings getJob = new QueryStrings(7, ComparisonOperator.CT, jobNumber, LogicalOperator.NONE);  //3 is "record ID#"  7 is jobnumber
            qJobs.Add(getJob);
            vJobsTable.Query(qJobs);
            
            return Ok(vJobsTable);
        }
    }
}

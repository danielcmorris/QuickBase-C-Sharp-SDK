using Intuit.QuickBase.Client;

namespace DCElectricWebAPI.Modules
{
    public class QuickBaseConnector
    {

        //class-wide variables
        static Intuit.QuickBase.Client.IQApplication appSL = null;
        static Intuit.QuickBase.Client.IQApplication appTS = null;
        static Intuit.QuickBase.Client.IQApplication appJL = null;
        static Intuit.QuickBase.Client.IQApplication appSafe = null;

        public  Intuit.QuickBase.Client.IQApplication getQBApp(int intApp) //Logs into Quickbase and gets app
        {
            //variables
            string strDomain = "dcelectricgroup.quickbase.com";
            string strToken = "ddma8nfc6zqtnmda3vq5cdgg94cs"; //Assigned in db d9mkaembc5sr6abti4p87c8zxwgp
            string strJlToken = "dxgqxwpgc238cvbpz9cbzda83x"; //assigned in Quickbase here: https://dcelectricgroup.quickbase.com/db/bkykszyj4?a=GetAppDevKey
            string strSafeToken = "d9mkaembc5sr6abti4p87c8zxwgp";




            string strApSlId = "";
            string strApTsId = "";
            string strApJlId = "";
            string strApSafeId = "";


            string strAccount = "jasch@dcelectricgroup.com";
            string strPW = "a$chC00!";
            //Log in and get app
            try
            {
                var client = Intuit.QuickBase.Client.QuickBase.Login(strAccount, strPW, strDomain);

                //strApSlId = "bm2k3zg85"; //test app id
                //strApTsId = "bm2k37pgt"; // test app id
                //strApJlId = "bm2isqezf"; // test app id
                //strApSafeId = "bm2k35gyj"; //test app id
                strApSlId = "bjrvqd33c"; //Sl app id
                strApTsId = "bhrneweey"; // TS app id
                strApJlId = "bkykszyj4"; //JL app id

                strApSafeId = "bk2wutv6x"; //Safety app id
                switch (intApp)
                {
                    case 0:

                        appSL = client.Connect(strApSlId, strToken);
                        return appSL;
                    case 1:
                        appTS = client.Connect(strApTsId, strToken);
                        return appTS;
                    case 2:

                        appJL = client.Connect(strApJlId, strJlToken);
                        return appJL;
                    case 3:
                        appSafe = client.Connect(strApSafeId, strSafeToken);
                        return appSafe;
                    default:
                        return null;
                }//end else
            }//end try
            catch (Exception ex)
            {
                writeLog(ex.Message.ToString() + " Message returned while processing GetQBApp. Check for errors in loading tables and columns.");
                return (null);
            }//end catch
        }//end getQBApp
        private static void writeLog(string msg) { }
    }

 
}

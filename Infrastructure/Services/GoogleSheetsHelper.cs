using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Reflection.Metadata;

namespace Infrastructure.Services
{
    public class GoogleSheetsHelper
    {
        public SheetsService Service { get; set; }
        //const string APPLICATION_NAME = "LeadBullSheet";
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        public GoogleSheetsHelper()
        {
            InitializeService();
        }
        private void InitializeService()
        {
            var credential = GetCredentialsFromFile();
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                //ApplicationName = APPLICATION_NAME
            });
        }
        private GoogleCredential GetCredentialsFromFile()
        {
            GoogleCredential credential;
            string fileName = "client_secrets.json";

            string path = "../Infrastructure/sheetCredentials/client_secrets.json";
            //var credentialKeyPath = AppDomain.CurrentDomain.BaseDirectory; 
            //using (var stream = new FileStream("C:\\Users\\hp\\source\\repos\\Api\\Infrastructure\\sheetCredentials\\client_secrets.json", FileMode.Open, FileAccess.Read))
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            return credential;
        }
        
    }
}

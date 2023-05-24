using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ValkoApp.Pages
{
    public class IndexModel : PageModel
    {
        public async Task OnGetAsync()
        {
            string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            HttpClient client = new();
            string response = await client.GetStringAsync(url);
            var dataList = JsonConvert.DeserializeObject<List<EmployeeModel>>(response);
            List<EmployeeWorkModel> EmployeeWorkList = new List<EmployeeWorkModel>();

            if (dataList != null)
            {
                int x = 0;
                foreach (var data in dataList)
                {
                    string id = data.Id;
                    string employeeName = data.EmployeeName;
                    DateTime StarTimeUtc = data.StarTimeUtc;
                    DateTime endTimeUtc = data.EndTimeUtc;
                    string entryNotes = data.EntryNotes;
                    string DeletedOn = data.DeletedOn;
                 
                   EmployeeWorkList.InsertRange(x, new List<EmployeeWorkModel>
                   {
                       new EmployeeWorkModel { EmployeeName = data.EmployeeName, WorkTime = endTimeUtc - StarTimeUtc }
                   });                                        

                   List<EmployeeWorkModel> groupedEmployees = EmployeeWorkList.GroupBy(e => e.EmployeeName)
                        .Select(e => new EmployeeWorkModel
                        {
                            EmployeeName = e.Key,
                            WorkTime = TimeSpan.FromMinutes(e.Sum(ee => ee.WorkTime.TotalMinutes))  
                        })
                        .ToList();                 

                    ViewData["employeeWork"] = groupedEmployees;               

                    x += +1;
                }              
            }
        }     
    }     
}
using Kaede_Executor_API.Models.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kaede_Executor_API.Controllers
{
    [ApiController]
    [Route("/GetKey")]
    public class KeySystemController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetReturn(string Hwid = "")
        {
            bool Found = false;

            var JsonResponse = new DataBase
            {
                Key = null,
                HWID = null
            };

            var DataBaseL = Directory.GetCurrentDirectory() + "\\DataBase";

            if (!Directory.Exists(DataBaseL))
                Directory.CreateDirectory(DataBaseL);

            DirectoryInfo d = new DirectoryInfo(DataBaseL);
            FileInfo[] Files = d.GetFiles("*.KE");

            foreach (FileInfo file in Files)
            {
                if (file.Name == $"{Hwid}.KE")
                {
                    Found = true;
                    DataBase FilesResponse = JsonConvert.DeserializeObject<DataBase>(System.IO.File.ReadAllText(file.FullName));
                    JsonResponse.Key = FilesResponse.Key;
                    JsonResponse.HWID = FilesResponse.HWID;
                }
            }

            if (!Found)
            {
                JsonResponse.Key = Helpers.KeySystemHelper.RandomString(16);
                JsonResponse.HWID = Hwid;
                System.IO.File.WriteAllText(DataBaseL + $"\\{JsonResponse.HWID}.KE", JsonConvert.SerializeObject(JsonResponse));
            }

            Found = false;

            var result = JsonConvert.SerializeObject(JsonResponse);

            return Content(result, "application/json");
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> GetAllReturn(string AdminKey = "")
        {
            if (AdminKey == "JtxVs3g81vS5KAt5")
            {
                var JsonResponse = new EveryData
                {
                    AdminAccess = true,
                    AdminAccessKey = AdminKey,
                    Keys = new List<DataBase>()
                };

                var DataBaseL = Directory.GetCurrentDirectory() + "\\DataBase";

                if (!Directory.Exists(DataBaseL))
                    Directory.CreateDirectory(DataBaseL);

                DirectoryInfo d = new DirectoryInfo(DataBaseL);
                FileInfo[] Files = d.GetFiles("*.KE");

                foreach (FileInfo file in Files)
                {
                    DataBase FilesResponse = JsonConvert.DeserializeObject<DataBase>(System.IO.File.ReadAllText(file.FullName));
                    JsonResponse.Keys.Add(new DataBase
                    {
                        Key = FilesResponse.Key,
                        HWID = FilesResponse.HWID,
                    });
                }

                var result = JsonConvert.SerializeObject(JsonResponse);

                return Content(result, "application/json");
            }
            else
            {
                var JsonResponse = new EveryData
                {
                    AdminAccess = false,
                    AdminAccessKey = AdminKey,
                    Keys = new List<DataBase>()
                };

                var result = JsonConvert.SerializeObject(JsonResponse);

                return Content(result, "application/json");
            }
        }
    }
}

using ImportTrigger.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportTrigger.Helpers
{
    public static class CreateBatch
    {
        public static void CreateBatchImportModel(string currentDirectory, string batchID)
        {

            string jsondata = string.Empty;
            try
            {
                string ref_num1 = Guid.NewGuid().ToString();
                string[] phoneNumbers = { "+353161123345", "+919654627686" };
                string dob = new DateTime(1991, 08, 28).ToString("yyyy-MM-ddTHH:mmZ");
                BatchImportModel batchImportModelUser1 = new BatchImportModel()
                {
                    ReferenceId = ref_num1,
                    Schema = "guest",
                    Mode = "upsert",

                    Guest = new GuestModel()
                    {
                        GuestType = "customer",
                        FirstName = "testfirstname1",
                        LastName = "testsecondname1",
                        Email = "testfirstname1@test.com",
                        Nationality = "Indian",
                        Gender = "male",
                        PhoneNumbers = phoneNumbers,
                        DateofBirth = dob,
                        Extensions = new List<Extension>()
                    {
                       new Extension()
                       {
                          Name = "ext",
                          Key = "default",
                          LoyaltyTier = "level38",
                          SaudiIdNumber = "1987865656",
                          ResidencyStatus = "KSA Resident",
                          Campus = "BMC Jeddah"
                       }
                    },
                        Identifiers = new List<Identifier>()
                    {
                       new Identifier()
                       {
                          Provider = "email",
                          Id = "testfirstname1@test.com"
                       }
                    }
                    }
                };
                jsondata += JsonConvert.SerializeObject(batchImportModelUser1) + Environment.NewLine;
                string ref_num2 = Guid.NewGuid().ToString();
                BatchImportModel batchImportModelUser2 = new BatchImportModel()
                {
                    ReferenceId = ref_num2,
                    Schema = "guest",
                    Mode = "upsert",
                    Guest = new GuestModel()
                    {
                        GuestType = "customer",
                        FirstName = "testfirstname2",
                        LastName = "testsecondname2",
                        Email = "testfirstname2@test.com",
                        Nationality = "Indian",
                        Gender = "male",
                        DateofBirth = dob,
                        PhoneNumbers = phoneNumbers,
                        Extensions = new List<Extension>()
                    {
                       new Extension()
                       {
                          Name = "ext",
                          Key = "default",
                          LoyaltyTier = "level45",
                          SaudiIdNumber = "1987865655",
                          ResidencyStatus = "KSA Resident",
                          Campus = "BMC Jeddah",
                       }
                    },
                        Identifiers = new List<Identifier>()
                    {
                       new Identifier()
                       {
                          Provider = "email",
                          Id = "testfirstname2@test.com",
                       }
                    }
                    }
                };
                jsondata += JsonConvert.SerializeObject(batchImportModelUser2);
                byte[] dataToCompress = Encoding.UTF8.GetBytes(jsondata);
                BatchImportHelper.GZipFile(currentDirectory, dataToCompress, batchID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

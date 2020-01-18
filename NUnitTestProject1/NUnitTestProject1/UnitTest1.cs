using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

             [Test]
        public void Test1()
        {
            using (var conn = new SqlConnection())
            {
                string userName;
                conn.ConnectionString = @"Data Source=DESKTOP-19GTHEH\SQLEXPRESS; Initial Catalog=ShootServ; Integrated Security=true;";
                conn.Open();
                using (var command = new SqlCommand(@"SELECT TOP (1000) [Id]
                      ,[Name]
                      ,[Family]
                      ,[FatherName]
                      ,[Login]
                      ,[Password]
                      ,[IdRole]
                      ,[DateCreate]
                      ,[E_mail]
                  FROM [ShootServ].[dbo].[Users]", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userName = reader["Name"].ToString();
                    }
                }

            }


            Assert.Pass();
        }
    }
}
    }
}

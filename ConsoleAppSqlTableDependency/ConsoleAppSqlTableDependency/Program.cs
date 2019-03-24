using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TableDependency;
using TableDependency.SqlClient;
using TableDependency.EventArgs;
using System.Net.Http;

using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ConsoleAppSqlTableDependency.EF;
using ConsoleAppSqlTableDependency.Models;
using System.Globalization;
using AutoMapper;
using KellermanSoftware.CompareNetObjects;

namespace ConsoleAppSqlTableDependency
{
    class Program
    {
        //To run broker on database for SqlDependancy
        //ALTER DATABASE SignalRDemo SET ENABLE_BROKER with rollback immediate
        //ALTER AUTHORIZATION ON DATABASE::secret db TO sa

        //https://medium.com/@rukshandangalla/how-to-notify-your-angular-5-app-using-signalr-5e5aea2030b2
        static HttpClient client = new HttpClient();
        static MapperConfiguration config = new MapperConfiguration(cfg => {            
            cfg.CreateMap<PlayerDetail, PlayerDTO>();
        });

        //private static string _con = @"data source=YourServerName; initial catalog=YourDatebase;  integrated security=True";
        //private static string _con = @"data source=YourServerName Or IP; Network Library=DBMSSOCN; initial catalog=YourDatebase;persist security info=True;user id=YourUser;password=YourPassword;";
        private static string _con = @"data source=YourIP;initial catalog=YourDB;persist security info=True;user id=YourUser;password=YourPassword;";

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:51113/api/Message/Posts");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            var mapper = new ModelToTableMapper<PlayerDetail>();
            mapper.AddMapping(c => c.PlayerId, "PlayerId");
            mapper.AddMapping(c => c.FacePhotoUrl, "FacePhotoUrl");

            // Just call the API from here with an object PlayerDetail


            // Here - as second parameter - we pass table name: this is necessary only if the model name is 
            // different from table name (in our case we have Customer vs Customers). 
            // If needed, you can also specifiy schema name.
            SqlTableDependency<PlayerDetail> depPlayer;
            SqlTableDependency<JournalItem> depJournal;
            SqlTableDependency<DummyNotification> depNotification;

            depPlayer = new SqlTableDependency<PlayerDetail>(_con, tableName: "PlayerDetail", mapper: mapper, includeOldValues: true);
            depPlayer.OnChanged += ChangedPlayer;
            depPlayer.Start();

            depJournal = new SqlTableDependency<JournalItem>(_con, tableName: "JournalItem");
            depJournal.OnChanged += ChangedJournal;
            depJournal.Start();

            depNotification = new SqlTableDependency<DummyNotification>(_con, tableName: "DummyNotification");
            depNotification.OnChanged += ChangedNotification;
            depNotification.Start();

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
            depPlayer.Stop();
            depPlayer.Dispose();
            depJournal.Stop();
            depJournal.Dispose();            
            depNotification.Stop();
            depNotification.Dispose();
        }

        private static void ChangedPlayer(object sender, RecordChangedEventArgs<PlayerDetail> e)
        {

            e.EntityOldValues.LastUpdatedDate = e.Entity.LastUpdatedDate;
            IMapper iMapper = config.CreateMapper();

            PlayerDTO oldDto = iMapper.Map<PlayerDetail, PlayerDTO>(e.EntityOldValues);
            PlayerDTO newDto = iMapper.Map<PlayerDetail, PlayerDTO>(e.Entity);

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult comparisonResult = compareLogic.Compare(oldDto,newDto);

            if (!comparisonResult.AreEqual)
            {
                RunAsync(e.Entity).GetAwaiter().GetResult();
            }
        }

        private static void ChangedJournal (object sender, RecordChangedEventArgs<JournalItem> e)
        {
            RunAsync(e.Entity).GetAwaiter().GetResult();
        }

        private static void ChangedNotification(object sender, RecordChangedEventArgs<DummyNotification> e)
        {
            RunAsync(e.Entity).GetAwaiter().GetResult();
        }


        static async Task<Uri> PostsAsync(List<PlayerJournalDTO> PlayerJournalDTOs)
        {
            string stringData = JsonConvert.SerializeObject(PlayerJournalDTOs);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                "http://localhost:51113/Message/Posts", contentData);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task RunAsync(object e)
        {
            PlayerDetail playerDetail = new PlayerDetail();
            JournalItem journalItem = new JournalItem();
            DummyNotification notificationItem = new DummyNotification();

            try
            {
                using (var context = new DummyEntities())
                {
                    if (e is PlayerDetail)
                    {
                        playerDetail = (PlayerDetail)e;
                        journalItem = null;
                        notificationItem = null;
                    }
                    else if (e is JournalItem)
                    {
                        journalItem = (JournalItem)e;
                        playerDetail = null; 
                        notificationItem = null;

                    }
                    else if (e is DummyNotification)
                    {
                        notificationItem = (DummyNotification)e;
                        playerDetail = null; 
                        journalItem = null; 
                    }

                    // TODO: Add UserEventNotification changes here as well. On insert we will call the CheckforOTWNotifications under D:\Qasir Data\Dummy\DummyAPI\Dummy.NotificationServices\Helpers\DummyNotificationProcessor.cs
                    // TODO: Add MatchStateNotification changes here as well. On insert we will call the CheckforMatchStateNotifications under D:\Qasir Data\Dummy\DummyAPI\Dummy.NotificationServices\Helpers\DummyNotificationProcessor.cs

                    List<PlayerJournalDTO> PlayerJournalDTOList = new List<PlayerJournalDTO>();
                    PlayerJournalDTOList.Add(new PlayerJournalDTO() { PlayerDetail = playerDetail, JournalItem = journalItem, DummyNotification = notificationItem});

                    var url = await PostsAsync(PlayerJournalDTOList);


                    Console.WriteLine($"Created at {url}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

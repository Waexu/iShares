namespace ETF_API.Logic.ETF
{
    public partial class Download
    {
        private static readonly HttpClient Client = new()
        {
            Timeout = TimeSpan.FromSeconds(Global.Settings.iShares.RequestTimeoutSeconds)
        };

        public static async Task<List<Models.Download.DownloadItem>> Execute(string ETF_Name)
        {
            try
            {
                Console.WriteLine($"---START {ETF_Name}");
                var DataStorage = new Storage(ETF_Name);
                await DataStorage.InitFolders();

                var Items = new List<Models.Download.DownloadItem>();
                var Year = (short)Global.Settings.iShares.FirstAvailableHoldingsDate.Year;
                var Month = (byte)Global.Settings.iShares.FirstAvailableHoldingsDate.Month;

                var Now = DateTime.Now;
                do
                {
                    var ETFItem = new Models.ETF.Item(ETF_Name, Year, Month);
                    if (ETFItem.AsOfDate > Now)
                    {
                        break;
                    }

                    var Item = new Models.Download.DownloadItem(ETFItem)
                    {
                        Status = "Timeout" // If for some reason item would not be processes it's probably timeout
                    };

                    Items.Add(Item);
                    Month++;
                    if (Month > 12)
                    {
                        Month = 1;
                        Year++;
                    }
                }
                while (true);

                var Locker = new object();
                var ThreadsCount = Global.Settings.iShares.Threads;
                var ThreadsPool = new List<Task>();

                void Cleanup()
                {
                    var Completed = ThreadsPool.Where(Q => Q.IsCompleted).ToList();
                    _ = ThreadsPool.RemoveAll(Q => Completed.Contains(Q));
                }

                foreach (var Item in Items)
                {
                    var ItemTask = Task.Run(async () =>
                    {
                        var ItemLog = new List<string>();
                        try
                        {
                            ItemLog.Add(string.Empty);
                            ItemLog.Add($"{DateTime.Now}: Starting {ETF_Name} as of {Item.ETF.AsOfDate.ToString(Global.DateFormat)}");
                            var OriginalCsvPath = DataStorage.GetOriginalCsvPath(Item.ETF.AsOfDate);
                            var OriginalJsonPath = DataStorage.GetOriginalJsonPath(Item.ETF.AsOfDate);
                            var MergedCsvPath = DataStorage.GetMergedCsvPath(Item.ETF.AsOfDate);

                            for (byte Attempt = 1; Attempt <= Global.Settings.iShares.MaxAttempts; Attempt++)
                            {
                                try
                                {
                                    if (Attempt > 1)
                                    {
                                        ItemLog.Add($"{DateTime.Now}: Starting attempt {Attempt}");
                                    }

                                    Item.IsNew = false;
                                    Item.Status = null;
                                    Item.Success = false;

                                    if (await (DataStorage.IsFileExists(MergedCsvPath)))
                                    {
                                        Item.Success = true;
                                        Item.Status = $"Already exists: {MergedCsvPath}";
                                        ItemLog.Add($"{DateTime.Now}: {Item.Status}");
                                        return;
                                    }

                                    var CsvTask = Client.GetAsync(Item.ETF.CsvUrl);
                                    var JsonTask = Client.GetAsync(Item.ETF.JsonUrl);
                                    var Csv = await CsvTask;
                                    var Json = await JsonTask;

                                    ItemLog.Add($"{DateTime.Now}: {Item.ETF.CsvUrl} | STATUS CODE {(int)Csv.StatusCode} ({Csv.StatusCode})");
                                    ItemLog.Add($"{DateTime.Now}: {Item.ETF.JsonUrl} | STATUS CODE {(int)Json.StatusCode} ({Json.StatusCode})");

                                    var CsvContent = await Csv.Content.ReadAsStringAsync();
                                    var JsonContent = await Json.Content.ReadAsStringAsync();

                                    var MergedLines = Logic.ETF.Csv.Original.MergeWithJson(CsvContent, JsonContent, Item.ETF.AsOfDate);
                                    var MergedCsvContent = Logic.ETF.Csv.Merged.ToString(MergedLines);

                                    // First we save merged csv because in case of error originals shouldn't be saved
                                    await DataStorage.WriteAllText(MergedCsvPath, MergedCsvContent);
                                    ItemLog.Add($"{DateTime.Now}: Csv merged and saved to {DataStorage.StorageType} Storage: {MergedCsvPath}");

                                    Item.Success = true;
                                    Item.IsNew = true;
                                    Item.Status = $"Saved to {MergedCsvPath}";

                                    try
                                    {
                                        await DataStorage.WriteAllText(OriginalCsvPath, CsvContent);
                                    }
                                    catch (Exception E)
                                    {
                                        // Not critical error but still better to report
                                        ItemLog.Add($"{DateTime.Now}: {E.GetFullMessage()}");
                                    }

                                    try
                                    {
                                        await DataStorage.WriteAllText(OriginalJsonPath, JsonContent);
                                    }
                                    catch (Exception E)
                                    {
                                        // Not critical error but still better to report
                                        ItemLog.Add($"{DateTime.Now}: {E.GetFullMessage()}");
                                    }

                                    break;
                                }
                                catch (Exception E)
                                {
                                    Item.Status = E.Message;
                                    ItemLog.Add($"{DateTime.Now}: {E.GetFullMessage()}");
                                }
                                finally
                                {
                                    if (Item.IsNew || !Item.Success) // We need delay only if HTTP request was actually made
                                    {
                                        var DelayMilliseconds = Helpers.GetRandomSleepTimeMilliseconds();
                                        if (DelayMilliseconds > 0)
                                        {
                                            ItemLog.Add($"{DateTime.Now}: Waiting {DelayMilliseconds} milliseconds...");
                                            await Task.Delay(DelayMilliseconds);
                                        }
                                    }
                                }
                            }
                        }
                        finally
                        {
                            ItemLog.Add($"{DateTime.Now}: Item processed.");
                            ItemLog.Add(string.Empty);
                            Console.WriteLine(string.Join(Environment.NewLine, ItemLog));
                        }

                    });

                    ThreadsPool.Add(ItemTask);



                    Cleanup();
                    while (ThreadsPool.Count >= ThreadsCount)
                    {
                        await Task.Delay(50);
                        Cleanup();
                    }
                }

                while (ThreadsPool.Count > 0)
                {
                    await Task.Delay(50);
                    Cleanup();
                }

                return Items;
            } 
            finally
            {
                Console.WriteLine($"---END {ETF_Name}");
            }
        }
    }
}

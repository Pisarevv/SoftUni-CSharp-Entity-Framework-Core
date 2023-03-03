namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            var result = ExportAlbumsInfo(context, 9);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var sb = new StringBuilder();

            var albums = context.Albums
                         .Where(a => a.ProducerId.HasValue && a.ProducerId == producerId)
                         .Select(a => new
                         {
                             a.Name,
                             a.ReleaseDate,
                             ProducerName = a.Producer.Name,
                             Songs = a.Songs
                             .Select(s =>
                             new
                             {
                                 SongName = s.Name,
                                 s.Price,
                                 SongWriter = s.Writer.Name
                             })
                             .OrderByDescending(s => s.SongName)
                             .ThenBy(s => s.SongWriter)
                             .ToArray(),
                             AlbumPrice = a.Price
                         })
                         .ToArray()
                         .OrderByDescending(a => a.AlbumPrice);
                   
    

            foreach (var a in albums)
            {
                sb.AppendLine($"-AlbumName: {a.Name}");           
                sb.AppendLine($"-ReleaseDate: {a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}");
                sb.AppendLine($"-ProducerName: {a.ProducerName}");
                sb.AppendLine($"-Songs:");
                int songNumber = 1;
                foreach (var s in a.Songs)
                {
                    sb.AppendLine($"---#{songNumber}");
                    sb.AppendLine($"---SongName: {s.SongName}");
                    sb.AppendLine($"---Price: {s.Price:F2}");
                    sb.AppendLine($"---Writer: {s.SongWriter}");
                    songNumber++;
                }

                sb.AppendLine($"-AlbumPrice: {a.AlbumPrice:F2}");
            }

            return sb.ToString().TrimEnd();


            //Console.WriteLine(
            //    context.Albums
            //             .AsNoTracking()
            //             .Where(a => a.ProducerId == producerId)                      
            //             .Select(a => new
            //             {
            //                 a.Name,
            //                 a.ReleaseDate,
            //                 ProducerName = a.Producer.Name,
            //                 Songs = a.Songs.Select(s =>
            //                 new
            //                 {
            //                     SongName = s.Name,
            //                     s.Price,
            //                     SongWriter = s.Writer
            //                 })
            //                 .OrderByDescending(s => s.SongName)
            //                 .OrderBy(s => s.SongWriter)
            //                 .ToList()

            //             })
            //             .ToQueryString());


        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }

    }
}

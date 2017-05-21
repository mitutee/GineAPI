using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using vk_dotnet;
using vk_dotnet.Objects;

namespace GetMyPhotos_
{
    class Program
    {
        static string token = "23786e7e58fdaa2596eb15d317d5495f5b726731d4bc08b0c03877e7e840eb23540126d27f9bd2dc5537c";

        static BotClient _bot = new BotClient(token);

        static void Main(string[] args)
        {
            int _offset = 0; // it is 0 because we start from the begining
            List<Photo> photos = _bot.Photos.GetAll(OwnerId : "80314023", count : 200);
            while (photos.Count > 0)
            {
                foreach(Photo ph in photos)
                {   
                    // --- pick up photo with the best quaility ---
                    var max_size = ph.sizes[0];
                    foreach (var size in ph.sizes)
                    {
                        if(size.height * size.width > max_size.height * max_size.width)
                        {
                            max_size = size;
                        }
                    }
                    // ---   ---
                    saveFile(max_size.src, ph.id.ToString() + ".jpg"); 
                }

                photos = _bot.Photos.GetAll(offset : _offset += 200, count : 200); // load new photos and shift the offset
            }
        }
        
        /// <summary>
        /// Saves the file from the specified URI
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="fileName"></param>
        static void saveFile(string uri, string fileName)
        {
            using (HttpClient cl = new HttpClient())
            {
                var stream = cl.GetStreamAsync(uri).Result;
                FileStream fileStream = File.Create(fileName);
                // Initialize the bytes array with the stream length and then fill it with data
                stream.CopyTo(fileStream);
                // Use write method to write to the file specified above

                fileStream.Dispose();
                stream.Dispose();
            }
        }
    }
}
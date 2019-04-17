using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FortniteMapTracker.Core
{
    public class TempFile : IDisposable
    {
        private string path;

        public TempFile(string extension)
        {
            path = $"{this.Path}{Guid.NewGuid()}.{extension}";
        }

        public void WriteToFile(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var fileStream = new FileStream(this.Path, FileMode.Create))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
        }

        public string Path
        {
            get
            {
                if (path == null)
                {
                    path = System.IO.Path.GetTempPath();
                }
                return path;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            if (path != null)
            {
                try
                {
                    File.Delete(path);
                }
                finally
                {
                    path = null;
                }
            }
        }
    }
}

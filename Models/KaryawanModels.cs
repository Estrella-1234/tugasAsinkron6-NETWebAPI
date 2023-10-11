using MongoDB.Bson;

namespace BackendNET.Models
{
    public class KaryawanModels
    {
        public ObjectId _id { get; set; }
        public int NIK { get; set; }
        public string? Nama { get; set; }
        public string? PendidikanTerakhir { get; set; }
        public string? Jabatan { get; set; }
    }
}

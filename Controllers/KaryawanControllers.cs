using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using BackendNET.Models; // Ganti dengan namespace yang sesuai dengan model Karyawan Anda

namespace BackendNET.Controllers
{
    [Route("api/karyawan")]
    [ApiController]
    public class KaryawanControllers : ControllerBase
    {
        private readonly IMongoCollection<KaryawanModels> _karyawanCollection;

        public KaryawanControllers(IMongoClient mongoClient)
        {
            // Ganti "NamaDatabase" dengan nama database MongoDB Anda
            var database = mongoClient.GetDatabase("Karyawan");
            _karyawanCollection = database.GetCollection<KaryawanModels>("Karyawan");
        }

        [HttpGet]
        public ActionResult<IEnumerable<KaryawanModels>> Get()
        {
            var karyawan = _karyawanCollection.Find(k => true).ToList();
            return Ok(karyawan);
        }

        [HttpGet("{nik}")]
        public ActionResult<KaryawanModels> Get(int nik)
        {
            var karyawan = _karyawanCollection.Find(k => k.NIK == nik).FirstOrDefault();
            if (karyawan == null)
            {
                return NotFound();
            }
            return Ok(karyawan);
        }

        [HttpPost]
        public ActionResult<KaryawanModels> Post([FromBody] KaryawanModels karyawan)
        {
            // Validasi karyawan
            if (karyawan == null)
            {
                return BadRequest();
            }

            // Simpan karyawan baru ke MongoDB
            _karyawanCollection.InsertOne(karyawan);

            // Return karyawan yang telah ditambahkan
            return CreatedAtAction(nameof(Get), new { nik = karyawan.NIK }, karyawan);
        }

        [HttpDelete("{nik}")]
        public ActionResult Delete(int nik)
        {
            var deleteResult = _karyawanCollection.DeleteOne(k => k.NIK == nik);
            if (deleteResult.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

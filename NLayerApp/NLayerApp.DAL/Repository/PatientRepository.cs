using System.Linq;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace NLayerApp.DAL.Repository
{
    public class PatientRepository:IPatientRepository
    {
        private readonly DataContext _context;
        private readonly IDistributedCache _cache;

        public PatientRepository(DataContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public ICollection<Patients> GetPatients()
        {
            var patientsList = new List<Patients>();
            var cached = _cache.Get("patientsList");
            if (cached != null)
            {
                var encoded = Encoding.UTF8.GetString(cached);
                patientsList=JsonConvert.DeserializeObject<List<Patients>>(encoded);
            }
            else {
                patientsList = _context.Patients.OrderBy(p => p.Id).Include(p => p.Doctor).ToList();
                var serializedPatients = JsonConvert.SerializeObject(patientsList,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }) ;
                var encoded = Encoding.UTF8.GetBytes(serializedPatients);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                _cache.Set("patientsList", encoded, options);
            }
            return patientsList;

        }
        public Patients GetPatientById(int id)
        {
            var patient = new Patients();
            var cached = _cache.Get("patient");
            if(cached != null)
            {
                var encoded = Encoding.UTF8.GetString(cached);
                patient = JsonConvert.DeserializeObject<Patients>(encoded);
            }
            else
            {
                patient= _context.Patients.Include(p => p.Doctor).Where(p => p.Id == id).FirstOrDefault();
                var serializedPatient = JsonConvert.SerializeObject(patient,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                var encoded = Encoding.UTF8.GetBytes(serializedPatient);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                _cache.Set("patient", encoded, options);
            }
            return patient;
        }
        public bool PatientExist(int id)
        {
            return _context.Patients.Any(p => p.Id == id);
        }
        public ICollection<Patients> GetPatientsBySurname(string name)
        {
            var patientSurname = new List<Patients>();
            var cached = _cache.Get("patientSurname");
            if (cached != null)
            {
                var encoded = Encoding.UTF8.GetString(cached);
                patientSurname = JsonConvert.DeserializeObject<List<Patients>>(encoded);
            }
            else
            {
                patientSurname = _context.Patients.Include(p => p.Doctor).Where(p => p.Surname == name).ToList();
                var serializedPatient = JsonConvert.SerializeObject(patientSurname,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                var encoded = Encoding.UTF8.GetBytes(serializedPatient);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                _cache.Set("patientSurname", encoded, options);
            }
            return patientSurname;
        }
        public ICollection<Patients> GetPatientsByDiagnosis(string diagnosis)
        {
            var patientDiagnosis = new List<Patients>();
            var cached = _cache.Get("patientDiagnosis");
            if (cached != null)
            {
                var encoded = Encoding.UTF8.GetString(cached);
                patientDiagnosis = JsonConvert.DeserializeObject<List<Patients>>(encoded);
            }
            else
            {
                patientDiagnosis = _context.Patients.Include(p => p.Doctor).Where(p => p.Diagnosis == diagnosis).ToList();
                var serializedPatient = JsonConvert.SerializeObject(patientDiagnosis,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                var encoded = Encoding.UTF8.GetBytes(serializedPatient);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                _cache.Set("patientDiagnosis", encoded, options);
            }
            return patientDiagnosis;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true:false;
        }
        public bool Create(Patients patient)
        {
            _context.Add(patient);
            return Save();
        }
        public bool Update(Patients patient)
        {
            _context.Update(patient);
            return Save();
        }
        public bool Delete(Patients patient)
        {
            _context.Remove(patient);
            return Save();
        }
    }
}

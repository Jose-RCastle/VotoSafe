using Microsoft.AspNetCore.Mvc;
using VotoSafe.Services;

namespace VotoSafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;

        public TestController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpGet("firebase")]
        public async Task<IActionResult> TestFirebase()
        {
            try
            {
                var db = _firebaseService.GetFirestoreDb();
                var usersCollection = _firebaseService.GetCollection("users");
                var snapshot = await usersCollection.Limit(1).GetSnapshotAsync();

                return Ok(new
                {
                    success = true,
                    message = "Conexión exitosa a VotoSafe",
                    projectId = db.ProjectId,
                    documentsFound = snapshot.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al conectar con VotoSafe Firebase",
                    error = ex.Message
                });
            }
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "VotoSafe API funcionando correctamente",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
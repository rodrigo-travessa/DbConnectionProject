using DbConnectionProject.Models.Models;
using DbConnectionProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DbConnectionProject.Controllers
{
    public class CarroController : Controller
    {

        private readonly IRepository<Carro> _carroRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        public CarroController(IRepository<Carro> CarroRepo, IRepository<Pessoa> pessoaRepository)
        {
            _carroRepository = CarroRepo;
            _pessoaRepository = pessoaRepository;

        }
        [BindProperty]
        Carro carro { get; set; } = new Carro();

        // GET: CarroController
        public ActionResult Index()
        {
            IEnumerable<Carro> carros = _carroRepository.ReadAll().ToList();
            IEnumerable<Pessoa> pessoas = _pessoaRepository.ReadAll().ToList();
            return View((carros,pessoas));
        }

        // GET: CarroController/Details/5
        public ActionResult Details(int id)
        {
            Carro carro = _carroRepository.Read(id);
            return View(carro);
        }

        // GET: CarroController/Create
        public ActionResult Create()
        {
            List<Pessoa> pessoas = _pessoaRepository.ReadAll();
            CarroCreateViewModel carroCreateViewModel = new CarroCreateViewModel()
            {
                Pessoas = pessoas,
                Carro = carro
            };
            return View(carroCreateViewModel);
        }

        // POST: CarroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(IFormCollection collection)
        {
            Carro novoCarro = new Carro()
            {
                Marca = collection["Carro.Marca"],
                Modelo = collection["Carro.Modelo"],
                DonoId = Convert.ToInt32(collection["Carro.DonoId"])
            };
            try
            {
                _carroRepository.Create(novoCarro);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CarroController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Carro carro = _carroRepository.Read(id);
                List<Pessoa> pessoas = _pessoaRepository.ReadAll();
                CarroCreateViewModel carroCreateViewModel = new CarroCreateViewModel()
                {
                    Pessoas = pessoas,
                    Carro = carro
                };
                return View(carroCreateViewModel);
            }
            catch
            {
                return View();
            }
            

        }

        // POST: CarroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id, IFormCollection collection)
        {
            Carro novoCarro = new Carro()
            {
                Marca = collection["Carro.Marca"],
                Modelo = collection["Carro.Modelo"],
                DonoId = Convert.ToInt32(collection["Carro.DonoId"]),
                Id = Convert.ToInt32(collection["Carro.id"])
                
            };
            try
            {
                _carroRepository.Update(novoCarro);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CarroController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Carro carro = _carroRepository.Read(id);
                return View(carro);
            }
            catch
            {
                ViewData["Message"] = "Não foi possível deletar";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CarroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Carro carro = _carroRepository.Read(id);
                _carroRepository.Delete(carro);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

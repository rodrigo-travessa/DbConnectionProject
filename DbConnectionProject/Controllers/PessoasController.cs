using DbConnectionProject.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace DbConnectionProject.Controllers
{
    public class PessoasController : Controller
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        public PessoasController(IRepository<Pessoa> repo)
        {
            _pessoaRepository = repo;
        }

        [BindProperty]
        public Pessoa Pessoa { get; set; } = new();


        // GET: PessoasController
        public ActionResult Index()
        {
            try
            {
                List<Pessoa> pessoas = _pessoaRepository.ReadAll();
                return View(pessoas);
            }
            catch
            {
                return View();
            }
            
        }

        // GET: PessoasController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Pessoa pessoa = _pessoaRepository.Read(id);
                return View(pessoa);
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoasController/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: PessoasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost()
        {
            try
            {
                _pessoaRepository.Create(Pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoasController/Edit/5
        public ActionResult Edit(int id)
        {
            Pessoa pessoa = new();
            try
            {
                pessoa = _pessoaRepository.Read(id);
            }
            catch
            {
                return View();
            }
            return View(pessoa);
        }

        // POST: PessoasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            try
            {
                _pessoaRepository.Update(Pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoasController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Pessoa pessoa = _pessoaRepository.Read(id);
                return View(pessoa);
            }
            catch
            {
                ViewData["Message"] = "Não foi possível deletar";
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: PessoasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            try
            {
                _pessoaRepository.Delete(Pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ThermalFlowAnalyzer.Data;
using ThermalFlowAnalyzer.Domain;
using ThermalFlowAnalyzer.Logic;

namespace ThermalFlowAnalyzer.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly ThermalDbContext _db;
        private readonly ICounterflowSolver _solver;

        public AnalysisController(ThermalDbContext db, ICounterflowSolver solver)
        {
            _db = db;
            _solver = solver;
        }

        public IActionResult Dashboard()
        {
            return View(_db.Analyses.ToList());
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(AnalysisInput input)
        {
            if (input.LayerHeight * 2 % 1 != 0)
            {
                ModelState.AddModelError(
                    "LayerHeight",
                    "Высота слоя должна быть кратна 0.5"
                );
            }

            if (!ModelState.IsValid)
                return View(input);

            _db.Analyses.Add(input);
            _db.SaveChanges();

            var points = _solver.Solve(input);
            points.ForEach(p => p.AnalysisInputId = input.Id);

            _db.Points.AddRange(points);
            _db.SaveChanges();

            return RedirectToAction("Result", new { id = input.Id });
        }


        public IActionResult Result(int id)
        {
            return View(new AnalysisViewModel
            {
                Input = _db.Analyses.Find(id),
                Points = _db.Points.Where(p => p.AnalysisInputId == id).ToList()
            });
        }

        public IActionResult Recalculate(int id)
        {
            var input = _db.Analyses.Find(id);
            if (input == null) return NotFound();

            var oldPoints = _db.Points
                .Where(p => p.AnalysisInputId == id);
            _db.Points.RemoveRange(oldPoints);

            var newPoints = _solver.Solve(input);
            newPoints.ForEach(p => p.AnalysisInputId = id);

            _db.Points.AddRange(newPoints);
            _db.SaveChanges();

            return RedirectToAction("Result", new { id });
        }

        public IActionResult Edit(int id)
        {
            var input = _db.Analyses.Find(id);
            if (input == null) return NotFound();

            return View(input);
        }

        [HttpPost]
        public IActionResult Edit(AnalysisInput input)
        {
            if (input.LayerHeight * 2 % 1 != 0)
                ModelState.AddModelError(
        "LayerHeight",
        "Высота слоя должна быть целым числом или кратной 0,5"
    );

            if (!ModelState.IsValid)
                return View(input);

            _db.Analyses.Update(input);

            var oldPoints = _db.Points
                .Where(p => p.AnalysisInputId == input.Id);
            _db.Points.RemoveRange(oldPoints);

            var points = _solver.Solve(input);
            points.ForEach(p => p.AnalysisInputId = input.Id);

            _db.Points.AddRange(points);
            _db.SaveChanges();

            return RedirectToAction("Result", new { id = input.Id });
        }

        public IActionResult Delete(int id)
        {
            var input = _db.Analyses.Find(id);
            if (input == null) return NotFound();

            return View(input);
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var points = _db.Points
                .Where(p => p.AnalysisInputId == id);
            _db.Points.RemoveRange(points);

            var input = _db.Analyses.Find(id);
            _db.Analyses.Remove(input);

            _db.SaveChanges();

            return RedirectToAction("Dashboard");
        }

    }
}

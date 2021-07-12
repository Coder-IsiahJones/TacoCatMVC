using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TacoCatMVC.Models;

namespace TacoCatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reverse()
        {
            Palindrome model = new();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(Palindrome palindrome)
        {
            // Variables
            string inputWord = palindrome.InputWord;
            string revWord = "";

            // Reverse word
            for (int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }

            // Result
            palindrome.RevWord = revWord;

            // Change to lowercase
            // Remove white-spaces
            // Remove special characters
            revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9] + ", "" );
            inputWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9] + ", "");

            // Check for palindrome
            if (revWord == inputWord)
            {
                palindrome.IsPalindrom = true;
                palindrome.Message = $"Success {palindrome.InputWord} is a Palindrome";
            }
            else
            {
                palindrome.IsPalindrom = false;
                palindrome.Message = $"Sorry {palindrome.InputWord} is a not Palindrome";
            }

            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

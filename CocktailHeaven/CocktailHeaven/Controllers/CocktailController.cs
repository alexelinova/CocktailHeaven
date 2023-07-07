﻿using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CocktailHeaven.Controllers
{
	public class CocktailController : Controller
	{
		private readonly ICategoryService categoryService;
		private readonly ICocktailService cocktailService;
		public CocktailController(ICategoryService categoryService, ICocktailService cocktailService)
		{
			this.categoryService = categoryService;
			this.cocktailService = cocktailService;
		}

		[HttpGet]
		public IActionResult AddIngredientCount()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddIngredientCount(int count)
		{
			if (count <= 0)
			{
				return View();
			}

			return RedirectToAction(nameof(Create), new { ingredientCount = count });
		}

		public async Task<IActionResult> Create(int ingredientCount)
		{
			var model = new AddCocktailFormModel
			{
				Categories = await this.categoryService.GetAllCategoriesAsync(),
				CountOfIngredients = ingredientCount
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddCocktailFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				model.Categories = await this.categoryService.GetAllCategoriesAsync();
				return this.View(model);
			}

			var userId = User.Claims
				.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

			if (userId != null && Guid.TryParse(userId.Value, out Guid id))
			{
				await this.cocktailService.CreateCocktailAsync(model, id);
			}

			return View();
		}
	}
}

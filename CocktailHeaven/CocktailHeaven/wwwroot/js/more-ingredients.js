const addIngredientBtn = document.getElementById("addIngredientBtn");
const ingredientContainer = document.getElementById("ingredientsContainer")
addIngredientBtn.addEventListener('click', function () {

	let ingredientIndex = ingredientContainer.childElementCount;

	const nextIndex = ingredientIndex++;

	const div = document.createElement("div");
	div.classList.add("row");

	const ingredientNameDiv = document.createElement("div");
	ingredientNameDiv.classList.add("mb-3", "col-sm-4");

	const ingredientNameLabel = document.createElement("label");
	ingredientNameLabel.setAttribute("for", `Ingredients_${nextIndex}__IngredientName`);
	ingredientNameLabel.textContent = "Ingredient";
	ingredientNameDiv.appendChild(ingredientNameLabel);

	const ingredientNameInput = document.createElement("input");
	ingredientNameInput.setAttribute("type", "text");
	ingredientNameInput.setAttribute("name", `Ingredients[${nextIndex}].IngredientName`);
	ingredientNameInput.classList.add("form-control");
	ingredientNameDiv.appendChild(ingredientNameInput);

	const quantityDiv = document.createElement("div");
	quantityDiv.classList.add("mb-3", "col-sm-4");

	const quantityLabel = document.createElement("label");
	quantityLabel.setAttribute("for", `Ingredients_${nextIndex}__Quantity`);
	quantityLabel.textContent = "Quantity";
	quantityDiv.appendChild(quantityLabel);

	const quantityInput = document.createElement("input");
	quantityInput.setAttribute("type", "text");
	quantityInput.setAttribute("name", `Ingredients[${nextIndex}].Quantity`);
	quantityInput.classList.add("form-control");
	quantityDiv.appendChild(quantityInput);

	const noteDiv = document.createElement("div");
	noteDiv.classList.add("mb-3", "col-sm-4");

	const noteLabel = document.createElement("label");
	noteLabel.setAttribute("for", `Ingredients_${nextIndex}__Note`);
	noteLabel.innerText = "Note";
	noteDiv.appendChild(noteLabel);

	const noteInput = document.createElement("input");
	noteInput.setAttribute("type", "text");
	noteInput.setAttribute("name", `Ingredients[${nextIndex}].Note`);
	noteInput.setAttribute("placeholder", "optional");
	noteInput.classList.add("form-control");
	noteDiv.appendChild(noteInput);

	div.appendChild(ingredientNameDiv);
	div.appendChild(quantityDiv);
	div.appendChild(noteDiv);
	ingredientsContainer.appendChild(div);
});